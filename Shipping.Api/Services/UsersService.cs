using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Services;

public class UsersService(
    UserManager<ApplicationUser> userManager,
    IJWTProvider jWTProvider,
    IMapper mapper,
    ISpecialCityCostService specialCityCostService,
    ISpecialCourierRegionService specialCourierRegionService):IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJWTProvider _jWTProvider = jWTProvider;
    private readonly IMapper _mapper = mapper;
    private readonly ISpecialCityCostService _specialCityCostService = specialCityCostService;
    private readonly ISpecialCourierRegionService _specialCourierRegionService = specialCourierRegionService;

    public async Task<string> AddCourierAsync(AddCourierDTO addCourierDTO,CancellationToken cancellationToken = default)
    {
        if(await _userManager.Users.AnyAsync(u => u.Email == addCourierDTO.Email))
            return "Another user with the same Email is already exist";
        var user = _mapper.Map<ApplicationUser>(addCourierDTO);
        var result = await _userManager.CreateAsync(user,addCourierDTO.Password);
        if(!result.Succeeded)
            return string.Join(",",result.Errors.Select(e => e.Description));
        //await _userManager.AddToRoleAsync(user,addEmployeeDTO.RoleName); // Courier Role
        foreach(var item in addCourierDTO.SpecialCourierRegions)
        {
            item.CourierId = user.Id;
        }
        var specialCourierRegion = _mapper.Map<List<SpecialCourierRegion>>(addCourierDTO.SpecialCourierRegions);
        await _specialCourierRegionService.AddRangeAsync(specialCourierRegion);
        return string.Empty;
    }

    public async Task<string> AddEmployeeAsync(AddEmployeeDTO addEmployeeDTO,CancellationToken cancellationToken = default)
    {
        if(await _userManager.Users.AnyAsync(u => u.Email == addEmployeeDTO.Email))
            return "Another user with the same Email is already exist";
        var user = _mapper.Map<ApplicationUser>(addEmployeeDTO);
        var result = await _userManager.CreateAsync(user,addEmployeeDTO.Password);
        if(!result.Succeeded)
            return string.Join(",",result.Errors.Select(e => e.Description));
        //await _userManager.AddToRoleAsync(user,addEmployeeDTO.RoleName);
        return string.Empty;
    }

    public async Task<string> AddMerchantAsync(AddMerchantDTO addMerchantDTO,CancellationToken cancellationToken = default)
    {
        if(await _userManager.Users.AnyAsync(u => u.Email == addMerchantDTO.Email))
            return "Another user with the same Email is already exist";
        var user = _mapper.Map<ApplicationUser>(addMerchantDTO);
        var result = await _userManager.CreateAsync(user,addMerchantDTO.Password);
        if(!result.Succeeded)
            return string.Join(",",result.Errors.Select(e => e.Description));
        //await _userManager.AddToRoleAsync(user,addEmployeeDTO.RoleName); // Merchant Role
        if(addMerchantDTO.SpecialCityCosts is not null)
        {
            foreach(var specialCityCosts in addMerchantDTO.SpecialCityCosts)
            {
                specialCityCosts.MerchantId = user.Id;
            }
            var specialCityCost = _mapper.Map<List<SpecialCityCost>>(addMerchantDTO.SpecialCityCosts);
            await _specialCityCostService.AddRangeAsync(specialCityCost);
        }
        return string.Empty;
    }

    public async Task<LoginResponseDTO?> GetTokenAsync(LoginDTO loginDTO,CancellationToken cancellationToken = default)
    {
        if(await _userManager.FindByEmailAsync(loginDTO.Email) is not { } user)
            return null;
        if(!await _userManager.CheckPasswordAsync(user,loginDTO.Password))
            return null;
        var (token, expiresIn) = _jWTProvider.GenerateJwtToken(user);
        return new LoginResponseDTO
        (
            user.Id,
                user.Email!,
                user.FullName,
            token,
            expiresIn
        );
    }
}
