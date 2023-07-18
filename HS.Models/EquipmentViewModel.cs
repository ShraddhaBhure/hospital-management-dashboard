using System.ComponentModel.DataAnnotations;

namespace HS.Models
{
    public class EquipmentViewModelMain
    {
        [Key]
        public List<OrderEquipment> OrderEquipment { get; set; }
        public List<EquipmentViewModel> EquipmentViewModel { get; set; }
    }
    public class EquipmentViewModel
    { 
        
        [Key]
       
  
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
       
        public string ManufacturerName { get; set; }
        public string ModelNumber { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
     
    }
    public class OrderEquipment : EquipmentViewModel
    {  //[Key]

        public int OrderId { get; set; }
      
        public decimal TotalPrice { get; set; }
        public string PurchaseDept { get; set; }
        public decimal PricePerUnit { get; set; }
        public string PurchaseAuthority { get; set; }
        public DateTime PurchaseDate { get; set; }
    }



}
