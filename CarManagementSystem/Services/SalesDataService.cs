using CarManagementSystem.Models;
using CarManagementSystem.Repositories;
using System.Security.Claims;

namespace CarManagementSystem.Services
{
    public class SalesDataService : ISalesDataService
    {
        private readonly ISalesDataRepository _repository;

        public SalesDataService(ISalesDataRepository repository)
        {
            _repository = repository;
        }

        public SalesData GetSalesDataById(int id)
        {
            return _repository.GetSalesDataById(id);
        }

        public SalesData CreateSalesData(SalesData salesData)
        {
            if (salesData == null) throw new ArgumentNullException(nameof(salesData));
            _repository.CreateSalesData(salesData);
            return salesData;
        }

        public SalesData UpdateSalesData(int id, SalesData salesData)
        {
            if (salesData == null || salesData.Id != id) throw new ArgumentException("ID mismatch or invalid data");

            var existingData = _repository.GetSalesDataById(id);
            if (existingData == null) throw new KeyNotFoundException($"Sales data with ID {id} not found.");

            _repository.UpdateSalesData(salesData);
            return salesData;
        }

        public bool DeleteSalesData(int id)
        {
            var existingData = _repository.GetSalesDataById(id);
            if (existingData == null) throw new KeyNotFoundException($"Sales data with ID {id} not found.");

            _repository.DeleteSalesData(id);
            return true;
        }

        public IEnumerable<Brand> GetAllBrand()
        {
            return _repository.GetAllBrands();
        }

        public IEnumerable<Class> GetAllClass()
        {
            return _repository.GetAllClasses();
        }

        public IEnumerable<Salesman> GetAllSalesmens()
        {
            return _repository.GetAllSalesman();
        }
    }
}
