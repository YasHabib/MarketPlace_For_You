using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories
{
    public class NotificationRepository : BaseRepository<Notification, string, MKPFYDbContext>, INotificationRepository
    {
        public NotificationRepository(MKPFYDbContext context)
            : base(context)
        {

        }
    }
}
