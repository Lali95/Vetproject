using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Vetproject.Data;

public class UsersContext : IdentityUserContext<IdentityUser>
{
    
    public UsersContext (DbContextOptions<UsersContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // It would be a good idea to move the connection string to user secrets
        options.UseSqlServer("Server=localhost,1433;Database=vetproject;Persist Security Info=False;User ID=SA;Password=VetProject2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=300;Initial Catalog=vetproject");

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
