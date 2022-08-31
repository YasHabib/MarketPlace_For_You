using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{
    /// <summary>
    /// Storing in-app notifications
    /// </summary>
    public class Notification : BaseEntity<string>
    {

        /// <summary>
        /// Content of the notification
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Date the notification was sent
        /// </summary>
        public DateTime SentDate { get; set; }

        /// <summary>
        /// If the notification is read by the user
        /// </summary>
        public bool IsRead { get; set; }

        ///// <summary>
        ///// Setting up many to many relation to the user
        ///// </summary>
        //public ICollection<User> users;

    }
}
