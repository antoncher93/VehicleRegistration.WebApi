using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        this.Database.EnsureCreated();

        this.AddInitialData();
    }

    private void AddInitialData()
    {
        if (!this.Brands.Any())
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
            this.Bodies.AddRange(bodies);
            
            
            var engines = new Engine[]
            {
                new Engine()
                {
                    HorsePower = 100.0,
                    Type = Types.EngineType.Gasoline,
                    Volume = 1.6,
                },
                new Engine()
                {
                    HorsePower = 125.0,
                    Type = Types.EngineType.Gasoline,
                    Volume = 1.8,
                },
                new Engine()
                {
                    HorsePower = 145.0,
                    Type = Types.EngineType.Gasoline,
                    Volume = 2.0,
                }
            };
            this.Engines.AddRange(engines);

            var models = new Model[]
            {
                new Model()
                {
                    ModelName = "Focus II",
                    Engines = engines.ToList(),
                    Bodies = bodies.ToList(),
                },
            };
            this.Models.AddRange(models);

            var brand = new Brand()
            {
                Name = "Ford",
                Models = models.ToList(),
            };
            this.Brands.AddRange(brand);
            this.SaveChanges();
        }
    }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Types.Model> Models { get; set; }
    public DbSet<Body> Bodies { get; set; }
    public DbSet<Engine> Engines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brands");
            entity.HasKey(brand => brand.Id);
            entity.HasMany(brand => brand.Models)
                .WithOne(model => model.Brand);
            entity.Property(brand => brand.Name).IsRequired();
        });

        modelBuilder.Entity<Types.Model>(entity =>
        {
            entity.ToTable("Models");
            entity.HasKey(model => model.Id);
            entity.Property(model => model.ModelName).IsRequired();
            entity.HasOne<Brand>(model => model.Brand)
                .WithMany(brand => brand.Models)
                .HasForeignKey("BrandId")
                .IsRequired(true);

            entity.HasMany(model => model.Bodies)
                .WithMany(body => body.Models)
                .UsingEntity(e => e.ToTable("ModelBody"));
        });

        modelBuilder.Entity<Engine>(entity =>
        {
            entity
                .HasMany(engine => engine.Models)
                .WithMany(model => model.Engines)
                .UsingEntity(e => e.ToTable("ModelEngine"));
        });

        modelBuilder.Entity<Body>(entity =>
        {
            entity.HasIndex(body => body.Name)
                .IsUnique(true);
        });
    }
}