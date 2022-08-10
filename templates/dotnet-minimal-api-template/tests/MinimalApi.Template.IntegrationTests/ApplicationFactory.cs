using Microsoft.AspNetCore.Mvc.Testing;
#if (enableHealthCheck)
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
#endif

namespace MinimalApi.Template.IntegrationTests
{
    public class ApplicationFactory : WebApplicationFactory<Program>
    {
#if (enableHealthCheck)
        private readonly HealthCheckService _healthCheckServiceFake = A.Fake<HealthCheckService>();
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton(_ => _healthCheckServiceFake);
            });
        }
#endif
    }
}