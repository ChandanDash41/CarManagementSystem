using CarManagementSystem.Models;
using CarManagementSystem.Repositories;
using CarManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDataController : ControllerBase
    {
        private readonly ISalesDataService _salesDataService;

        public SalesDataController(ISalesDataService salesDataService)
        {
            _salesDataService = salesDataService;
        }

        [HttpGet("{id}")]
        public IActionResult GetSalesData(int id)
        {
            try
            {
                var salesData = _salesDataService.GetSalesDataById(id);
                return Ok(salesData);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateSalesData([FromBody] SalesData salesData)
        {
            if (salesData == null)
                return BadRequest("Sales data is null.");

            var createdData = _salesDataService.CreateSalesData(salesData);
            return CreatedAtAction(nameof(GetSalesData), new { id = createdData.Id }, createdData);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSalesData(int id, [FromBody] SalesData salesData)
        {
            if (salesData == null || salesData.Id != id)
                return BadRequest("Sales data or ID mismatch.");

            try
            {
                var updatedData = _salesDataService.UpdateSalesData(id, salesData);
                return Ok(updatedData);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid sales data.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSalesData(int id)
        {
            try
            {
                _salesDataService.DeleteSalesData(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("salesmen")]
        public IActionResult GetSalesmen()
        {
            var salesmen = _salesDataService.GetAllSalesmens();
            return Ok(salesmen);
        }

        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = _salesDataService.GetAllBrand();
            return Ok(brands);
        }

        [HttpGet("classes")]
        public IActionResult GetClasses()
        {
            var classes = _salesDataService.GetAllClass();
            return Ok(classes);
        }
    }
}
