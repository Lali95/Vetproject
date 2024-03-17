using Vetproject.Model;

namespace Vetproject.Data.Repository;

public interface IMedicalRecordRepository
{
    MedicalRecord Get(int id);
    IEnumerable<MedicalRecord> GetAll();
    void Add(MedicalRecord medicalRecord);
    void Update(MedicalRecord medicalRecord);
    void Delete(int id);
    IEnumerable<MedicalRecord> Search(string ownerName, string s, string searchTerm);
}