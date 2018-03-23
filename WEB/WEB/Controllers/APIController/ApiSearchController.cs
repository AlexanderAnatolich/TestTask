using BLL.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Models;

namespace WEB.Controllers.APIController
{
    [System.Web.Http.RoutePrefix("api/ApiSearch")]
    public class ApiSearchController : ApiController
    {
        public class CustomModel
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }
        private BookService _bookService;
        private NewsPaperService _newsPaperService;
        private JournalService _journalService;
        public ApiSearchController()
        {
            
        }
        [HttpPost]
        public IHttpActionResult Search([FromBody]CustomModel mod)
        {
            if (String.IsNullOrEmpty(mod.Name)) return BadRequest();

            if (mod.Type == "Books")
            {
                var resultSearch = GetBooks(mod.Name);
                return Ok(resultSearch);
            }
            if (mod.Type == "Newspapers")
            {
                var resultSearch = GetNewsPaper(mod.Name);
                return Ok(resultSearch);
            }
            if (mod.Type == "Journal")
            {
                var resultSearch = GetJournal(mod.Name);
                return Ok(resultSearch);
            }
            return BadRequest("Incorrect Search Type");
        }
        private List<BookViewModel> GetBooks(string partialTitle)
        {
            _bookService = new BookService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());

            var SearchList = (from m in _bookService.GelAllBooks()
                              select m).ToList();
            var resultSearch = SearchList.Where(s => s.Title.Contains(partialTitle)).ToList();
            return resultSearch;
        }
        private List<NewsPaperViewModel> GetNewsPaper(string partialTitle)
        {
            _newsPaperService = new NewsPaperService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
            var SearchList = (from m in _newsPaperService.ShowNewsPapers()
                              select m).ToList();
            var resultSearch = SearchList.Where(s => s.Title.Contains(partialTitle)).ToList();
            return resultSearch;
        }
        private List<JournalViewModel> GetJournal(string partialTitle)
        {
            _journalService = new JournalService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());

            var SearchList = (from m in _journalService.GetAll()
                              select m).ToList();
            var resultSearch = SearchList.Where(s => s.Title.Contains(partialTitle)).ToList();
            return resultSearch;
        }
    }
}
