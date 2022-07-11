using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels.FAQ;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class FAQService : IFAQService
    {
        private readonly IUnitOfWork _uow;

        public FAQService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<FAQVM> Create(FAQaddVM src)
        {
            var newEntity = new FAQ(src);

            _uow.FAQs.Create(newEntity);
            await _uow.SaveAsync();

            var model = new FAQVM(newEntity);

            return model;
        }

        public async Task<FAQVM> GetById(Guid id)
        {
            var result = await _uow.FAQs.GetById(id);
            var model = new FAQVM(result);
            return model;
        }
        public async Task<List<FAQVM>> GetAll()
        {
            var results = await _uow.FAQs.GetAll();
            var models = results.Select(faqs => new FAQVM(faqs)).ToList();
            return models;
        }
        public async Task<FAQVM> Update(FAQupdateVM src)
        {
            //read
            var entity = await _uow.FAQs.GetById(src.Id);
            //perform
            entity.Title = src.Title;
            entity.Description = src.Description;
            //write
            _uow.FAQs.Update(entity);
            await _uow.SaveAsync();
            //return the FAQ to front end
            var model = new FAQVM(entity);
            return model;
        }
        public async Task Delete(Guid id)
        {
            var entity = await _uow.FAQs.GetById(id);
            _uow.FAQs.Delete(entity);
            await _uow.SaveAsync();
        }
    }
}
