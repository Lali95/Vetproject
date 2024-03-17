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
        var allMedicalRecords = _medicalRecordRepository.GetAll();
        return Ok(allMedicalRecords);
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