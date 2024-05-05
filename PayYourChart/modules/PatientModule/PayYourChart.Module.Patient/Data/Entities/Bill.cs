namespace PayYourChart.Module.Patient;

internal class Bill
{
    public long Id { get; init; }
    
    public long PatientId { get; init; }
    
    public DateTime DueDate { get; init; }

    public required string Provider { get; init; }

    public List<LineItem> LineItems { get; set; } = new();
}