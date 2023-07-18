
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HS.Models
{
    //public class Patient
    //{
    //    //[Key]
    //    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    //public int Id { get; set; }

    //    //[Required]
    //    //[StringLength(255)]
    //    //public string Name { get; set; }

    //    //[Required]
    //    //[StringLength(15)]
    //    //public string Phone { get; set; }

    //    //[Required]
    //    //public int Gender { get; set; }

    //    //[Required]
    //    //[StringLength(255)]
    //    //public string HealthCondition { get; set; }

    //    //[ForeignKey("Doctor")]
    //    //public int DoctorId { get; set; }

    //    //[ForeignKey("Nurse")]
    //    //public int NurseId { get; set; }

    //    //[Required]
    //    //[DataType(DataType.DateTime)]
    //    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    //    //public DateTime Created { get; set; }

    //    //public virtual Doctor Doctor { get; set; }

    //    //public virtual Nurse Nurse { get; set; }
    //}
    public class Patient
        {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string DeseaseDeatails { get; set; }
        public string AddmitedDepartment { get; set; }
        public int DocId { get; set; }
        public string DocName { get; set; }
        public int NurseId { get; set; }
        public string NurseName { get; set; }
        public string WardNo { get; set; }
        public string RoomNo { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
        public DateTime AddmitDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public decimal Payment { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}