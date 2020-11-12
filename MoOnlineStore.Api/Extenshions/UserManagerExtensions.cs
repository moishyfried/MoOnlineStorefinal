using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoOnlineStore.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoOnlineStore.Api.Extenshions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> GavetheUserByClaimPrincibal(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user.Claims?.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value;

            return await userManager.Users.FirstOrDefaultAsync(p => p.Email == email);
        }
        public static async Task<AppUser> GavetheUserAddressByClaimPrincibal(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var email =  user.Claims?.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value;

            return await userManager.Users.Include(x => x.address).FirstOrDefaultAsync(p => p.Email == email);
        }
      


    }
}
