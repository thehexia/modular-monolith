using PayYourChart.Module.Tests.Common;

namespace PayYourChart.Module.Patient.UnitTests;

public class UpdateLineItemHandlerTests(PatientsEndpointsTestFixture app)  : IClassFixture<PatientsEndpointsTestFixture>
{
    readonly PatientsEndpointsTestFixture _app = app;

    [Fact]
    public async void UpdateLineItemHandler_updates_existing_line_item_and_if_quantity_greater_than_or_equal_1() 
    {
        // Arrange
        const long lineItemId = 1;
        DateTime dueDate = DateTime.Now;
        const string provider = "Dr. Smith";
        short quantity = (short)_app.f.CreateInt(1, 100);

        UpdateLineItemCommand command = new(lineItemId, dueDate, provider, quantity);
        var sut = _app.f.Create<UpdateLineItemHandler>();

        // Act
        await sut.Handle(command, CancellationToken.None);

        // Assert
        await _app.BillRepository.Received(1).UpdateLineItemAsync(command.LineItemId, command.DateOfService, command.Provider, command.Quantity);
    }


    [Fact]
    public async void UpdateLineItemHandler_deletes_existing_line_item_if_quantity_is_0() 
    {
        // Arrange
        const long lineItemId = 1;
        DateTime dueDate = DateTime.Now;
        const string provider = "Dr. Smith";
        short quantity = 0;

        UpdateLineItemCommand command = new(lineItemId, dueDate, provider, quantity);
        var sut = _app.f.Create<UpdateLineItemHandler>();

        // Act
        await sut.Handle(command, CancellationToken.None);

        // Assert
        await _app.BillRepository.Received(1).DeleteLineItemAsync(command.LineItemId);
    }
}
