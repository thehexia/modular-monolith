namespace PayYourChart.Module.Patient;

internal class Patient
{
    public long Id { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string SSN { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public List<Bill> Bills { get; init; } = new();
}
