using System.ComponentModel.DataAnnotations;
namespace HS.Models
{

    public class StudentData
    { 
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? DOB { get; set; }
        public int? SchoolId { get; set; }
        public string SchoolName { get; set; }
        public int? Class { get; set; }
        public string Section { get; set; }
        public string City { get; set; }
        public string Grade { get; set; }
        public byte[] StudentImage { get; set; }
    }
}
