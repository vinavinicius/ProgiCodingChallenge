using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ProgiCodingChallenge.Tests.Integration.WebApplications;

public class ProgiCodingChallengeApiWebApplicationFactory : WebApplicationFactory<ProgiCodingChallenge.API.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
    }
}
