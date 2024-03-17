using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

    [HttpGet("getEmptyMedicalRecord")]
    public ActionResult<MedicalRecord> GetEmptyMedicalRecord()
    {
        var emptyMedicalRecord = new MedicalRecord();
        return Ok(emptyMedicalRecord);
    }

    [HttpPost("saveMedicalRecord")]
    public ActionResult SaveMedicalRecord([FromBody] MedicalRecord filledMedicalRecord)
    {
        _medicalRecordRepository.Add(filledMedicalRecord);
        return Ok("Medical Record saved successfully.");
    }

    [HttpGet("getAllMedicalRecords")]
    public ActionResult<IEnumerable<MedicalRecord>> GetAllMedicalRecords()
    {
        var allMedicalRecords = _medicalRecordRepository.GetAll()
            .OrderByDescending(record => record.CreatedAt) // Order by creation date in descending order
            .ToList();
        return Ok(allMedicalRecords);
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


    [HttpGet("searchMedicalRecords")]
    public ActionResult<IEnumerable<MedicalRecord>> SearchMedicalRecords([FromQuery] string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return BadRequest("Search term cannot be empty.");
        }

        var searchTermLower = searchTerm.ToLower(); // Convert search term to lowercase

        var matchingRecords = _medicalRecordRepository.GetAll()
            .Where(record =>
                record.OwnerName.ToLower().Contains(searchTermLower) || // Convert strings to lowercase for comparison
                record.HorseName.ToLower().Contains(searchTermLower) ||
                record.VetName.ToLower().Contains(searchTermLower))
            .ToList();

        return Ok(matchingRecords);
    }





}