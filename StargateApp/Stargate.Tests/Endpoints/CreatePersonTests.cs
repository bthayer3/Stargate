using System.Text;
using Xunit;

namespace StargateTests.Endpoints
{
    public class CreatePersonTests
    {
        [Fact]
        public async Task CreatePersonWithNewNameShouldSucceed()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "\"Tom Bishop\"";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/Person", inputContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"id\":5,\"success\":true,\"message\":\"Successful\",\"responseCode\":201}", responseContent);
        }

        [Fact]
        public async Task CreatePersonWithNoNameShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "\"\"";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/Person", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"Name is required to create a person.\",\"responseCode\":400}", responseContent);
        }

        [Fact]
        public async Task CreatePersonWithExistingNameShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "\"Mark Pooler\"";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/Person", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"Person by the name 'Mark Pooler' already exists.\",\"responseCode\":400}", responseContent);
        }
    }
}