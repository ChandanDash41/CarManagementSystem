using CarManagementSystem.DTOs;

namespace CarManagementSystem.Services
{
    public interface ISalesmanCommissionService
    {
        Task<List<SalesmanCommissionDto>> GetCommissionReportAsync();
    }
}
