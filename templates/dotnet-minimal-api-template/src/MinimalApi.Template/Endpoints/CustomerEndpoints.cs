using Microsoft.AspNetCore.Http.HttpResults;

namespace MinimalApi.Template.Endpoints
{
    internal static class CustomerEndpoints
    {
        public static RouteGroupBuilder MapCustomerEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllCustomers);
            group.MapGet("/{id}", GetCustomer);
            return group;
        }

        public static async Task<Ok<IEnumerable<CustomerDto>>> GetAllCustomers(ICustomersRepository customersRepository)
        {
            var customers = await customersRepository.RetrieveAll();

            return TypedResults.Ok(customers);
        }

        public static async Task<Results<Ok<CustomerDto>, NotFound>> GetCustomer(Guid id, ICustomersRepository customersRepository)
        {
            var customer = await customersRepository.RetrieveById(id);

            return customer is not null
                ? TypedResults.Ok(customer)
                : TypedResults.NotFound();
        }
    }

    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public interface ICustomersRepository
    {
        Task<IEnumerable<CustomerDto>> RetrieveAll();
        Task<CustomerDto?> RetrieveById(Guid id);
    }

    internal class CustomersRepository : ICustomersRepository
    {
        private readonly IReadOnlyCollection<CustomerDto> _customers = new List<CustomerDto>
        {
            new CustomerDto {Id = new Guid("34e79e90-ad20-4b8f-811c-c36934f05af1"), Name = "Billy Brown Bob" },
            new CustomerDto {Id = new Guid("34e79e90-ad20-4b8f-811c-c36934f05af2"), Name = "Heowdy Dillan" },
            new CustomerDto {Id = new Guid("34e79e90-ad20-4b8f-811c-c36934f05af3"), Name = "Kechuka billa bing bong" }
        }.AsReadOnly();

        public Task<IEnumerable<CustomerDto>> RetrieveAll()
        {
            return Task.FromResult(_customers.AsEnumerable());
        }

        public Task<CustomerDto?> RetrieveById(Guid id)
        {
            return Task.FromResult(_customers.FirstOrDefault(c => c.Id == id));
        }
    }
}
