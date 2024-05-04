namespace PayYourChart.Module.Patient;

internal class Patient
{
    public long Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string SSN { get; init; }
    public DateTime? DateOfBirth { get; init; }
}
