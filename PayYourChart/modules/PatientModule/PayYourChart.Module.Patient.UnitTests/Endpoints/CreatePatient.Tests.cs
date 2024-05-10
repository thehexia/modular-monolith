namespace PayYourChart.Module.Patient.UnitTests;

public class CreatePatientTests(PatientsEndpointsTestFixture app)  : TestBase<PatientsEndpointsTestFixture>
{
    readonly PatientsEndpointsTestFixture _app = app;

    [Fact]
    public async void CreatePatient_requires_first_name()
    {
        // Arrange
        var request = new CreatePatientRequest(null, "Doe", "123-45-6789", DateTime.Now);

        // Act
        var (response, result) = await _app.Client.POSTAsync<CreatePatient, CreatePatientRequest, ErrorResponse>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Errors.Count.Should().Be(1);
        result.Errors["firstName"].Contains("First name is required.").Should().BeTrue();
    }


    [Fact]
    public async void CreatePatient_first_name_max_length()
    {
        // Arrange
        var request = new CreatePatientRequest(new string('a', 65), "Doe", "123-45-6789", DateTime.Now);

        // Act
        var (response, result) = await _app.Client.POSTAsync<CreatePatient, CreatePatientRequest, ErrorResponse>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Errors.Count.Should().Be(1);
        result.Errors["firstName"].Contains("First name can only be a maximum of 64 characters.").Should().BeTrue();
    }


    [Fact]
    public async void CreatePatient_requires_last_name()
    {
        // Arrange
        var request = new CreatePatientRequest("John", null, "123-45-6789", DateTime.Now);

        // Act
        var (response, result) = await _app.Client.POSTAsync<CreatePatient, CreatePatientRequest, ErrorResponse>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Errors.Count.Should().Be(1);
        result.Errors["lastName"].Contains("Last name is required.").Should().BeTrue();
    }


    [Fact]
    public async void CreatePatient_last_name_max_length()
    {
        // Arrange
        var request = new CreatePatientRequest("John", new string('a', 65), "123-45-6789", DateTime.Now);

        // Act
        var (response, result) = await _app.Client.POSTAsync<CreatePatient, CreatePatientRequest, ErrorResponse>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Errors.Count.Should().Be(1);
        result.Errors["lastName"].Contains("Last name can only be a maximum of 64 characters.").Should().BeTrue();
    }


    [Fact]
    public async void CreatePatient_requires_ssn()
    {
        // Arrange
        var request = new CreatePatientRequest("John", "Doe", null, DateTime.Now);

        // Act
        var (response, result) = await _app.Client.POSTAsync<CreatePatient, CreatePatientRequest, ErrorResponse>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Errors.Count.Should().Be(1);
        result.Errors["ssn"].Contains("SSN is required.").Should().BeTrue();
    }


    [Fact]
    public async void CreatePatient_validates_ssn_format()
    {
        // Arrange
        var request = new CreatePatientRequest("John", "Doe", "123456789", DateTime.Now);

        // Act
        var (response, result) = await _app.Client.POSTAsync<CreatePatient, CreatePatientRequest, ErrorResponse>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Errors.Count.Should().Be(1);
        result.Errors["ssn"].Contains("SSN must be of format XXX-XX-XXXX.").Should().BeTrue();
    }
}