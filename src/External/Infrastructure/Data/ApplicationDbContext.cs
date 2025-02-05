using Domain.Entities;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<TourGuide> TourGuides { get; set; }
    public DbSet<NormalUser> NormalUsers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourCategory> TourCategories { get; set; }
}
