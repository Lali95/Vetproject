using System.Collections.Generic;
using Vetproject.Model;

namespace Vetproject.Data.Repository
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetAllMedicines();
    }
}