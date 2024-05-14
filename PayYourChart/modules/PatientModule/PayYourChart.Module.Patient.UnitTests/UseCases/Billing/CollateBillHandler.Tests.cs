using FluentResults;

namespace PayYourChart.Module.Patient.UnitTests;

public class CollateBillHandlerTests(PatientsEndpointsTestFixture app)  : IClassFixture<PatientsEndpointsTestFixture>
{
    readonly PatientsEndpointsTestFixture _app = app;

    [Fact]
    public async void CollateBillHandler_collates_line_items_and_returns_final_bill_with_sum() 
    {
        // Arrange
        const long billId = 1;
        const long patientId = 2;
        DateTime dueDate = DateTime.Now;
        const string provider = "Dr. Smith";

        CollateBillQuery query = new(billId);
        List<LineItem> mockLineItems = _app.f.CreateMany<LineItem>(3).ToList();

        var bill = new Bill
        {
            Id = billId,
            PatientId = patientId,
            DueDate = dueDate,
            Provider = provider,
            LineItems = mockLineItems
        };

        _app.BillRepository.GetAsync(billId).Returns(bill);

        var sut = _app.f.Create<CollateBillHandler>();

        // Act
        Result<CollateBillResponse> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(bill.Id);
        result.Value.PatientId.Should().Be(bill.PatientId);
        result.Value.DueDate.Should().Be(bill.DueDate);
        result.Value.Provider.Should().Be(bill.Provider);
        result.Value.LineItems.Should().BeEquivalentTo(bill.LineItems);
        result.Value.GrossTotal.Should().Be(bill.LineItems.Sum(l => l.Price * l.Quantity));
    }


    [Fact]
    public async void CollateBillHandler_returns_fail_if_bill_not_found() 
    {
        // Arrange
        const long billId = 1;

        CollateBillQuery query = new(billId);

        _app.BillRepository.GetAsync(billId).Returns((Bill?)null);

        var sut = _app.f.Create<CollateBillHandler>();

        // Act
        Result<CollateBillResponse> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be($"No bill with id {billId} found.");
    }
}
