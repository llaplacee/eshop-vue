using System;
using System.Security.Claims;

namespace EshopApi.Utilities
{
    public static class StaticTools
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return Convert.ToInt32(user.Identity.Name);
        }
    }
}
