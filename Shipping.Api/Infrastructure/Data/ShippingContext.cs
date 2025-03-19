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
    }
