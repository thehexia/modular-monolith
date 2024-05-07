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
            .AddCertificate(options => 
            {
                // Only trust the cert if it comes from my custom ca-cert
                // Don't hardcode passwords in production. Use key vault.
                options.CustomTrustStore = [new X509Certificate2("certs/ca-cert.pfx")];

                // Only ignoring revocation lists in this because I'm too lazy to publish a revocation list
                // and the default behavior is to deny if one doesn't exist.
                options.RevocationMode = X509RevocationMode.NoCheck;
                options.ChainTrustValidationMode = X509ChainTrustMode.CustomRootTrust;
                options.AllowedCertificateTypes = CertificateTypes.All;

                options.Events = new CertificateAuthenticationEvents
                {
                    OnCertificateValidated = context =>
                    {
                        var validationService = context.HttpContext.RequestServices
                            .GetRequiredService<ICertificateValidationService>();

                        if (validationService.ValidateCertificate(context.ClientCertificate))
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
                        var logger = context.HttpContext.RequestServices
                            .GetRequiredService<ILogger<CertificateValidationService>>();

                        var failMessage = "Client SSL Cert is invalid.";
                        logger.LogError(context.Exception, failMessage);
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
