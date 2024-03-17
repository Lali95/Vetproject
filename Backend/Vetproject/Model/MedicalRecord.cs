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
        public string MedicalIntervention { get; set; }
        public DateTime CreatedAt { get; set; } // New property for storing creation date

        // Collection of medicines
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
    }

 
}