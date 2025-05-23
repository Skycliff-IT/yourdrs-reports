using Microsoft.EntityFrameworkCore;
using VA.API.Data;
using VA.API.Models;

namespace VA.API.Tests.Data
{
    public class CustomerContextTests
    {
        private DbContextOptions<CustomerContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public void CustomerContext_CreatesDatabase_And_SeedsData()
        {
            // Arrange
            var options = CreateInMemoryOptions();

            // Act
            using (var context = new CustomerContext(options))
            {
                context.Database.EnsureCreated();
                var customers = context.Customers.ToList();

                // Assert
                Assert.Equal(2, customers.Count);

                Assert.Contains(customers, c =>
                    c.Id == Guid.Parse("e455f48f-35d1-4fa4-aaf1-4f7fcf5da22a") &&
                    c.CustomerCode == "CUST-001" &&
                    c.CustomerName == "John Doe");

                Assert.Contains(customers, c =>
                    c.Id == Guid.Parse("0c30022b-8617-47ec-8e2d-6f327f507084") &&
                    c.CustomerCode == "CUST-002" &&
                    c.CustomerName == "Jane Smith");
            }
        }

        [Fact]
        public void CustomerContext_CanAddCustomer()
        {
            // Arrange
            var options = CreateInMemoryOptions();

            // Act
            using (var context = new CustomerContext(options))
            {
                var newCustomer = new Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerCode = "CUST-003",
                    CustomerName = "Alice Johnson"
                };
                context.Customers.Add(newCustomer);
                context.SaveChanges();

                // Assert
                var customer = context.Customers.SingleOrDefault(c => c.CustomerCode == "CUST-003");
                Assert.NotNull(customer);
                Assert.Equal("Alice Johnson", customer!.CustomerName);
            }
        }
    }
}
