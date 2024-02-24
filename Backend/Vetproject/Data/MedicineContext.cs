using Microsoft.EntityFrameworkCore;
using Vetproject.Model;

namespace Vetproject.Data;

public class MedicineContext : DbContext
{
    public DbSet<Medicine> Medicines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=vetproject;User Id=sa;Password=VetProject2024;Encrypt=false;");
    }
}
