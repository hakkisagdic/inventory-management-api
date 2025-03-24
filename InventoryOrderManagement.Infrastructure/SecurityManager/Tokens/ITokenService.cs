using System.Security.Claims;
using InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity;

namespace InventoryOrderManagement.Infrastructure.SecurityManager.Tokens;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, List<Claim>? userClaims);
    string GenerateRefreshToken();
}