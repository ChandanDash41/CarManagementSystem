using CarManagementSystem.Models;

namespace CarManagementSystem.Services
{
    public interface ISalesDataService
    {
        SalesData GetSalesDataById(int id);
        SalesData CreateSalesData(SalesData salesData);
        SalesData UpdateSalesData(int id, SalesData salesData);
        bool DeleteSalesData(int id);
        IEnumerable<Salesman> GetAllSalesmens();
        IEnumerable<Brand> GetAllBrand();
        IEnumerable<Class> GetAllClass();

    }
}
