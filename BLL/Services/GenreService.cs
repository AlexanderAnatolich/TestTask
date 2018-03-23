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
using System.Net;
using System.Web.Mvc;

namespace BLL.Services
{
    public class GenreService
    {
        DataContex _modelsContext;
        GenerRepository _generRepository;
        public GenreService(string connectionString)
        {
            _modelsContext = new DataContex(connectionString);
            _generRepository = new GenerRepository(_modelsContext);
        }
        public void CreateGener(CreateGenerViewModel cNPDTO)
        {
            Gener tempPaper = Mapper.Map<CreateGenerViewModel, Gener>(cNPDTO);

            _generRepository.Create(tempPaper);
        }
        public IEnumerable<GenerViewModel> ShowAllGeners()
        {
            IEnumerable<Gener> tempGeners = _generRepository.Get();
            return Mapper.Map<IEnumerable<Gener>, List<GenerViewModel>>(tempGeners);
        }
        public void DeleteGeners(int Id)
        {
            var temGener = _generRepository.FindById(Id);
            _generRepository.Remove(temGener);
        }
        public void UpdateGener(GenerViewModel tempNewsPaper)
        {
            var newsGener = new Gener();
            Mapper.Map(tempNewsPaper, newsGener);
            _generRepository.Update(newsGener);
        }
        public GenerViewModel GetGener(int? id)
        {
            Gener tempGener = _generRepository.FindById(id);
            GenerViewModel gener = new GenerViewModel();

            Mapper.Map(tempGener, gener);

            return gener;
        }
    }
}