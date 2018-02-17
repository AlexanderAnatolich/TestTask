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

namespace BLL.Services
{
    public class NewsPaperService
    {
        DataContex _modelsContext;
        PaperPublishHouseRepository _publishHouseRepository;
        NewsPapersRepository _newsPapersRepository;
        public NewsPaperService(string connectionString)
        {
            _modelsContext = new DataContex(connectionString);
            _publishHouseRepository = new PaperPublishHouseRepository(_modelsContext);
            _newsPapersRepository = new NewsPapersRepository(_modelsContext);
        }
        public CreateNewsPaperViewModel BeforeCreateNewsPaper()
        {
            var tempNewsPaper = new CreateNewsPaperViewModel();

            tempNewsPaper.ListPublichHouse = new SelectList(_publishHouseRepository.Get(), "Id", "PublishHouse");

            return tempNewsPaper;
        }
        public void CreateNewsPaper(CreateNewsPaperViewModel cNPDTO)
        {
            NewsPaper tempPaper = Mapper.Map<NewsPaper>(cNPDTO);

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
        public void UpdateNewsPapers(EditNewsPaperViewModel tempNewsPaper)
        {
            var newsPaper = new NewsPaper();
            Mapper.Map(tempNewsPaper, newsPaper);
            _newsPapersRepository.Update(newsPaper);
        }
        public EditNewsPaperViewModel GetNewsPaper(int? id)
        {
            NewsPaper tempPaper = _newsPapersRepository.FindById(id);
            EditNewsPaperViewModel newsPaper = new EditNewsPaperViewModel();

            Mapper.Map(tempPaper, newsPaper);

            newsPaper.ListPublichHouse = new SelectList(_publishHouseRepository.Get(), "Id", "PublishHouse");

            return newsPaper;
        }
    }
}