using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Core.Domain.Helpers;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;
using Shipping.Api.Services;

namespace Shipping.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegionController:ControllerBase
{
    private readonly RegionService _regionService;
    public RegionController(RegionService _regionService)
    {
        this._regionService = _regionService;

    }
    [HttpGet]
    public async Task<ActionResult<List<RegionDto>>> GetAllRegions()
    {
        try
        {
            if(await _regionService.GetAllRegionsAsync() == null)
            {
                return NotFound(new ResponseAPI(404));
            }
            return Ok(await _regionService.GetAllRegionsAsync());
        }
        catch
        {
            return BadRequest(new ResponseAPI(400));
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<RegionDto>> GetRegionById(int id)
    {
        try
        {
            if(await _regionService.GetRegionByIdAsync(id) == null)
            {
                return NotFound(new ResponseAPI(404));
            }
            return Ok(await _regionService.GetRegionByIdAsync(id));
        }
        catch
        {
            return BadRequest(new ResponseAPI(400));
        }
    }
    [HttpPost]
    public async Task<ActionResult<RegionDto>> CreateRegion(RegionDto region)
    {
        try
        {
        if(region == null)
        {
            return NotFound(new ResponseAPI(404));
            }
            return Ok(await _regionService.CreateRegionAsync(region));
        }
        catch
        {
            return BadRequest(new ResponseAPI(400));
        }
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<RegionDto>> UpdateRegion(int id,RegionDto region)
    {
        try
        { 
        if(id != region.Id)
        {
            return NotFound(new ResponseAPI(404,"ID Not Match"));

        }
        await _regionService.UpdateRegionAsync(region);
            return Ok();
        }
        catch
        {
            return BadRequest(new ResponseAPI(400));
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRegion(int id)
    {
        try
        {
        if(await _regionService.GetRegionByIdAsync(id) == null)
        {
            return NotFound();
        }
        await _regionService.DeleteRegionAsync(id);
            return Ok();
        }
        catch
        {
            return BadRequest(new ResponseAPI(400));
        }
    }
}
