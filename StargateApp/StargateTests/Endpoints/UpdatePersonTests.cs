using System.Text;
using Xunit;

namespace StargateTests.Endpoints
{
    public class UpdatePersonTests
    {
        [Fact]
        public async Task UpdatePersonThatExistsWithNewNameShouldSucceed()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "\"Mark S Pooler\"";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PutAsync("/Person/1", inputContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"id\":1,\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }

        [Fact]
        public async Task UpdatePersonWithInvalidIdShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "\"Grady Shaw\"";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PutAsync("/Person/99", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"Person does not exist for Id 99.\",\"responseCode\":400}", responseContent);
        }

        [Fact]
        public async Task UpdatePersonThatExistsWithTakenNameShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "\"Mark Pooler\"";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PutAsync("/Person/2", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"Cannot update name to 'Mark Pooler' as this name is already in use.\",\"responseCode\":400}", responseContent);
        }
    }
}