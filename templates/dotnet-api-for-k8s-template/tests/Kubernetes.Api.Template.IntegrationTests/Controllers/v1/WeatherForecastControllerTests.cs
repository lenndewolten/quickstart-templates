using FluentAssertions;
using Kubernetes.Api.Template.UnitTests;
using System.Net;
using Xunit;

namespace Kubernetes.Api.Template.IntegrationTests.Controllers.v1
{
    public class WeatherForecastControllerTests : IClassFixture<ApplicationFactory>
    {
        private readonly ApplicationFactory _factory;
        private const string _baseUrl = "/WeatherForecast";

        public WeatherForecastControllerTests(ApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Given_a_WeatherForecastController_when_GetWeatherForecast_is_called_then_the_correct_response_is_returned()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(_baseUrl);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
