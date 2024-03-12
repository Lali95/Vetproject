using Microsoft.EntityFrameworkCore;
using Vetproject.Model;

public class MedicalRecordDbContext : DbContext
{
    public DbSet<MedicalRecord> MedicalRecords { get; set; }

    public MedicalRecordDbContext(DbContextOptions<MedicalRecordDbContext> options) : base(options)
    {
    }
}