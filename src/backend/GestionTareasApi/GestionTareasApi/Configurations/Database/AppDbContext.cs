using GestionTareasApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionTareasApi.Configurations.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).UseIdentityColumn();

            entity.Property(u => u.Name).HasMaxLength(50).IsRequired();

            entity.Property(u => u.Email).HasMaxLength(50).IsRequired();
            entity.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<TaskItem>(entity =>
        {
           
        });
    }
}
