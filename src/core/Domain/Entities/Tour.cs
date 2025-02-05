using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Domain.Entities;

public class Tour
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }

    [MaxLength(200)]
    public required string Location { get; set; }
    public string GoogleMapsLocation =>
        $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(Location)}";
    public required uint CurrentCapacity { get; set; } = 0;
    public required uint MaxCapacity { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public List<Review> Reviews { get; set; } = new List<Review>();
    public List<Booking> Bookings { get; set; } = new List<Booking>();
    public List<Session> Sessions { get; set; } = new List<Session>();
    public List<TourCategory> Categories { get; set; } = new List<TourCategory>();
}
