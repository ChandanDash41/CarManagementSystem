namespace CarManagementSystem.Models
{
    public class SalesData
    {
        public int Id { get; set; }
        public int SalesmanId { get; set; }
        public int BrandId { get; set; }
        public int ClassId { get; set; }
        public int NumberOfCarsSold { get; set; }
        public decimal ModelPrice { get; set; }
    }
}
