using CarManagementSystem.DTOs;
using CarManagementSystem.Repositories;

namespace CarManagementSystem.Services
{
    public class SalesmanCommissionService : ISalesmanCommissionService
    {
        private readonly ISalesmanCommissionRepository _salesmanRepository;

        public SalesmanCommissionService(ISalesmanCommissionRepository salesmanRepository)
        {
            _salesmanRepository = salesmanRepository;
        }

        public async Task<List<SalesmanCommissionDto>> GetCommissionReportAsync()
        {
            var salesData = await _salesmanRepository.GetSalesDataAsync();

            foreach (var item in salesData)
            {
                item.TotalFixedCommission = item.FixedCommission * item.NumberOfCarsSold;

                item.ClassCommission = (item.NumberOfCarsSold * item.FixedCommission) * (item.ClassCommission / 100.0M);

                if (item.CarClass == "A" && item.TotalCommission > 500000)
                {
                    item.AdditionalCommission = item.TotalCommission * 0.02M;
                }

                item.TotalCommission = item.TotalFixedCommission + item.ClassCommission + item.AdditionalCommission;
            }

            return salesData;
        }
    }
}
