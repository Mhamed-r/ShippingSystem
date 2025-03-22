using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Domain.Helpers;
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
        try
        {
            if(await _courierReportsService.GetAllCourierReportsAsync() == null)
            {
                return NotFound(new ResponseAPI(404));
            }
            return Ok(await _courierReportsService.GetAllCourierReportsAsync());
        } catch
        {
            return BadRequest(new ResponseAPI(400));
        }
    }
    [HttpGet("{id}")]
        public async Task<ActionResult<CourierReportDto>> GetReports(int id)
        {
        try
        {

            if(await _courierReportsService.GetCourierReportByIdAsync(id) == null)
            {
                return NotFound(new ResponseAPI(404));

            }

            return Ok(await _courierReportsService.GetCourierReportByIdAsync(id));
        }
        catch
        {
            return BadRequest(new ResponseAPI(400));
        }

    }

    
}
