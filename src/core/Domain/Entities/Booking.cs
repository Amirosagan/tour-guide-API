using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace Domain.Entities;

public class Booking
{
    [Key]
    public Guid Id { get; set; }
    public required Guid TourId { get; set; }

    [Required]
    public Tour Tour { get; set; } = null!;

    [ForeignKey("UserId")]
    public required string UserId { get; set; }

    [Required]
    public User User { get; set; } = null!;
    public required DateTime Date { get; set; }
    public required uint Seats { get; set; } = 1;
}
