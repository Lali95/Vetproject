using System;
using Vetproject.Enum;
using Vetproject.Model;

namespace Vetproject.Model
{
    
    public class MedicalRecord
    {
        public int Id { get; init; }
        public string VetName { get; set; }
        public string OwnerName { get; set; }
        public string HorseName { get; set; }
        public string Place { get; set; }
        public string Medication { get; set; } 
        public string MedicalIntervention { get; set; }
    }
    
}