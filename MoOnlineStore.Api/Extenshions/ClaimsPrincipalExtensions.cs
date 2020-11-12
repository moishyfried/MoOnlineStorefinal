using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoOnlineStore.Api.Extenshions
{
    public static  class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
          var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
          return email;
        }
    }
}
