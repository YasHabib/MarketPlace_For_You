using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities.Interfaces
{   /// <summary>
/// Soft deleting a user
/// </summary>
    public interface ISoftDeleted
    {
        /// <summary>
        /// Generic interface for implementing soft delete
        /// </summary>
      public bool IsDeleted { get; set; }
    }
}
