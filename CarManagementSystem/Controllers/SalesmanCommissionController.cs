using CarManagementSystem.Models;
using CarManagementSystem.Repositories;
using CarManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesmanCommissionController : ControllerBase
    {
        private readonly ISalesmanCommissionService _salesmanService;

        public SalesmanCommissionController(ISalesmanCommissionService salesmanService)
        {
            _salesmanService = salesmanService;
        }

        [HttpGet("commissionReport")]
        public async Task<IActionResult> GetSalesmanCommissionReport()
        {
            var result = await _salesmanService.GetCommissionReportAsync();
            return Ok(result);
        }
    }
}
