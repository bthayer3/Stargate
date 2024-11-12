using Xunit;

namespace StargateTests.Endpoints
{
    public class GetPersonTests
    {
        [Fact]
        public async Task GetPersonEndpointShouldReturnFourPeople()
        {
            // Act
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var response = await appFactory.GetAsync("/Person");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"people\":[{\"personId\":1,\"name\":\"Mark Pooler\",\"currentRank\":\"\",\"currentDutyTitle\":\"\",\"careerStartDate\":null,\"careerEndDate\":null},{\"personId\":2,\"name\":\"Grace Johnson\",\"currentRank\":\"Private\",\"currentDutyTitle\":\"Mission Specialist\",\"careerStartDate\":\"2024-01-01T00:00:00\",\"careerEndDate\":null},{\"personId\":3,\"name\":\"Jake Lee\",\"currentRank\":\"Specialist\",\"currentDutyTitle\":\"Commander\",\"careerStartDate\":\"2022-03-15T00:00:00\",\"careerEndDate\":null},{\"personId\":4,\"name\":\"Robert Bush\",\"currentRank\":\"General of the Army\",\"currentDutyTitle\":\"Retired\",\"careerStartDate\":\"2022-09-19T00:00:00\",\"careerEndDate\":\"2024-02-13T00:00:00\"}],\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }

        [Fact]
        public async Task GetPersonByNameEndpointWithValidNameShouldReturnOnePerson()
        {
            // Act
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var response = await appFactory.GetAsync("/Person/Mark%20Pooler");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"person\":{\"personId\":1,\"name\":\"Mark Pooler\",\"currentRank\":\"\",\"currentDutyTitle\":\"\",\"careerStartDate\":null,\"careerEndDate\":null},\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }

        [Fact]
        public async Task GetPersonByNameEndpointWithInvalidNameShouldReturnNullForPerson()
        {
            // Act
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var response = await appFactory.GetAsync("/Person/Matt%20Ashley");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"person\":null,\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }
    }
}