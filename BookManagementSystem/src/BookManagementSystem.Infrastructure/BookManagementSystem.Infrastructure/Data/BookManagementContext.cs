using BookManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Infrastructure.Data;

/// <summary>
/// DbContext para a aplicação de Gerenciamento de Livros.
/// </summary>
public class BookManagementContext : DbContext
{
    public BookManagementContext(DbContextOptions<BookManagementContext> options) : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Genre Configuration
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
            entity.HasMany(e => e.Books).WithOne(b => b.Genre).HasForeignKey(b => b.GenreId).OnDelete(DeleteBehavior.Restrict);
        });

        // Author Configuration
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.Property(e => e.BirthDate).IsRequired();
            entity.Property(e => e.Biography).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
            entity.HasMany(e => e.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.Restrict);
        });

        // Book Configuration
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Isbn).IsRequired().HasMaxLength(13);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.PublishedDate).IsRequired();
            entity.Property(e => e.Pages).IsRequired();
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
            entity.HasOne(e => e.Author).WithMany(a => a.Books).HasForeignKey(e => e.AuthorId);
            entity.HasOne(e => e.Genre).WithMany(g => g.Books).HasForeignKey(e => e.GenreId);
        });
    }
}
