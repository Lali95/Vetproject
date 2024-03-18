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
            try
            {
                return _dbContext.MedicalRecords.FirstOrDefault(record => record.Id == id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while retrieving the medical record.", ex);
            }
        }

        public IEnumerable<MedicalRecord> GetAll()
        {
            try
            {
                return _dbContext.MedicalRecords.ToList();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while retrieving all medical records.", ex);
            }
        }

        public void Add(MedicalRecord medicalRecord)
        {
            try
            {
                _dbContext.MedicalRecords.Add(medicalRecord);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while adding the medical record.", ex);
            }
        }

        public void Update(MedicalRecord medicalRecord)
        {
            try
            {
                // Detach any existing entity with the same key from the context
                var existingRecord = _dbContext.MedicalRecords.Local.FirstOrDefault(record => record.Id == medicalRecord.Id);
                if (existingRecord != null)
                {
                    _dbContext.Entry(existingRecord).State = EntityState.Detached;
                }

                // Update the medical record
                _dbContext.MedicalRecords.Update(medicalRecord);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while updating the medical record.", ex);
            }
        }


        public void Delete(int id)
        {
            try
            {
                var recordToDelete = _dbContext.MedicalRecords.FirstOrDefault(record => record.Id == id);
                if (recordToDelete != null)
                {
                    _dbContext.MedicalRecords.Remove(recordToDelete);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while deleting the medical record.", ex);
            }
        }

        public IEnumerable<MedicalRecord> Search(string searchTerm)
        {
            try
            {
                return _dbContext.MedicalRecords
                    .Where(record => record.OwnerName.Contains(searchTerm) ||
                                     record.HorseName.Contains(searchTerm) ||
                                     record.VetName.Contains(searchTerm))
                    .ToList();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("Error occurred while searching for medical records.", ex);
            }
        }
    }
}
