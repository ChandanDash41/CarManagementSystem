using CarManagementSystem.Models;

namespace CarManagementSystem.Repositories
{
    public interface ISalesDataRepository
    {
        void CreateSalesData(SalesData salesData);
        SalesData GetSalesDataById(int id);
        void UpdateSalesData(SalesData salesData);
        void DeleteSalesData(int id);
        List<Salesman> GetAllSalesman();
        List<Brand> GetAllBrands();
        List<Class> GetAllClasses();


    }
}
