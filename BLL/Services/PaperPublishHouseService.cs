using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;
using DAL.Models;
using BLL.Models;
using DAL.Repositories;
using DAL.DataContext;
using AutoMapper;

namespace BLL.Services
{
    public class PaperPublishHouseService
    {
        DataContex _modelsContext;
        PaperPublishHouseRepository _publishHouseRepository;
        public PaperPublishHouseService(string connectionString)
        {
            _modelsContext = new DataContex(connectionString);
            _publishHouseRepository = new PaperPublishHouseRepository(_modelsContext);
        }
        public void CreatePaperPublichHouse(CreatePaperPublishHousViewModel cPPHDTO)
        {           
            PaperPublishHouses tempPaperPublishHous = Mapper.Map<CreatePaperPublishHousViewModel, PaperPublishHouses>(cPPHDTO);
            _publishHouseRepository.Create(tempPaperPublishHous);
        }
        public IEnumerable<PaperPublishHouseViewModel> ShowPublishHouse()
        {
            var tempPublishHouse = _publishHouseRepository.Get();
            return Mapper.Map<IEnumerable<PaperPublishHouses>, IEnumerable<PaperPublishHouseViewModel>>(tempPublishHouse);
        }
        public void DeletePublishHouse(int id)
        {
            var tempPublishHouse = _publishHouseRepository.FindById(id);
            _publishHouseRepository.Remove(tempPublishHouse);
        }
        public void UpdatePaperPublishHouse(EditPaperPublishHouseViewModel tempNewsPaper)
        {
            var newsPaper = new PaperPublishHouses();
            Mapper.Map(tempNewsPaper, newsPaper);
            _publishHouseRepository.Update(newsPaper);
        }
        public PaperPublishHouseViewModel GetPaperPublishHosuse(int? id)
        {
            var publichHouse = new PaperPublishHouseViewModel();

            PaperPublishHouses tempPublishHouse = _publishHouseRepository.FindById(id);

            Mapper.Map(tempPublishHouse, publichHouse);

            return publichHouse;
        }
    }
}