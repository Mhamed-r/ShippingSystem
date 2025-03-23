using Shipping.Api.Core.Domain.Models;

namespace Shipping.Api.Core.Abstraction;

public interface IJWTProvider
{
    (string token, int expiresIn) GenerateJwtToken(ApplicationUser user);
}
