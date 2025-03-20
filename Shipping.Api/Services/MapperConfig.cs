using AutoMapper;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Services;

public class MapperConfig:Profile
{
    public MapperConfig()
    {
        CreateMap<CourierReport,CourierReportDto>().AfterMap((src,dest) =>
        {
            dest.CourierName = src.Courier.FullName;
            dest.Area = src.Order.CitySetting.Name;
            dest.ClientName = src.Order.MerchantId;
            dest.CustomerName = src.Order.CustomerName;
            dest.CustomerPhone = src.Order.CustomerPhone1;
            dest.CustomerAddress = src.Order.CustomerAddress;
            dest.products = src.Order.Products.Select(x => x.Name).ToList();
            dest.orderStatus = src.Order.Status.ToString();
            dest.Amount = src.Order.OrderCost;
            dest.Notes = src.Order.Notes;
            




        }).ReverseMap();

        CreateMap<Region,RegionDto>().AfterMap((src,dest) =>
        {}).ReverseMap();
    }
}
