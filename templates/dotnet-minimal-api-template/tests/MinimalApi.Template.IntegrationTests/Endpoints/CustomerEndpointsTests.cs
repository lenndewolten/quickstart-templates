
namespace MinimalApi.Template.IntegrationTests.Endpoints
{
    public class CustomerEndpointsTests : IClassFixture<ApplicationFactory>
    {
        private readonly ApplicationFactory _factory;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public CustomerEndpointsTests(ApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Given_a_CustomerEndpoint_when_the_GetAllCustomers_is_called_then_correct_response_is_returned()
        {
            // Arrange
            var customers = A.CollectionOfDummy<CustomerDto>(3);
            var customersRepositoryFake = A.Fake<ICustomersRepository>(x => x.Strict());
            A.CallTo(() => customersRepositoryFake.RetrieveAll()).Returns(customers);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(customersRepositoryFake);
                });
            })
            .CreateClient();

            // Act
            var response = await client.GetAsync("/customers");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            var dtos = await JsonSerializer.DeserializeAsync<IEnumerable<CustomerDto>>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions);
            dtos.Should().BeEquivalentTo(customers);
        }

        [Fact]
        public async Task Given_a_CustomerEndpoint_when_the_GetById_is_called_then_correct_response_is_returned()
        {
            // Arrange
            var customer = A.Dummy<CustomerDto>();
            var customersRepositoryFake = A.Fake<ICustomersRepository>(x => x.Strict());
            A.CallTo(() => customersRepositoryFake.RetrieveById(customer.Id)).Returns(customer);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(customersRepositoryFake);
                });
            })
            .CreateClient();

            // Act
            var response = await client.GetAsync($"/customers/{customer.Id}");

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            var dtos = await JsonSerializer.DeserializeAsync<CustomerDto>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions);
            dtos.Should().BeEquivalentTo(customer);
        }
    }
}
