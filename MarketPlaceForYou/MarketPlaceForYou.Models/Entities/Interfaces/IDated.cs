using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities.Interfaces
{
    /// <summary>
    /// Interface if a class uses Created date
    /// </summary>
    public interface IDated
    {/// <summary>
    /// Created date/time
    /// </summary>
        public DateTime Created { get; set; }

    }
}
