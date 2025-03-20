using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Infrastructure.Dtos;
using Shipping.Api.Services;

namespace Shipping.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CourierReportController:ControllerBase
{
    private readonly CourierReportsService _courierReportsService;
    public CourierReportController(CourierReportsService _courierReportsService)
    {
        this._courierReportsService = _courierReportsService;

    }
    [HttpGet]
    public async Task<ActionResult<GetAllCourierOrderCountDto>> GetAllReports()
    {
        if(await _courierReportsService.GetAllCourierReportsAsync() == null)
        {
            return NotFound();
        }
        return Ok(await _courierReportsService.GetAllCourierReportsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourierReportDto>> GetReports(int id)
    {
        if(await _courierReportsService.GetCourierReportByIdAsync(id) == null)
        {
            return NotFound();
        }

        return Ok(await _courierReportsService.GetCourierReportByIdAsync(id));

    }
   
}
