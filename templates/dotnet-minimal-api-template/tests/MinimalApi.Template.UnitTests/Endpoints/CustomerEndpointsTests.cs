using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi.Template.Endpoints;

namespace MinimalApi.Template.UnitTests.Endpoints
{
    public class CustomerEndpointsTests
    {
        [Fact]
        public async Task Given_a_CustomerEndpoint_when_the_GetCustomers_is_called_with_an_Id_that_does_not_exist_then_NotFound_is_returned()
        {
            // Arrange
            var customersRepositoryFake = A.Fake<ICustomersRepository>(x => x.Strict());
            A.CallTo(() => customersRepositoryFake.RetrieveById(A<Guid>.Ignored)).Returns((CustomerDto?)null);

            // Act
            var actual = await CustomerEndpoints.GetCustomer(Guid.NewGuid(), customersRepositoryFake);

            // Assert
            actual.Result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public async Task Given_a_CustomerEndpoint_when_the_GetCustomers_is_called_with_an_Id_that_exists_then_Ok_is_returned()
        {
            // Arrange
            var customersRepositoryFake = A.Fake<ICustomersRepository>(x => x.Strict());
            A.CallTo(() => customersRepositoryFake.RetrieveById(A<Guid>.Ignored)).Returns(A.Dummy<CustomerDto>());

            // Act
            var actual = await CustomerEndpoints.GetCustomer(Guid.NewGuid(), customersRepositoryFake);

            // Assert
            actual.Result.Should().BeOfType<Ok<CustomerDto>>();
        }
    }
}
