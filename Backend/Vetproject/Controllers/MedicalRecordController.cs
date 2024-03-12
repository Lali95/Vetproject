using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Vetproject.Model;

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordController : ControllerBase
{
    private readonly MedicalRecordDbContext _dbContext;

    public MedicalRecordController(MedicalRecordDbContext dbContext)
    {
        _dbContext = dbContext;
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
        // Save the filledMedicalRecord to the database
        _dbContext.MedicalRecords.Add(filledMedicalRecord);
        _dbContext.SaveChanges();

        // Return a success message or status code
        return Ok("Medical Record saved successfully.");
    }

    [HttpGet("getAllMedicalRecords")]
    public ActionResult<IEnumerable<MedicalRecord>> GetAllMedicalRecords()
    {
        var allMedicalRecords = _dbContext.MedicalRecords.ToList();
        return Ok(allMedicalRecords);
    }

    [HttpGet("searchMedicalRecords")]
    public ActionResult<IEnumerable<MedicalRecord>> SearchMedicalRecords([FromQuery] string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return BadRequest("Search term cannot be empty.");
        }

        var matchingRecords = _dbContext.MedicalRecords
            .Where(record => record.OwnerName.Contains(searchTerm))
            .ToList();

        return Ok(matchingRecords);
    }
}