using System;
using System.Collections.Generic;
using VA.API.Customers.GetCustomers;
using VA.API.Models;
using VA.CrossCutting.Pagination;
using Xunit;

namespace VA.API.Tests.Customers.GetCustomers
{
    public class GetCustomersQueryTests
    {
        [Fact]
        public void GetCustomersQuery_StoresRequest()
        {
            // Arrange
            var request = new PaginationRequest(PageIndex: 1, PageSize: 10);

            // Act
            var query = new GetCustomersQuery(request);

            // Assert
            Assert.Equal(request, query.Request);
        }

        [Fact]
        public void GetCustomersResponse_StoresPaginatedResult()
        {
            // Arrange
            var customerDtos = new List<CustomerDto>
            {
                new CustomerDto
                {
                    Id = Guid.NewGuid(),
                    CustomerCode = "C001",
                    CustomerName = "Test Customer"
                }
            };
            var paginatedResult = new PaginatedResult<CustomerDto>(
                pageIndex: 1,
                pageSize: 10,
                count: 1,
                data: customerDtos
            );

            // Act
            var response = new GetCustomersResponse(paginatedResult);

            // Assert
            Assert.Equal(paginatedResult, response.Customers);
            Assert.Equal(1, response.Customers.Count);
            Assert.Equal(customerDtos, response.Customers.Data);
        }
    }

}
