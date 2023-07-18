namespace HS.Models
{
    public class AssetTracking
    {
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public string AssetType { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string LastCheckedOutBy { get; set; }
        public DateTime? LastCheckedOutDate { get; set; }
        public DateTime? LastCheckedInDate { get; set; }
        public string CurrentOwner { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
    }
}
