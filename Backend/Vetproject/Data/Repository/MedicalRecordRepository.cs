using Vetproject.Model;

namespace Vetproject.Data.Repository;

public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly MedicalRecordDbContext _dbContext;

    public MedicalRecordRepository(MedicalRecordDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public MedicalRecord Get(int id)
    {
        return _dbContext.MedicalRecords.FirstOrDefault(record => record.Id == id);
    }

    public IEnumerable<MedicalRecord> GetAll()
    {
        return _dbContext.MedicalRecords.ToList();
    }

    public void Add(MedicalRecord medicalRecord)
    {
        _dbContext.MedicalRecords.Add(medicalRecord);
        _dbContext.SaveChanges();
    }

    public void Update(MedicalRecord medicalRecord)
    {
        _dbContext.MedicalRecords.Update(medicalRecord);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var recordToDelete = _dbContext.MedicalRecords.FirstOrDefault(record => record.Id == id);
        if (recordToDelete != null)
        {
            _dbContext.MedicalRecords.Remove(recordToDelete);
            _dbContext.SaveChanges();
        }
    }

    public IEnumerable<MedicalRecord> Search(string searchTerm)
    {
        return _dbContext.MedicalRecords.Where(record => record.OwnerName.Contains(searchTerm)).ToList();
    }
}
