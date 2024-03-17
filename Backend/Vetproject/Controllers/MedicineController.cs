using Microsoft.AspNetCore.Mvc;
using Vetproject.Model;
using Vetproject.Data.Repository;
using System;
using Vetproject.Service;

namespace Vetproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly ILoggerService _logger;

        public MedicineController(IMedicineRepository medicineRepository, ILoggerService logger)
        {
            _medicineRepository = medicineRepository;
            _logger = logger;
        }
    
        [HttpGet("getMedicines")]
        public IActionResult GetMedicines()
        {
            try
            {
                var medicines = _medicineRepository.GetAllMedicines();
                return Ok(medicines);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Error with medicines");
            }
        }
    }
}