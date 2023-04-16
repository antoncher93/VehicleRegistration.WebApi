using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        this.Database.EnsureCreated();

        this.AddInitialData();
    }

    

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Body> Bodies { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<EngineType> EngineTypes { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Registration> Registrations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brands");
            entity.HasKey(brand => brand.Id);
            entity
                .HasMany(brand => brand.Models)
                .WithOne(model => model.Brand)
                .HasForeignKey(model => model.BrandId);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicles");
            entity
                .HasOne(v => v.Model)
                .WithMany(model => model.Vehicles)
                .HasForeignKey(vehicle => vehicle.ModelId);

            entity
                .HasOne(vehicle => vehicle.Engine)
                .WithOne(engine => engine.Vehicle)
                .HasForeignKey<Vehicle>(v => v.EngineId);

            entity
                .HasOne(vehicle => vehicle.Body)
                .WithMany(body => body.Vehicles)
                .HasForeignKey(v => v.BodyId);
        });

        modelBuilder.Entity<Engine>(entity =>
        {
            entity.ToTable("Engines");
            entity
                .HasOne(e => e.Type)
                .WithMany(type => type.Engines)
                .HasForeignKey(e => e.EngineTypeId);
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.ToTable("Owners");
            entity.HasMany(owner => owner.Registrations)
                .WithOne(registration => registration.Owner)
                .HasForeignKey(registration => registration.OwnerId);
        });

        modelBuilder.Entity<EngineType>(entity =>
        {
            entity.ToTable("EngineTypes");
        });
    }
    
    private void AddInitialData()
    {
        if (!Bodies.Any())
        {
            var bodies = new Body[]
            {
                new Body()
                {
                    Name = "Седан"
                },
                new Body()
                {
                    Name = "Купе"
                },
                new Body()
                {
                    Name = "Хетчбек"
                },
                new Body()
                {
                    Name = "Универсал"
                }
            };
            Bodies.AddRange(bodies);
            SaveChanges();
        }

        if (!this.Brands.Any())
        {
            var brand = new Brand()
            {
                Name = "Ford",
                Models = new List<Model>()
                {
                    new Model()
                    {
                        ModelName = "Focus",
                    }
                }
            };
            Brands.Add(brand);
            SaveChanges();
        }

        if (!this.EngineTypes.Any())
        {
            this.EngineTypes.Add(new EngineType()
            {
                Name = "Gasoline"
            });
        }

        SaveChanges();
    }
}