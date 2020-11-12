using Microsoft.AspNetCore.Identity;
using MoOnlineStore.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Infrastructure.Identity
{
  public static  class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "moishy",
                    Email = "moses7.fried7@gmail.com",
                    EmailConfirmed = true,
                    UserName = "fraidyfried",
                    PhoneNumber =" 845 248 3868",
                    PhoneNumberConfirmed = true,
                    address = new Address
                    {
                        FirstName = "moishy ",
                        LastName = "fried",
                        Street = "3 vayoel moshe ct ",
                        Apartment ="311",
                        City = "New York",
                        State = "NY",
                        Zipcode = "10950"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w145430rd");
            }
        }
    }
}
