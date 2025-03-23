using System.ComponentModel.DataAnnotations;

namespace Shipping.Api.Infrastructure.Dtos;

public class RegionDto
{
    public int Id { get; set; }
    public required string Governorate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
}
