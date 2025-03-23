using CarManagementSystem.DTOs;
using CarManagementSystem.Models;
using CarManagementSystem.Repositories;
using CarManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementSystem.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly ICarModelService _service;

        public CarModelController(ICarModelService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllCarModels()
        {
            var models = _service.GetAllCarModels();
            if (models == null || models.Count == 0)
                return NotFound("No car models found.");

            return Ok(models);
        }

        [HttpGet("GetByFilter")]
        public ActionResult<List<CarModel>> GetCarModels([FromQuery] string search = "", [FromQuery] string orderBy = "DateOfManufacturing DESC")
        {
            return _service.GetCarModels(search, orderBy);
        }

        [HttpPost("AddCarModel")]
        public IActionResult AddCarModel(CarModelDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            int carModelId = _service.AddCarModel(model);

            if (carModelId > 0 && model.Images?.Count > 0)
            {
                _service.AddCarModelImage(carModelId, model.Images);
            }

            return Ok(new { message = "Car Model added successfully!", carModelId });
        }

        [HttpDelete("DeleteCarModel/{id}")]
        public async Task<IActionResult> DeleteCarModel(int id)
        {
            var result = await _service.DeleteCarModelAsync(id);
            if (!result)
                return NotFound(new { message = "Car Model not found" });

            return Ok(new { message = "Car Model deleted successfully" });
        }


    }
}
