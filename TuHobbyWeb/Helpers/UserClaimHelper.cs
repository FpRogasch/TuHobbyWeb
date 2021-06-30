using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace TuHobbyWeb.Helpers
{
    public static class UserClaimHelper
    {
        public static string GetFullName(this IIdentity identity)
        {
            if (identity == null)
            {
                return string.Empty; // ""
            }

            var claims = ((ClaimsIdentity)identity);
            return claims.FindFirst("FullName")?.Value;
        }
    }
}