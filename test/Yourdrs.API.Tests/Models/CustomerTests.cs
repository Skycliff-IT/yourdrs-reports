using VA.API.Models;

namespace VA.API.Tests.Models
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "CUST123";
            var name = "Test Customer";

            // Act
            var customer = new Customer
            {
                Id = id,
                CustomerCode = code,
                CustomerName = name
            };

            // Assert
            Assert.Equal(id, customer.Id);
            Assert.Equal(code, customer.CustomerCode);
            Assert.Equal(name, customer.CustomerName);
        }

        [Fact]
        public void Customer_DefaultValues_AreSet()
        {
            // Act
            var customer = new Customer();

            // Assert
            Assert.Equal(Guid.Empty, customer.Id); // If Id is not set, it will be Guid.Empty
            Assert.Null(customer.CustomerCode);
            Assert.Null(customer.CustomerName);
        }
    }
}