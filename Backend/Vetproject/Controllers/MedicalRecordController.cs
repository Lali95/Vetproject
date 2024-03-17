using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Vetproject.Data.Repository;
using Vetproject.Model;

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    public MedicalRecordController(IMedicalRecordRepository medicalRecordRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
    }

    [HttpGet("{id}")]
    public ActionResult<MedicalRecord> GetMedicalRecord(int id)
    {
        var medicalRecord = _medicalRecordRepository.Get(id);
        if (medicalRecord == null)
        {
            return NotFound("Medical record not found.");
        }

        return Ok(medicalRecord);
    }

    [HttpGet("getAllMedicalRecords")]
    public ActionResult<IEnumerable<MedicalRecord>> GetAllMedicalRecords()
    {
        var allMedicalRecords = _medicalRecordRepository.GetAll()
            .OrderByDescending(record => record.CreatedAt)
            .ToList();
        return Ok(allMedicalRecords);
    }

    [HttpPost("saveMedicalRecord")]
    public ActionResult SaveMedicalRecord([FromBody] MedicalRecord filledMedicalRecord)
    {
        _medicalRecordRepository.Add(filledMedicalRecord);
        return Ok("Medical Record saved successfully.");
    }

    [HttpPut("updateMedicalRecord/{id}")]
    public ActionResult UpdateMedicalRecord(int id, [FromBody] MedicalRecord medicalRecord)
    {
        var existingRecord = _medicalRecordRepository.Get(id);
        if (existingRecord == null)
        {
            return NotFound("Medical record not found.");
        }

        medicalRecord.Id = id;
        _medicalRecordRepository.Update(medicalRecord);
        return Ok("Medical Record updated successfully.");
    }

    [HttpDelete("deleteMedicalRecord/{id}")]
    public ActionResult DeleteMedicalRecord(int id)
    {
        var existingRecord = _medicalRecordRepository.Get(id);
        if (existingRecord == null)
        {
            return NotFound("Medical record not found.");
        }

        _medicalRecordRepository.Delete(id);
        return Ok("Medical Record deleted successfully.");
    }

    [HttpGet("searchMedicalRecords")]
    public ActionResult<IEnumerable<MedicalRecord>> SearchMedicalRecords([FromQuery] string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return BadRequest("Search term cannot be empty.");
        }

        var matchingRecords = _medicalRecordRepository.Search(searchTerm);
        return Ok(matchingRecords);
    }
}
