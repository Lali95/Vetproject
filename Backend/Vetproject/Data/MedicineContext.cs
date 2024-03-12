using Microsoft.EntityFrameworkCore;
using Vetproject.Model;

namespace Vetproject.Data
{
    public class MedicineContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }

        public MedicineContext(DbContextOptions<MedicineContext> options) : base(options)
        {
        }
    }
}