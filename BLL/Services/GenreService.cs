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
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;

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
        public async Task CreateAsync(CreateGenerViewModel model)
        {
            var tempPaper = Mapper.Map<CreateGenerViewModel, Gener>(model);

            await _generRepository.CreateAsync(tempPaper);
        }
        public async Task<IEnumerable<GenerViewModel>> GetAllAsync()
        {
            var tempGeners = await _generRepository.GetAsync();
            return Mapper.Map<IEnumerable<Gener>, List<GenerViewModel>>(tempGeners);
        }
        public async Task DeleteAsync(int Id)
        {
            var temGener = _generRepository.FindById(Id);
            await _generRepository.RemoveAsync(temGener);
        }
        public async Task UpdateAsync(GenerViewModel tempNewsPaper)
        {
            var newsGener = new Gener();
            Mapper.Map(tempNewsPaper, newsGener);
            await _generRepository.UpdateAsync(newsGener);
        }
        public async Task<GenerViewModel> Get(int? id)
        {
            var tempGener = await _generRepository.FindByIdAsync(id);
            var gener = new GenerViewModel();

            Mapper.Map(tempGener, gener);

            return gener;
        }
    }
}