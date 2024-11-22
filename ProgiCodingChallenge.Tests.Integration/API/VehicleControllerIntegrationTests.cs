using FluentAssertions;
using Newtonsoft.Json;
using ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;
using ProgiCodingChallenge.Tests.Integration.WebApplications;
using System.Text;
using Xunit;

namespace ProgiCodingChallenge.Tests.Integration.API;

public class VehicleControllerIntegrationTests : ProgiCodingChallengeApiWebApplicationFactory
{
    private readonly HttpClient _client;

    public VehicleControllerIntegrationTests()
    {
        _client = CreateClient();
    }

    [Fact]
    public async Task CalculatePrice_ShouldReturnOk_WhenCommandIsValid()
    {
        // Arrange
        var command = new VehicleCalculatePriceCommand
        {
            VehicleType = "Common",
            VehiclePrice = 100M
        };

        var content = new StringContent(
            JsonConvert.SerializeObject(command),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await _client.PostAsync("/api/vehicle/CalculatePrice", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var responseData = JsonConvert.DeserializeObject<VehicleCalculatePriceDto>(
            await response.Content.ReadAsStringAsync()
        );

        responseData.Should().NotBeNull();
        responseData!.Type.Should().Be("Common");
        responseData.TotalPrice.Should().Be(217.000M);
        responseData.Price.Should().Be(100M);
    }

    [Fact]
    public async Task CalculatePrice_ShouldReturnBadRequest_WhenCommandIsInvalid()
    {
        // Arrange
        var command = new VehicleCalculatePriceCommand
        {
            VehicleType = "",
            VehiclePrice = -1
        };

        var content = new StringContent(
            JsonConvert.SerializeObject(command),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await _client.PostAsync("/api/vehicle/CalculatePrice", content);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}