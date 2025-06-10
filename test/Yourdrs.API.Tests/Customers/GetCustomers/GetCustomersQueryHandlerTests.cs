using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VA.API.Customers.GetCustomers;
using VA.API.Data;
using VA.API.Models;
using VA.CrossCutting.Pagination;
using Xunit;

namespace VA.API.Tests.Customers.GetCustomers
{
    public class GetCustomersQueryHandlerTests
    {
        private CustomerContext CreateContextWithData(IEnumerable<Customer> customers)
        {
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CustomerContext(options);
            context.Database.EnsureCreated();
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task Handle_ReturnsPaginatedResult_WithCorrectData()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), CustomerCode = "CUST-001", CustomerName = "Alpha" },
                new Customer { Id = Guid.NewGuid(), CustomerCode = "CUST-002", CustomerName = "Bravo" },
                new Customer { Id = Guid.NewGuid(), CustomerCode = "CUST-003", CustomerName = "Charlie" }
            };
            var context = CreateContextWithData(customers);

            var handler = new GetCustomersQueryHandler(context);
            var request = new PaginationRequest(PageIndex: 0, PageSize: 2);
            var query = new GetCustomersQuery(request);

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Customers);
            Assert.Equal(0, response.Customers.PageIndex);
            Assert.Equal(2, response.Customers.PageSize);
            Assert.Equal(5, response.Customers.Count);
            Assert.Equal(2, response.Customers.Data.Count());

            // Should be ordered by CustomerName: Alpha, Bravo
            var data = response.Customers.Data.ToList();
            Assert.Equal("Alpha", data[0].CustomerName);
            Assert.Equal("Bravo", data[1].CustomerName);
        }

        [Fact]
        public async Task Handle_ReturnsEmpty_WhenNoCustomers()
        {
            // Arrange
            var context = CreateContextWithData(Array.Empty<Customer>());
            var handler = new GetCustomersQueryHandler(context);
            var request = new PaginationRequest(PageIndex: 0, PageSize: 10);
            var query = new GetCustomersQuery(request);

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Customers);
            Assert.Equal(2, response.Customers.Count);
        }

        [Fact]
        public async Task Handle_SkipsAndTakes_Correctly()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), CustomerCode = "CUST-001", CustomerName = "Alpha" },
                new Customer { Id = Guid.NewGuid(), CustomerCode = "CUST-002", CustomerName = "Bravo" },
                new Customer { Id = Guid.NewGuid(), CustomerCode = "CUST-003", CustomerName = "Charlie" }
            };
            var context = CreateContextWithData(customers);

            var handler = new GetCustomersQueryHandler(context);
            var request = new PaginationRequest(PageIndex: 1, PageSize: 2); // Should skip 2, take 1
            var query = new GetCustomersQuery(request);

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal("Charlie", response.Customers.Data.First().CustomerName);
        }
    }
}
