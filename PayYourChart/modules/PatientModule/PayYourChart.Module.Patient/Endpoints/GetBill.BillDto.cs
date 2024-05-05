namespace PayYourChart.Module.Patient;

public class BillDto
{
    public long Id { get; init; }
    
    public long PatientId { get; set; }
    
    public DateTime DueDate { get; set; }

    public required string Provider { get; set; }

    public required List<LineItemDto> LineItems { get; init; }

    public decimal GrossTotal { get; init; }
}
