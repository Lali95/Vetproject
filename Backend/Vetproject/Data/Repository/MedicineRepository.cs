using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vetproject.Model;

namespace Vetproject.Data.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly MedicineContext _context;

        public MedicineRepository(MedicineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicine>> GetAllMedicines()
        {
            return await _context.Medicines.ToListAsync();
        }
    }
}