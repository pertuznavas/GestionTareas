using GestionTareasApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionTareasApi.Configurations.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
           
        });

        modelBuilder.Entity<TaskItem>(entity =>
        {

        });
    }
}
