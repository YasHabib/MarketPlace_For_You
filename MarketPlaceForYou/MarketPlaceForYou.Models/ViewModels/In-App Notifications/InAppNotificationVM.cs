using MarketPlaceForYou.Models.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels
{   /// <summary>
    /// Displaying notification in web portal
    /// </summary>
    public class InAppNotificationVM
    {   
        /// <summary>
        /// Constructor for notification view model
        /// </summary>
        /// <param name="src"></param>
        public InAppNotificationVM(Notification src)
        {
            NotificationMsg = src.Content;
            NotificationDate = DateTime.UtcNow.Date;
        }
    
        /// <summary>
        /// Message in the notification the user will see
        /// </summary>
        public string NotificationMsg { get; set; } = string.Empty;
        /// <summary>
        /// Date when the notification was sent
        /// </summary>
        public DateTime NotificationDate { get; set; }
    }
}
