using System;
using System.Collections.Generic;

namespace Vetproject.Model
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string VetName { get; set; }
        public string OwnerName { get; set; }
        public string HorseName { get; set; }
        public string Place { get; set; }
        public string MedicalTreatment { get; set; }
        public DateTime CreatedAt { get; set; } 
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
    }

 
}