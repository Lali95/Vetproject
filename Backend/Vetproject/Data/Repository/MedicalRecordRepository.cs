using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vetproject.Model;

namespace Vetproject.Data.Repository
{
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
                // Detach any existing entity with the same key from the context
                var existingRecord = _dbContext.MedicalRecords.Local.FirstOrDefault(record => record.Id == medicalRecord.Id);
                if (existingRecord != null)
                {
                    _dbContext.Entry(existingRecord).State = EntityState.Detached;
                }

                
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
                return _dbContext.MedicalRecords
                    .Where(record => record.OwnerName.Contains(searchTerm) ||
                                     record.HorseName.Contains(searchTerm) ||
                                     record.VetName.Contains(searchTerm))
                    .ToList();
                
        }
    }
}
