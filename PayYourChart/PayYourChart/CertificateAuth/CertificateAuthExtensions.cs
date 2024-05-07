using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Certificate;
using PayYourChart.Module.Common;

namespace PayYourChart.CertificateAuth.Extensions;

internal static partial class CertificateAuthExtensions
{
    const string CertificateActor = "CertificateActor";

    public static void AddCertificateAuth(this IServiceCollection services)
    {
        services.AddSingleton<ICertificateValidationService, CertificateValidationService>();

        services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
            .AddCertificate(CertificateAuthenticationDefaults.AuthenticationScheme, options => 
            {
                // Only trust the cert if it comes from my custom ca-cert
                // Don't hardcode passwords in production. Use key vault.
                options.AllowedCertificateTypes = CertificateTypes.SelfSigned;
                options.RevocationMode = X509RevocationMode.NoCheck;

                options.Events = new CertificateAuthenticationEvents
                {
                    OnCertificateValidated = context =>
                    {
                        // var validationService = context.HttpContext.RequestServices
                        //     .GetRequiredService<ICertificateValidationService>();

                        // if (validationService.ValidateCertificate(context.ClientCertificate))
                        if (true)
                        {
                            var claims = new[]
                            {
                                new Claim(
                                    ClaimTypes.NameIdentifier,
                                    context.ClientCertificate.Subject,
                                    ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                new Claim(
                                    ClaimTypes.Name,
                                    context.ClientCertificate.Subject,
                                    ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                new Claim(
                                    ClaimTypes.Role,
                                    Roles.Admin,
                                    ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                new Claim(
                                    ClaimTypes.Actor,
                                    CertificateActor,
                                    ClaimValueTypes.String, context.Options.ClaimsIssuer),
                            };

                            context.Principal = new ClaimsPrincipal(
                                new ClaimsIdentity(claims, context.Scheme.Name));
                            context.Success();
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var failMessage = "Client SSL Cert is invalid.";
                        Console.WriteLine(context.Exception);
                        context.Fail(failMessage);
                        return Task.CompletedTask;   
                    }
                };
            });
    
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.AdminCertPolicy, x => 
                x.RequireRole(Roles.Admin)
                .RequireClaim(ClaimTypes.Actor, CertificateActor));
        });
    }
}
