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
        /// <param name="notificationMsg"></param>
        /// <param name="notificationDate"></param>
        public InAppNotificationVM(string notificationMsg, DateTime notificationDate)
        {
            NotificationMsg = notificationMsg;
            NotificationDate = notificationDate;
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
