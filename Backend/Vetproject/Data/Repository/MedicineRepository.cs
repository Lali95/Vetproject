using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Medicine> GetAllMedicines()
        {
            return _context.Medicines.ToList();
        }
    }
}