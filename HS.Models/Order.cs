using System.ComponentModel.DataAnnotations;


namespace HS.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public int ShipVia { get; set; }

        [Required]
        public decimal Freight { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipName { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipCity { get; set; }

        [StringLength(50)]
        public string ShipRegion { get; set; }

        [Required]
        [StringLength(20)]
        public string ShipPostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ShipCountry { get; set; }

        //[ForeignKey("CustomerID")]
        //public virtual Customer Customer { get; set; }

        //[ForeignKey("EmployeeID")]
        //public virtual Employee Employee { get; set; }

        //[ForeignKey("ShipVia")]
        //public virtual Shipper Shipper { get; set; }
    }
}
