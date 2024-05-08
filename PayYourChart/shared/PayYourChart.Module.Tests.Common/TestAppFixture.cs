
using System.Net.Http.Headers;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace PayYourChart.Module.Tests.Common;

public abstract class BaseTestApp : AppFixture<Program>
{
    protected override async Task SetupAsync()
    {
        // place one-time setup code here
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: "TestScheme");
    }
    
    protected override void ConfigureApp(IWebHostBuilder builder)
    {
        // do host builder config here
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(defaultScheme: "TestScheme")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "TestScheme", options => { });
        });
    }
}

