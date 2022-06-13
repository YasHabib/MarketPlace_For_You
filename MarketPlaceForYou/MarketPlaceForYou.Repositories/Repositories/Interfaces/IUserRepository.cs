using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Update(UsersEntity entity);

        //void Delete(UserInformation entity); //For super admin to delete users
    }
}
