using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Domain.Models;

namespace Shipping.Api.Infrastructure.Data;

public class ShippingContext(DbContextOptions<ShippingContext> options):IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{
    public virtual DbSet<Branch> Branches { get; set; }
    public virtual DbSet<CitySetting> CitySettings { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderReport> OrderReports { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Region> Regions { get; set; }
    public virtual DbSet<ShippingType> ShippingTypes { get; set; }
    public virtual DbSet<WeightSetting> WeightSettings { get; set; }
    public virtual DbSet<CourierReport> CourierReports { get; set; }
    public virtual DbSet<SpecialCityCost> SpecialCityCosts { get; set; }
    public virtual DbSet<SpecialCourierRegion> GetSpecialCourierRegions { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>()
            .Property(a => a.CanceledOrder)
            .HasPrecision(18,2);
        builder.Entity<ApplicationUser>()
            .Property(a => a.DeductionCompanyFromOrder)
        .HasPrecision(18,2);

        builder.Entity<ApplicationUser>()
            .Property(a => a.PickupPrice)
        .HasPrecision(18,2);

        builder.Entity<CitySetting>()
            .Property(c => c.StandardShippingCost)
            .HasPrecision(18,2);

        builder.Entity<CitySetting>()
            .Property(c => c.pickupShippingCost)
            .HasPrecision(18,2);

        builder.Entity<Order>()
            .Property(o => o.OrderCost)
            .HasPrecision(18,2);

        builder.Entity<Order>()
            .Property(o => o.TotalWeight)
            .HasPrecision(18,2);

        builder.Entity<Product>()
            .Property(p => p.Weight)
            .HasPrecision(18,2);

        builder.Entity<ShippingType>()
            .Property(s => s.BaseCost)
            .HasPrecision(18,2);

        builder.Entity<WeightSetting>()
            .Property(w => w.CostPerKg)
            .HasPrecision(18,2);

        builder.Entity<WeightSetting>()
            .Property(w => w.MaxWeight)
            .HasPrecision(18,2);

        builder.Entity<WeightSetting>()
            .Property(w => w.MinWeight)
            .HasPrecision(18,2);
        builder.Entity<SpecialCityCost>()
            .Property(s => s.Price)
            .HasPrecision(18,2);
        base.OnModelCreating(builder);
    }
}
