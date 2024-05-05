using System.ComponentModel.DataAnnotations;

namespace PayYourChart.Module.Patient;

internal class Patient
{
    public long Id { get; init; }
    
    [MaxLength(32)]
    public required string FirstName { get; set; }

    [MaxLength(32)]
    public required string LastName { get; set; }

    [MaxLength(11)] // 11 because of dashes 123-45-6789
    public required string SSN { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    public List<Bill> Bills { get; init; } = new();
}
