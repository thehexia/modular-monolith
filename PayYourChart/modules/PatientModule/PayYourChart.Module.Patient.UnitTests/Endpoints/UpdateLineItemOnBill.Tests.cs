using FastEndpoints;
using NSubstitute.ReturnsExtensions;

namespace PayYourChart.Module.Patient.UnitTests;

public class UpdateLineItemOnBillTests(PatientsEndpointsTestFixture app) : IClassFixture<PatientsEndpointsTestFixture>
{
    readonly PatientsEndpointsTestFixture _app = app;
    
    [Fact]
    public async Task UpdateLineItemOnBill_returns_ok_if_valid_request()
    {
        // Arrange
        const long lineItemId = 1;
        UpdateLineItemRequest request = new(lineItemId, DateTime.Now, "Provider", 1);
        
        // Act
        var response = await _app.Client.PUTAsync<UpdateLineItemOnBill, UpdateLineItemRequest>(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateLineItemOnBill_returns_bad_request_if_bill_id_is_invalid()
    {
        // Arrange
        const long lineItemId = 1;
        UpdateLineItemRequest request = new(lineItemId, DateTime.Now, "", -1);
        
        // Act
        var response = await _app.Client.PUTAsync<UpdateLineItemOnBill, UpdateLineItemRequest>(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateLineItemOnBill_returns_bad_request_if_provider_is_null()
    {
        // Arrange
        const long lineItemId = 1;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        UpdateLineItemRequest request = new(lineItemId, DateTime.Now, null, 1);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // Act
        var response = await _app.Client.PUTAsync<UpdateLineItemOnBill, UpdateLineItemRequest>(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}