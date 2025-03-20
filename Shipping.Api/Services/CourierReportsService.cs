using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;
using Shipping.Api.Infrastructure.Repositories;
using System.Linq;

namespace Shipping.Api.Services;

public class CourierReportsService
{
    private readonly IGenericRepository<CourierReport,int> _genericRepository;
    private readonly IGenericRepository<ApplicationUser,string> _userRepository;
    private readonly IMapper _map; 
    public CourierReportsService(IGenericRepository<CourierReport,int> _genericRepository,IMapper _map,IGenericRepository<ApplicationUser,string> _userRepository)
    {
        this._genericRepository = _genericRepository;
        this._map = _map;
        this._userRepository = _userRepository;
    }
    public async Task<List<GetAllCourierOrderCountDto>> GetAllCourierReportsAsync()
    {
        var courierReports = await _genericRepository.GetAllAsync();
        if(courierReports == null)
        {
            return null;
        }
        var reportsDto = _map.Map<List<CourierReportDto>>(courierReports);
        List<GetAllCourierOrderCountDto> getAllCourierOrderCountDto = new();
        getAllCourierOrderCountDto.Add(new GetAllCourierOrderCountDto
        {
            CourierName = reportsDto.Select(C => C.CourierName).FirstOrDefault(),
            OrdersCount = courierReports.Select(O => O.OrderId).Count()
        });

        return getAllCourierOrderCountDto;

    }
    public async Task<CourierReportDto> GetCourierReportByIdAsync(int id)
    {
        CourierReport CourierReport = await _genericRepository.GetByIdAsync(id);
        if(CourierReport == null)
        {
            return null;
        }
        var reportsDto = _map.Map<CourierReportDto>(CourierReport);
        List<ApplicationUser> applicationUser = new();
        applicationUser = await _userRepository.GetAllAsync();
       
            var merchant = applicationUser.Where(u => u.Id == reportsDto.ClientName).FirstOrDefault();

            reportsDto.ClientName = merchant?.StoreName ?? "Unknown StoreName";
        
        return reportsDto;

    }
    //public async Task<CourierReportDto> CreateCourierReportAsync(CourierReportDto courierReportDto)
    //{
    //    CourierReport CourierReport = new() {
    //        CourierId = courierReportDto.CourierId,
            
    //    };
    //    await _genericRepository.AddAsync(CourierReport);
    //    return _map.Map<CourierReportDto>(CourierReport);
    //}


}
