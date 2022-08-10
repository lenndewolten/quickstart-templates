using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
#if (enableHealthCheck)
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
#endif

namespace Kubernetes.Api.Template.UnitTests
{
    public class ApplicationFactory : WebApplicationFactory<Program>
    {
#if (enableHealthCheck)
        private readonly Mock<HealthCheckService> _healthCheckServiceMock = new(MockBehavior.Strict);
#endif
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
#if (enableHealthCheck)
                _healthCheckServiceMock
                .Setup(m => m.CheckHealthAsync(It.IsAny<Func<HealthCheckRegistration, bool>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HealthReport(new Dictionary<string, HealthReportEntry>(), HealthStatus.Healthy, TimeSpan.FromMilliseconds(100)));

                services.AddSingleton(_ => _healthCheckServiceMock.Object);
#endif
            });
        }
    }
}