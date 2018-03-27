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
        public async Task CreateAsync(CreateNewsPaperViewModel cNPDTO)
        {
            NewsPaper tempPaper = Mapper.Map<NewsPaper>(cNPDTO);
            tempPaper.PublishHouse = null;
            await _newsPapersRepository.CreateAsync(tempPaper);
        }
        public async Task<IEnumerable<NewsPaperViewModel>> GetAllAsync()
        {
            var result = await _newsPapersRepository.GetAllAsync();
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
            NewsPaper tempPaper = await _newsPapersRepository.FindByIdAsync(id);

            var result = Mapper.Map<NewsPaperViewModel>(tempPaper);

            return result;
        }
    }
}