using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IAuth0Service
    {
        Task BlockUser(string userId);
        Task UnblockUser(string userId);

    }
}
