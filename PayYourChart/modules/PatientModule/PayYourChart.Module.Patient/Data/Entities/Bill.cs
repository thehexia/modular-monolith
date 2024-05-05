using System.ComponentModel.DataAnnotations;

namespace PayYourChart.Module.Patient;

internal class Bill
{
    public long Id { get; init; }
    
    public long PatientId { get; init; }
    
    public DateTime DueDate { get; init; }

    [StringLength(128)]
    public required string Provider { get; init; }

    public List<LineItem> LineItems { get; set; } = new();
}