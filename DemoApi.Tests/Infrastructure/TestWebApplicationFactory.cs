using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DemoApi.Tests.Infrastructure;

public class TestWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // 👇 This is the ONLY thing we need
        builder.UseEnvironment("Test");
    }
}
