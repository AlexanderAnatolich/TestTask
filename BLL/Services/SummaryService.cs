using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Model.Models;
using BLL.AutoMapperProfile;
using AutoMapper;
using Newtonsoft.Json;
using System.Configuration;

namespace BLL.Services
{
    public class SummaryService
    {
        private BookService _bookService;
        private NewsPaperService _newsPaperService;
        public SummaryService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString();
            _bookService = new BookService(connectionString);
            _newsPaperService = new NewsPaperService(connectionString);
        }
        public async Task<List<SummaryViewModel>> Calculate()
        {
            var Convertmodel = new List<SummaryViewModel>();
            var allBooks = await _bookService.GelAllAsync();
            var allNewsPaper = await _newsPaperService.GetAllAsync();
            List<SummaryViewModel> tempParam = new List<SummaryViewModel>();

            Convertmodel.AddRange(Mapper.Map<IEnumerable<BookViewModel>, List<SummaryViewModel>>(allBooks, tempParam));
            Convertmodel.AddRange(Mapper.Map(allNewsPaper, tempParam));

            return Convertmodel;
        }
    }
}
