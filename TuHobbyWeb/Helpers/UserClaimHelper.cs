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

        // Metodo para obtener el ID de un usuario, buscando con Claims, el nombre identificador que se le da al usuario la primera vez que hace LogIn
        public static int GetId(this IIdentity identity)
        {
            if (identity == null)
                return 0; // ""
            var claims = ((ClaimsIdentity)identity);
            var val = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (val != null)
            {
                try
                {
                    return int.Parse(val);

                }
                catch (Exception e)
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}