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
        public void CreatePublichHouse(CreatePublishHousViewModel cPPHDTO)
        {           
            PublishHouse tempPaperPublishHous = Mapper.Map<CreatePublishHousViewModel, PublishHouse>(cPPHDTO);
            _publishHouseRepository.Create(tempPaperPublishHous);
        }
        public IEnumerable<PublishHouseViewModel> ShowPublishHouse()
        {
            var tempPublishHouse = _publishHouseRepository.Get();
            return Mapper.Map<IEnumerable<PublishHouse>, IEnumerable<PublishHouseViewModel>>(tempPublishHouse);
        }
        public void DeletePublishHouse(int id)
        {
            var tempPublishHouse = _publishHouseRepository.FindById(id);
            _publishHouseRepository.Remove(tempPublishHouse);
        }
        public void UpdatePublishHouse(PublishHouseViewModel tempNewsPaper)
        {
            var newsPaper = new PublishHouse();
            Mapper.Map(tempNewsPaper, newsPaper);
            _publishHouseRepository.Update(newsPaper);
        }
        public PublishHouseViewModel GetPublishHouse(int? id)
        {
            var publichHouse = new PublishHouseViewModel();

            PublishHouse tempPublishHouse = _publishHouseRepository.FindById(id);

            Mapper.Map(tempPublishHouse, publichHouse);

            return publichHouse;
        }
    }
}