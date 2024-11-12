using Xunit;

namespace StargateTests.Endpoints
{
    public class GetAstronautDutiesTests
    {
        [Fact]
        public async Task GetAstronautDutiesForNormalPersonShouldReturnPersonWithNoDuties()
        {
            // Act
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var response = await appFactory.GetAsync("/AstronautDuty/Mark%20Pooler");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"person\":{\"personId\":1,\"name\":\"Mark Pooler\",\"currentRank\":\"\",\"currentDutyTitle\":\"\",\"careerStartDate\":null,\"careerEndDate\":null},\"astronautDuties\":[],\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }

        [Fact]
        public async Task GetAstronautDutiesForInvalidPersonShouldReturnNoPersonOrDuties()
        {
            // Act
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var response = await appFactory.GetAsync("/AstronautDuty/Beth%20Atlas");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"person\":null,\"astronautDuties\":[],\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }

        [Fact]
        public async Task GetAstronautDutiesForAstronautShouldReturnPersonWithDuties()
        {
            // Act
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var response = await appFactory.GetAsync("/AstronautDuty/Grace%20Johnson");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"person\":{\"personId\":2,\"name\":\"Grace Johnson\",\"currentRank\":\"Private\",\"currentDutyTitle\":\"Mission Specialist\",\"careerStartDate\":\"2024-01-01T00:00:00\",\"careerEndDate\":null},\"astronautDuties\":[{\"id\":1,\"rank\":\"Private\",\"dutyTitle\":\"Mission Specialist\",\"dutyStartDate\":\"2024-01-01T00:00:00\",\"dutyEndDate\":null}],\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }
    }
}