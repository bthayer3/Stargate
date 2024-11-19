using System.Text;
using Xunit;

namespace StargateTests.Endpoints
{
    public class CreateAstronautDutyTests
    {
        [Fact]
        public async Task CreateAstronautDutyForInvalidPersonShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grant Lee\",\"rank\": \"PVT\",\"dutyTitle\": \"PayloadSpecialist\",\"dutyStartDate\": \"2024-11-12\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/AstronautDuty", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"Person does not exist.\",\"responseCode\":400}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyWithInvalidRankShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grace Johnson\",\"rank\": \"JUNK\",\"dutyTitle\": \"PayloadSpecialist\",\"dutyStartDate\": \"2024-11-12\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/AstronautDuty", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"The value 'JUNK' is not a valid type for Rank. These are the valid types: PVT, PFC, SPC, CPL, SGT, SSGT, SFC, MSGT, FSGT, WO, CW2, CW3, CW4, CW5, LT2, LT1, CPT, MAJ, LTC, COL, BG, MG, LTG, GEN, GOA.\",\"responseCode\":400}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyWithInvalidDutyTitleShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grace Johnson\",\"rank\": \"PVT\",\"dutyTitle\": \"JUNK\",\"dutyStartDate\": \"2024-11-12\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/AstronautDuty", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"The value 'JUNK' is not a valid type for DutyTitle. These are the valid types: MissionSpecialist, PayloadSpecialist, FlightEngineer, Commander, Pilot, LunarModulePilot, ScienceOfficer, Retired.\",\"responseCode\":400}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyWithRetiredDutyTitleForRetiredPersonShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Robert Bush\",\"rank\": \"GOA\",\"dutyTitle\": \"Retired\",\"dutyStartDate\": \"2024-11-12\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/AstronautDuty", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"Cannot create new astronaut duty for person because person is 'Retired'.\",\"responseCode\":400}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyWithPastStartDateShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grace Johnson\",\"rank\": \"PVT\",\"dutyTitle\": \"Pilot\",\"dutyStartDate\": \"2023-10-12\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/AstronautDuty", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"New astronaut duty must be after 01/01/2024.\",\"responseCode\":400}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyWithSameLatestDutyDateShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grace Johnson\",\"rank\": \"PVT\",\"dutyTitle\": \"Pilot\",\"dutyStartDate\": \"2024-01-01\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/AstronautDuty", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"New astronaut duty must be after 01/01/2024.\",\"responseCode\":400}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyWithSameLatestDutyShouldFail()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grace Johnson\",\"rank\": \"PVT\",\"dutyTitle\": \"MissionSpecialist\",\"dutyStartDate\": \"2024-01-02\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/AstronautDuty", inputContent);

