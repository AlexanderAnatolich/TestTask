using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Models;
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
        public List<BookAndPaperViewModel> convertmodel { get; private set; }
        public SummaryService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString();
            _bookService = new BookService(connectionString);
            _newsPaperService = new NewsPaperService(connectionString);
        }
        public async Task<List<BookAndPaperViewModel>> Calculate()
        {
            convertmodel = new List<BookAndPaperViewModel>();
            var allBooks = await _bookService.GelAllBooksAsync();
            var allNewsPaper = _newsPaperService.ShowNewsPapers();
            List<BookAndPaperViewModel> tempParam = new List<BookAndPaperViewModel>();

            convertmodel.AddRange(Mapper.Map<IEnumerable<BookViewModel>, List<BookAndPaperViewModel>>(allBooks, tempParam));
            convertmodel.AddRange(Mapper.Map<IEnumerable<NewsPaperViewModel>, List<BookAndPaperViewModel>>(allNewsPaper, tempParam));

            return convertmodel;
        }
    }
}
