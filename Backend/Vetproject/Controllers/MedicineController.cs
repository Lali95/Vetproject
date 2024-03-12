using Microsoft.AspNetCore.Mvc;
using Vetproject.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Vetproject.Data;

namespace Vetproject.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MedicineController : ControllerBase
{
    private readonly MedicineContext _context;

    public MedicineController(MedicineContext context)
    {
        _context = context;
    }
    
    [HttpGet("getMedicines")]
    public IActionResult GetMedicines()
    {
        try
        {
            var items = _context.Medicines.ToList();
           
        
            return Ok(items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Error with medicines");
        }
    }

    
}