            // Assert
            Xunit.Assert.False(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"success\":false,\"message\":\"New astronaut duty must be a different duty instead of 'Mission Specialist'.\",\"responseCode\":400}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyWithNewFutureDateDutyShouldSucceed()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grace Johnson\",\"rank\": \"PVT\",\"dutyTitle\": \"Pilot\",\"dutyStartDate\": \"2024-01-02\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var response = await appFactory.PostAsync("/AstronautDuty", inputContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"id\":8,\"success\":true,\"message\":\"Successful\",\"responseCode\":201}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyMatchesInAstronautDetailsTable()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grace Johnson\",\"rank\": \"PVT\",\"dutyTitle\": \"Pilot\",\"dutyStartDate\": \"2024-01-02\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var postResponse = await appFactory.PostAsync("/AstronautDuty", inputContent);
            var response = await appFactory.GetAsync("/AstronautDuty/Grace%20Johnson");

            // Assert
            postResponse.EnsureSuccessStatusCode();
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"person\":{\"personId\":2,\"name\":\"Grace Johnson\",\"currentRank\":\"Private\",\"currentDutyTitle\":\"Pilot\",\"careerStartDate\":\"2024-01-01T00:00:00\",\"careerEndDate\":null},\"astronautDuties\":[{\"id\":8,\"rank\":\"Private\",\"dutyTitle\":\"Pilot\",\"dutyStartDate\":\"2024-01-02T00:00:00\",\"dutyEndDate\":null},{\"id\":1,\"rank\":\"Private\",\"dutyTitle\":\"Mission Specialist\",\"dutyStartDate\":\"2024-01-01T00:00:00\",\"dutyEndDate\":\"2024-01-01T00:00:00\"}],\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyForRetiredForOneDayLaterMatchesInAstronautDetailsTable()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grace Johnson\",\"rank\": \"PVT\",\"dutyTitle\": \"Retired\",\"dutyStartDate\": \"2024-01-02\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var postResponse = await appFactory.PostAsync("/AstronautDuty", inputContent);
            var response = await appFactory.GetAsync("/AstronautDuty/Grace%20Johnson");

            // Assert
            postResponse.EnsureSuccessStatusCode();
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"person\":{\"personId\":2,\"name\":\"Grace Johnson\",\"currentRank\":\"Private\",\"currentDutyTitle\":\"Retired\",\"careerStartDate\":\"2024-01-01T00:00:00\",\"careerEndDate\":\"2024-01-01T00:00:00\"},\"astronautDuties\":[{\"id\":8,\"rank\":\"Private\",\"dutyTitle\":\"Retired\",\"dutyStartDate\":\"2024-01-02T00:00:00\",\"dutyEndDate\":null},{\"id\":1,\"rank\":\"Private\",\"dutyTitle\":\"Mission Specialist\",\"dutyStartDate\":\"2024-01-01T00:00:00\",\"dutyEndDate\":\"2024-01-01T00:00:00\"}],\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyForRetiredForYearsLaterMatchesAstronautDetailsTable()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Grace Johnson\",\"rank\": \"PVT\",\"dutyTitle\": \"Retired\",\"dutyStartDate\": \"2026-07-12\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var postResponse = await appFactory.PostAsync("/AstronautDuty", inputContent);
            var response = await appFactory.GetAsync("/AstronautDuty/Grace%20Johnson");

            // Assert
            postResponse.EnsureSuccessStatusCode();
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"person\":{\"personId\":2,\"name\":\"Grace Johnson\",\"currentRank\":\"Private\",\"currentDutyTitle\":\"Retired\",\"careerStartDate\":\"2024-01-01T00:00:00\",\"careerEndDate\":\"2026-07-11T00:00:00\"},\"astronautDuties\":[{\"id\":8,\"rank\":\"Private\",\"dutyTitle\":\"Retired\",\"dutyStartDate\":\"2026-07-12T00:00:00\",\"dutyEndDate\":null},{\"id\":1,\"rank\":\"Private\",\"dutyTitle\":\"Mission Specialist\",\"dutyStartDate\":\"2024-01-01T00:00:00\",\"dutyEndDate\":\"2026-07-11T00:00:00\"}],\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }

        [Fact]
        public async Task CreateAstronautDutyForFirstTimeForNormalPersonMatchesAstronautDetailsTable()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory().CreateClient();
            var rawString = "{\"name\": \"Mark Pooler\",\"rank\": \"PVT\",\"dutyTitle\": \"PayloadSpecialist\",\"dutyStartDate\": \"2024-09-02\"}";
            var inputContent = new StringContent(rawString, Encoding.UTF8, "application/json");

            // Act
            var postResponse = await appFactory.PostAsync("/AstronautDuty", inputContent);
            var response = await appFactory.GetAsync("/AstronautDuty/Mark%20Pooler");

            // Assert
            postResponse.EnsureSuccessStatusCode();
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("{\"person\":{\"personId\":1,\"name\":\"Mark Pooler\",\"currentRank\":\"Private\",\"currentDutyTitle\":\"Payload Specialist\",\"careerStartDate\":\"2024-09-02T00:00:00\",\"careerEndDate\":null},\"astronautDuties\":[{\"id\":8,\"rank\":\"Private\",\"dutyTitle\":\"Payload Specialist\",\"dutyStartDate\":\"2024-09-02T00:00:00\",\"dutyEndDate\":null}],\"success\":true,\"message\":\"Successful\",\"responseCode\":200}", responseContent);
        }
    }
}