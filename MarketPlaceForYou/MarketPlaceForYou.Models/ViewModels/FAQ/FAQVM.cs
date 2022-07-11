using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.FAQ
{/// <summary>
/// These informations will be displayed to the end user
/// </summary>
    public class FAQVM
    {/// <summary>
    /// Constructor to view these info to end user
    /// </summary>
    /// <param name="src"></param>
        public FAQVM(Entities.FAQ src)
        {
            Id = src.Id;
            Title = src.Title;
            Description = src.Description;
        }
        /// <summary>
        /// Id of the FAQ
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title of the FAQ
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Description of the FAQ
        /// </summary>
        public string? Description { get; set; }
    }
}
