using CarManagementSystem.DTOs;
using CarManagementSystem.Models;

namespace CarManagementSystem.Repositories
{
    public interface ISalesmanCommissionRepository
    {
        Task<List<SalesmanCommissionDto>> GetSalesDataAsync();
    }
}
