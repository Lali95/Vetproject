using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        try
        {
            var medicalRecord = _medicalRecordRepository.Get(id);
            if (medicalRecord == null)
            {
                return NotFound("Medical record not found.");
            }

            return Ok(medicalRecord);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpGet("getAllMedicalRecords")]
    public ActionResult<IEnumerable<MedicalRecord>> GetAllMedicalRecords()
    {
        try
        {
            var allMedicalRecords = _medicalRecordRepository.GetAll()
                .OrderByDescending(record => record.CreatedAt)
                .ToList();
            return Ok(allMedicalRecords);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpPost("saveMedicalRecord")]
    public ActionResult SaveMedicalRecord([FromBody] MedicalRecord filledMedicalRecord)
    {
        try
        {
            _medicalRecordRepository.Add(filledMedicalRecord);
            return Ok("Medical Record saved successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpPut("updateMedicalRecord/{id}")]
    public ActionResult UpdateMedicalRecord(int id, [FromBody] MedicalRecord medicalRecord)
    {
        try
        {
            var existingRecord = _medicalRecordRepository.Get(id);
            if (existingRecord == null)
            {
                return NotFound("Medical record not found.");
            }

         
            _medicalRecordRepository.Update(medicalRecord);
            return Ok("Medical Record updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpDelete("deleteMedicalRecord/{id}")]
    public ActionResult DeleteMedicalRecord(int id)
    {
        try
        {
            var existingRecord = _medicalRecordRepository.Get(id);
            if (existingRecord == null)
            {
                return NotFound("Medical record not found.");
            }

            _medicalRecordRepository.Delete(id);
            return Ok("Medical Record deleted successfully.");
        }
        catch (Exception ex)
        {
           
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpGet("searchMedicalRecords")]
    public ActionResult<IEnumerable<MedicalRecord>> SearchMedicalRecords([FromQuery] string searchTerm)
    {
        try
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var matchingRecords = _medicalRecordRepository.Search(searchTerm);
            return Ok(matchingRecords);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}
