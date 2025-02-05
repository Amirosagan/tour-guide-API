using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace Domain.Entities;

public class Review
{
    [Key]
    public int Id { get; set; }
    public required int Rating { get; set; }

    [MaxLength(400)]
    public string Comment { get; set; } = String.Empty;
    public required DateTime CreatedAt { get; set; }

    [ForeignKey("TourId")]
    public required Guid TourId { get; set; }

    [Required]
    public Tour Tour { get; set; } = null!;

    [ForeignKey("UserId")]
    public required string UserId { get; set; }

    [Required]
    public User User { get; set; } = null!;
}
