using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;
using DAL.Repositories;
using DAL.Models;
using DAL.DataContext;
using BLL.Models;
using AutoMapper;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class NewsPaperService
    {
        DataContex _modelsContext;
        PublishHouseRepository _publishHouseRepository;
        NewsPapersRepository _newsPapersRepository;
        public NewsPaperService(string connectionString)
        {
            _modelsContext = new DataContex(connectionString);
            _publishHouseRepository = new PublishHouseRepository(_modelsContext);
            _newsPapersRepository = new NewsPapersRepository(_modelsContext);
        }
        public void CreateNewsPaper(CreateNewsPaperViewModel cNPDTO)
        {
            NewsPaper tempPaper = Mapper.Map<NewsPaper>(cNPDTO);
            tempPaper.PublishHouse = null;
            _newsPapersRepository.Create(tempPaper);
        }
        public IEnumerable<NewsPaperViewModel> ShowNewsPapers()
        {
            List<NewsPaper> tempNewsPapers = _newsPapersRepository.Get().ToList();
            return Mapper.Map<List<NewsPaper>, List<NewsPaperViewModel>>(tempNewsPapers);
        }
        public void DeleteNewsPaper(int id)
        {
            var NewsPaper = _newsPapersRepository.FindById(id);
            _newsPapersRepository.Remove(NewsPaper);
        }
        public async Task<Boolean> DeleteRangeNewsPaper(List<int> id)
        {
            var result = await _newsPapersRepository.RemoveRangeAsync(x => id.Contains(x.Id));
            return result;
        }
        public void UpdateNewsPapers(NewsPaperViewModel tempNewsPaper)
        {
            var newsPaper = new NewsPaper();
            Mapper.Map(tempNewsPaper, newsPaper);
            _newsPapersRepository.Update(newsPaper);
        }
        public NewsPaperViewModel GetNewsPaper(int? id)
        {
            NewsPaper tempPaper = _newsPapersRepository.FindById(id);

            var newsPaper = Mapper.Map<NewsPaperViewModel>(tempPaper);

            return newsPaper;
        }
    }
}