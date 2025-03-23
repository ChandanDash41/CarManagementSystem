using CarManagementSystem.DTOs;
using CarManagementSystem.Models;

namespace CarManagementSystem.Services
{
    public interface ICarModelService
    {
        List<CarModel> GetAllCarModels();
        List<CarModel> GetCarModels(string search, string orderBy);
        int AddCarModel(CarModelDto model);
        void AddCarModelImage(int carModelId, List<IFormFile> images);
        Task<bool> DeleteCarModelAsync(int id);
    }
}
