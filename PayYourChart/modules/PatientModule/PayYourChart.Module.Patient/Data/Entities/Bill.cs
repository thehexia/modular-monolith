namespace PayYourChart.Module.Patient;

internal class Bill
{
    public long Id { get; init; }
    
    public long PatientId { get; set; }
    
    public DateTime DueDate { get; set; }

    public required string Provider { get; set; }

    public List<LineItem> LineItems { get; init; } = new();
}