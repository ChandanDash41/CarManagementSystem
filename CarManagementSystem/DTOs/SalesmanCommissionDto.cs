namespace CarManagementSystem.DTOs
{
    public class SalesmanCommissionDto
    {
        public int SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        public string Brand { get; set; }
        public string CarClass { get; set; }
        public int NumberOfCarsSold { get; set; }
        public decimal FixedCommission { get; set; }
        public decimal TotalFixedCommission { get; set; }
        public decimal ClassCommission { get; set; }
        public decimal AdditionalCommission { get; set; }
        public decimal TotalCommission { get; set; }
    }
}
