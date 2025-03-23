using CarManagementSystem.DTOs;
using CarManagementSystem.Models;

namespace CarManagementSystem.Repositories
{
    public interface ICarModelRepository
    {
        List<CarModel> GetAllCarModels();
        List<CarModel> GetCarModels(string search, string orderBy);
        int AddCarModel(CarModelDto model);
        void AddCarModelImage(int carModelId, List<IFormFile> images);
        Task<bool> DeleteCarModelAsync(int id);
    }
}
