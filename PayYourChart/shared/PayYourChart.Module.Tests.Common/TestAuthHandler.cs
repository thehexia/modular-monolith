using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PayYourChart.Module.Common;

namespace PayYourChart.Module.Tests.Common;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        const string claimsIssuer = "Test issuer";
        const string subject = "Test subject";
        const string certificateActor = "CertificateActor";

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                subject,
                ClaimValueTypes.String, claimsIssuer),
            new Claim(
                ClaimTypes.Name,
                subject,
                ClaimValueTypes.String, claimsIssuer),
            new Claim(
                ClaimTypes.Role,
                Roles.Admin,
                ClaimValueTypes.String, claimsIssuer),
            new Claim(
                ClaimTypes.Actor,
                certificateActor,
                ClaimValueTypes.String, claimsIssuer),
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "TestScheme");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}