using System.ComponentModel.DataAnnotations;

namespace HS.Models
{
    public class MedicalEquipment
    { 
        [Key]
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string ManufacturerName { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string PurchaseBy { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchaseDept { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        public string Department { get; set; }
        public string Condition { get; set; }
        public int Quantity { get; set; }
         public DateTime LastMaintenanceDate { get; set; }
        public DateTime NextMaintenanceDate { get; set; }
        public string Notes { get; set; }
        public decimal Price { get; set; }

      
    }

}
