using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;
using DAL.Repositories;
using DAL.Models;
using DAL.DataContext;
using Model.Models;
using AutoMapper;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class NewsPaperService
    {
        PublishHouseRepository _publishHouseRepository;
        NewsPapersRepository _newsPapersRepository;
        public NewsPaperService(string connectionString)
        {
            _publishHouseRepository = new PublishHouseRepository(connectionString);
            _newsPapersRepository = new NewsPapersRepository(connectionString);
        }
        public async Task CreateAsync(CreateNewsPaperViewModel model)
        {
            NewsPaper tempPaper = Mapper.Map<NewsPaper>(model);
            tempPaper.PublishHouse = null;
            await _newsPapersRepository.CreateAsync(tempPaper);
        }
        public async Task<IEnumerable<NewsPaperViewModel>> GetAllAsync()
        {
            var resultRequest = await _newsPapersRepository.GetAllAsync();
            var result = Mapper.Map<IEnumerable<NewsPaperViewModel>>(resultRequest);
            return result;
        }
        public async Task DeleteAsync(int id)
        {
            await _newsPapersRepository.RemoveAsync(id);
        }
        public async Task<Boolean> DeleteAsync(List<int> id)
        {
            var result = await _newsPapersRepository.RemoveAsync(id);
            return result;
        }
        public async Task UpdateAsync(NewsPaperViewModel tempNewsPaper)
        {
            var newsPaper = new NewsPaper();
            Mapper.Map(tempNewsPaper, newsPaper);
            await _newsPapersRepository.UpdateAsync(newsPaper);
        }
        public async Task<NewsPaperViewModel> GetAsync(int id)
        {
            var resultQuery = await _newsPapersRepository.FindByIdAsync(id);

            var result = Mapper.Map<NewsPaperViewModel>(resultQuery);
            return result;
        }
        public async Task<List<NewsPaperViewModel>> FirndByTitleAsync(string partialTitle)
        {
            var resultQuery = await _newsPapersRepository.FirndByTitleAsync(partialTitle);
            var result = Mapper.Map<List<NewsPaperViewModel>>(resultQuery);
            return result;
        }
    }
}