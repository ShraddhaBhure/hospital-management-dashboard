using System.ComponentModel.DataAnnotations;

namespace HS.Models
{
    public class VisitedPatient
    {
        [Key]
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientAddress { get; set; }
        public DateTime AdmitDate { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public int VisitYear { get; set; }
        public double AverageAge { get; internal set; }
        public int PatientCount { get; internal set; }
    }
   
   
}
