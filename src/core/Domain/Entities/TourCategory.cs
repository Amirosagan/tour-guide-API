using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class TourCategory
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
    public List<Tour> Tours { get; set; } = new List<Tour>();
}
