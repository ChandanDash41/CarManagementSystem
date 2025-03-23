using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;

namespace CarManagementSystem.Services
{
    public class CarModelService : ICarModelService
    {
        private readonly ICarModelRepository _repository;

        public CarModelService(ICarModelRepository repository)
        {
            _repository = repository;
        }

        public List<CarModel> GetAllCarModels()
        {
            return _repository.GetAllCarModels();
        }

        public List<CarModel> GetCarModels(string search, string orderBy)
        {
            return _repository.GetCarModels(search, orderBy);
        }

        public int AddCarModel(CarModelDto model)
        {
            return _repository.AddCarModel(model);
        }

        public void AddCarModelImage(int carModelId, List<IFormFile> images)
        {
            _repository.AddCarModelImage(carModelId, images);
        }

        public async Task<bool> DeleteCarModelAsync(int id)
        {
            return await _repository.DeleteCarModelAsync(id);
        }
    }
}
