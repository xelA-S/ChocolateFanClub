using System.Security.Claims;

namespace ChocolateFanClub
{
    //This is an example of an extension
    public static class ClaimsPrincipalExtensions
    {
        // this method works as an extension for httpContextAccessor.HttpContext?.User
        // "this" refers to the current instance which the item is related to
        // ClaimsPrincipal is an implementation which relates to claim based identites
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
