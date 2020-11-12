using System;
using System.Collections.Generic;
using System.Text;
using MoOnlineStore.Core.Identity;

namespace MoOnlineStore.Core.Interfaces
{
   public interface ITokenService
    {
        string GenarateToken(AppUser user);
    }
}
