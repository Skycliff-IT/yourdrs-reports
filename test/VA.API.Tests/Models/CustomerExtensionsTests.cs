using VA.API.Models;

namespace VA.API.Tests.Models
{
    public class CustomerExtensionsTests
    {
        [Fact]
        public void ToCustomerDtoList_MapsAllPropertiesCorrectly()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerCode = "C001",
                    CustomerName = "Customer One"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerCode = "C002",
                    CustomerName = "Customer Two"
                }
            };

            // Act
            var dtos = customers.ToCustomerDtoList().ToList();

            // Assert
            Assert.Equal(customers.Count, dtos.Count);
            for (int i = 0; i < customers.Count; i++)
            {
                Assert.Equal(customers[i].Id, dtos[i].Id);
                Assert.Equal(customers[i].CustomerCode, dtos[i].CustomerCode);
                Assert.Equal(customers[i].CustomerName, dtos[i].CustomerName);
            }
        }

        [Fact]
        public void ToCustomerDtoList_EmptyList_ReturnsEmpty()
        {
            // Arrange
            var customers = new List<Customer>();

            // Act
            var dtos = customers.ToCustomerDtoList();

            // Assert
            Assert.Empty(dtos);
        }

        [Fact]
        public void ToCustomerDtoList_NullInput_ThrowsArgumentNullException()
        {
            // Arrange
            List<Customer>? customers = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => customers!.ToCustomerDtoList().ToList());
        }
    }
}
