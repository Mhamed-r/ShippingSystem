using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        if(await _regionService.GetAllRegionsAsync() == null)
        {
            return NotFound();
        }
        return Ok(await _regionService.GetAllRegionsAsync());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<RegionDto>> GetRegionById(int id)
    {
        if(await _regionService.GetRegionByIdAsync(id) == null)
        {
            return NotFound();
        }
        return Ok(await _regionService.GetRegionByIdAsync(id));
    }
    [HttpPost]
    public async Task<ActionResult<RegionDto>> CreateRegion(RegionDto region)
    {
        if(region == null)
        {
            return BadRequest();
        }
        return Ok(await _regionService.CreateRegionAsync(region));
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<RegionDto>> UpdateRegion(int id,RegionDto region)
    {
        if(id != region.Id)
        {
            return BadRequest("ID mismatch");
        }
        await _regionService.UpdateRegionAsync(region);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRegion(int id)
    {
        if(await _regionService.GetRegionByIdAsync(id) == null)
        {
            return NotFound();
        }
        await _regionService.DeleteRegionAsync(id);
        return Ok();
    }
}
