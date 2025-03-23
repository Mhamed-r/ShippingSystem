namespace Shipping.Api.Infrastructure.Dtos;

public class GetAllCourierOrderCountDto
{
    public required string CourierName { get; set; }
    public int OrdersCount { get; set; } = 0;
}
