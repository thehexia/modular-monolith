// using NSubstitute.ReturnsExtensions;

using NSubstitute.ReturnsExtensions;

namespace PayYourChart.Module.Patient.UnitTests;


public class GetPatientTests(PatientsEndpointsTestFixture app)  : IClassFixture<PatientsEndpointsTestFixture>
{
    readonly PatientsEndpointsTestFixture _app = app;
    
    [Fact]
    public async Task GetPatientById_returns_dto_if_in_database()
    {
        // Arrange
        const long patientId = 1;
        Patient patient = _app.f
            .Build<Patient>()
            .With(p => p.Id, patientId)
            .Create();
            
        _app.PatientRepository.GetAsync(patientId).Returns(patient);

        // Act
        TestResult<PatientDto> result = await _app.Client.GETAsync<GetPatient, GetPatientByIdRequest, PatientDto>(new(patientId));

        // Assert
        result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        _app.Mapper.Get().Map<PatientDto>(patient).Should().BeEquivalentTo(result.Result); 
    }


    [Fact]
    public async void GetPatientById_returns_no_content_if_not_in_database()
    {
        // Arrange
        const long patientId = 2;
        _app.PatientRepository.GetAsync(Arg.Is(patientId)).ReturnsNull();

        // Act
        TestResult<PatientDto> result = await _app.Client.GETAsync<GetPatient, GetPatientByIdRequest, PatientDto>(new(patientId));

        // Assert
        result.Response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
