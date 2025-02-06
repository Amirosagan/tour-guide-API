using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Session
{
    [Key]
    public Guid Id { get; set; }
    public required Guid TourId { get; set; }

    [Required]
    public Tour Tour { get; set; } = null!;
    public int CurrentCapacity { get; set; } = 0;
    public required uint MaxCapacity { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
}
