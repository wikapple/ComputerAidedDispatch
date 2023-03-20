using ComputerAidedDispatchAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComputerAidedDispatchAPI.Data;

public class ComputerAidedDispatchContext: IdentityDbContext<ApplicationUser>
{
	public ComputerAidedDispatchContext(DbContextOptions<ComputerAidedDispatchContext> options):
		base(options)
	{
        this.Database.EnsureCreated();
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Dispatcher> Dispatchers { get; set; }
    public DbSet<CallForService> CallsForService { get; set; }
    public DbSet<CallComment> CallComments { get; set; }

 


    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CallForService>()
            .HasMany(c => c.CallComments)
            .WithOne(p => p.CallForService)
            .HasForeignKey(p => p.CallId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CallForService>()
            .HasMany(c => c.Units)
            .WithOne(u => u.CallForService)
            .HasForeignKey(u => u.CallNumber)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ApplicationUser>()
             .HasMany(x => x.CallComments)
             .WithOne(cc => cc.ApplicationUser)
             .HasForeignKey(cc => cc.userId);

        modelBuilder.Entity<Unit>()
            .HasOne(u => u.UserInfo)
            .WithOne(ui => ui.ThisUnit)
            .HasForeignKey<Unit>(u => u.UserId);

        modelBuilder.Entity<Dispatcher>()
            .HasOne(d => d.UserInfo)
            .WithOne(ui => ui.ThisDispatcher)
            .HasForeignKey<Dispatcher>(d => d.UserId);


        /*
        modelBuilder.Entity<Unit>()
            .HasData(
                new Unit
                {
                    Id = 1,
                    UnitNumber = "PD-10",
                    Status = "Available",
                },
                new Unit
                {
                    Id = 2,
                    UnitNumber = "PD-14",
                    Status = "Available",
                },
                new Unit
                {
                    Id = 3,
                    UnitNumber = "PD-24",
                    Status = "Available",
                },
                new Unit
                {
                    Id = 4,
                    UnitNumber = "PD-55",
                    Status = "Available",
                }
            ) ;
        */

    }
}
