using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;
using DAL.Models;
using Model.Models;
using DAL.Repositories;
using DAL.DataContext;
using AutoMapper;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PublishHouseService
    {
        DataContex _modelsContext;
        PublishHouseRepository _publishHouseRepository;
        public PublishHouseService(string connectionString)
        {
            _modelsContext = new DataContex(connectionString);
            _publishHouseRepository = new PublishHouseRepository(_modelsContext);
        }
        public async Task CreateAsync(CreatePublishHousViewModel model)
        {           
            var tempPaperPublishHous = Mapper.Map<CreatePublishHousViewModel, PublishHouse>(model);
            await _publishHouseRepository.CreateAsync(tempPaperPublishHous);
        }
        public async Task<IEnumerable<PublishHouseViewModel>> GetAllAsync()
        {
            var tempPublishHouse = await _publishHouseRepository.GetAsync();
            return Mapper.Map<IEnumerable<PublishHouse>, IEnumerable<PublishHouseViewModel>>(tempPublishHouse);
        }
        public async Task DeleteAsync(int id)
        {
            var tempPublishHouse = await _publishHouseRepository.FindByIdAsync(id);
            _publishHouseRepository.Remove(tempPublishHouse);
        }
        public async Task UpdateAsync(PublishHouseViewModel tempNewsPaper)
        {
            var newsPaper = new PublishHouse();
            Mapper.Map(tempNewsPaper, newsPaper);
            await _publishHouseRepository.UpdateAsync(newsPaper);
        }
        public async Task<PublishHouseViewModel> GetAsync(int? id)
        {
            var publichHouse = new PublishHouseViewModel();

            PublishHouse tempPublishHouse = await _publishHouseRepository.FindByIdAsync(id);

            Mapper.Map(tempPublishHouse, publichHouse);

            return publichHouse;
        }
    }
}