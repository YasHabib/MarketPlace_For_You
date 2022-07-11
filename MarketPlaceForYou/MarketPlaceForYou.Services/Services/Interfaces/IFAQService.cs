using MarketPlaceForYou.Models.ViewModels.FAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IFAQService
    {
        Task<FAQVM> Create(FAQaddVM src);
        Task<FAQVM> GetById(Guid Id);
        Task<List<FAQVM>> GetAll();
        Task<FAQVM> Update(FAQupdateVM src);
        Task Delete(Guid Id);
    }
}
