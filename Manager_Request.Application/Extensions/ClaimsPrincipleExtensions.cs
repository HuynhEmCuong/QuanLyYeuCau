using Manager_Request.Ultilities;
using System.Linq;
using System.Security.Claims;

namespace Manager_Request.Application.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value.ToInt();
        }


        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static string GetValueByType(this ClaimsPrincipal user, string type)
        {
            var userClaims = user.Claims.FirstOrDefault(x => x.Type == type);
            if (userClaims != null)
            {
                return userClaims.Value.ToSafetyString();
            }
            return string.Empty;
        }
    }
}