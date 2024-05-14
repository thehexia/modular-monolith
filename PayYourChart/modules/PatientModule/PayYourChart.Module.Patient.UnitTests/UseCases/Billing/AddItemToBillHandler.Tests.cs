using FluentResults;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Patient.UnitTests;

public class AddItemToBillHandlerTests(PatientsEndpointsTestFixture app)  : IClassFixture<PatientsEndpointsTestFixture>
{
    readonly PatientsEndpointsTestFixture _app = app;

    [Fact]
    public async void AddItemToBillHandler_adds_item_to_bill_and_returns_line_item() 
    {
        // Arrange
        const long billId = 1;
        const long itemId = 2;
        const string provider = "Dr. Smith";
        DateTime dateOfService = DateTime.Now;
        const short quantity = 1;
        const decimal price = 1000.10m;
        const string description = "Mock Service";

        AddItemToBillCommand command = new(billId, itemId, provider, dateOfService, quantity);
        LineItem expectedLineItem = new()
        {
            Id = 0,
            BillId = billId,
            ItemCatalogId = itemId,
            Provider = provider,
            DateOfService = dateOfService,
            Quantity = quantity,
            Price = price,
            Description = description,
        };

        var getItemQueryResponse = new GetItemResponse
        {
            Id = itemId,
            ItemCode = "MOCK",
            Price = price,
            Description = description,
            SpecialApprovalRequired = false
        };

        _app.Mediator.Send(Arg.Any<GetItemQuery>()).Returns(Result.Ok(getItemQueryResponse));
        
        var sut = _app.f.Create<AddItemToBillHandler>();
        
        // Act
        Result<LineItem> result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedLineItem);

        await _app.BillRepository.Received().AddLineItemAsync(Arg.Is<LineItem>(lineItem => 
            lineItem.Price == getItemQueryResponse.Price &&
            lineItem.Description == getItemQueryResponse.Description &&
            lineItem.ItemCatalogId == getItemQueryResponse.Id &&
            lineItem.DateOfService == command.DateOfService &&
            lineItem.BillId == command.BillId &&
            lineItem.Provider == command.Provider &&
            lineItem.Quantity == command.Quantity));  
    }


    [Fact]
    public async void AddItemToBillHandler_returns_failure_if_item_not_found() 
    {
        // Arrange
        const long billId = 1;
        const long itemId = 2;
        const string provider = "Dr. Smith";
        DateTime dateOfService = DateTime.Now;
        const short quantity = 1;

        AddItemToBillCommand command = new(billId, itemId, provider, dateOfService, quantity);
        _app.Mediator.Send(Arg.Any<GetItemQuery>()).Returns(Result.Fail("Item not found"));
        
        var sut = _app.f.Create<AddItemToBillHandler>();
        
        // Act
        Result<LineItem> result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("Could not get information about line item from catalog.");
    }

    
}
