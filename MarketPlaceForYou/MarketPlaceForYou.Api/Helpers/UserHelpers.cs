using System.Security.Claims;

namespace MarketPlaceForYou.Api.Helpers
{
    /// <summary>
    /// Retrieving user's id when creating a listing and attaching the id to the listing. 
    /// </summary>
    public static class UserHelpers
    {/// <summary>
    /// Getting the ID..
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
        public static string? GetId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) ?? principal.FindFirst(c => c.Type == "sub");
            if(userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
                return userIdClaim.Value;
            return null;
        }
    }
}
