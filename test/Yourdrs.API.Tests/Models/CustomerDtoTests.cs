using VA.API.Models;

namespace VA.API.Tests.Models
{
    public class CustomerDtoTests
    {
        [Fact]
        public void CustomerDto_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "CUST001";
            var name = "Sample Customer";

            // Act
            var dto = new CustomerDto
            {
                Id = id,
                CustomerCode = code,
                CustomerName = name
            };

            // Assert
            Assert.Equal(id, dto.Id);
            Assert.Equal(code, dto.CustomerCode);
            Assert.Equal(name, dto.CustomerName);
        }

        [Fact]
        public void CustomerDto_DefaultValues_AreSet()
        {
            // Act
            var dto = new CustomerDto();

            // Assert
            Assert.Null(dto.CustomerCode);
            Assert.Null(dto.CustomerName);
        }
    }
}
