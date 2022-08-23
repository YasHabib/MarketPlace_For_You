using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{
    /// <summary>
    /// Base Entity for implementing Id
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class BaseEntity<TId>
    {
        /// <summary>
        /// Id generic
        /// </summary>
        [Key]
        public TId? Id { get; set; }
    }
}
