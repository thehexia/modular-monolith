// namespace PayYourChart.Module.Patient.UnitTests;

// public class GetBillTests(EndpointsTestFixture app)  : TestBase<EndpointsTestFixture>
// {
//     readonly EndpointsTestFixture _app = app;

//     [Fact]
//     public async void GetBillById_returns_dto_if_in_database()
//     {
//         // Arrange
//         const long billId = 1;
//         Bill bill = _app.f
//             .Build<Bill>()
//             .With(b => b.Id, billId)
//             .Create();
//         _app.BillRepository.GetAsync(billId).Returns(bill);

//         // Act
//         TestResult<BillDto> result = await _app.Client.GETAsync<GetBill, GetBillRequest, BillDto>(new(1));

//         // Assert
//         result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
//         _app.Mapper.Get().Map<BillDto>(bill).Should().BeEquivalentTo(result.Result); 
//     }
// }
