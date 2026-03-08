using Microsoft.EntityFrameworkCore;

namespace CafeWithCats.Models;

public class CafeContext : DbContext
{
    public CafeContext(DbContextOptions<CafeContext> options) : base(options) { }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Cafe> Cafes { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Drink> Drinks { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservationClient> ReservationClients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReservationClient>().HasKey(rc => new { rc.ReservationId, rc.ClientId });
    }
}