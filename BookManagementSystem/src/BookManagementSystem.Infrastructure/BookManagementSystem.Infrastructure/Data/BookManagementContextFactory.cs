using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookManagementSystem.Infrastructure.Data;

public class BookManagementContextFactory : IDesignTimeDbContextFactory<BookManagementContext>
{
    public BookManagementContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookManagementContext>();

        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ??
            Environment.GetEnvironmentVariable("DefaultConnection") ??
            "Host=localhost;Port=5432;Database=BookManagementDb;Username=postgres;Password=postgres";

        optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("BookManagementSystem.Infrastructure"));

        return new BookManagementContext(optionsBuilder.Options);
    }
}
