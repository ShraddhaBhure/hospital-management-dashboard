
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HS.Models
{
    //public class Nurse
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //    public DateTime DOB { get; set; }
    //    public string Phone { get; set; }
    //    public string Gender { get; set; }
    //    public string Specialist { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    [NotMapped]
    //    public IFormFile Photo { get; set; }
    //    public string PhotoPath { get; set; }

    //    public string Salary { get; set; }
    //}
    //
    //

    public class Nurse
    {
        [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DOB { get; set; }
    public string Phone { get; set; }
    public string Gender { get; set; }
    public string Specialist { get; set; }
    public DateTime CreatedDate { get; set; }
    
        [NotMapped]
    public IFormFile Photo { get; set; }
    public string PhotoPath { get; set; }

    public string Salary { get; set; }
}

}
