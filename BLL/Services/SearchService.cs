using Model.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SearchService
    {
        private BookService _bookService;
        private NewsPaperService _newsPaperService;
        private JournalService _journalService;

        public async Task<List<BookViewModel>> GetBooks(string partialTitle)
        {
            _bookService = new BookService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
            var books = await _bookService.GelAllAsync();
            var resultSearch = books.Where(s => s.Title.Contains(partialTitle)).ToList();
            return resultSearch;
        }
        public async Task<List<NewsPaperViewModel>> GetNewsPaper(string partialTitle)
        {
            _newsPaperService = new NewsPaperService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
            var newsPaper = await _newsPaperService.GetAllAsync();
            var resultSearch = newsPaper.Where(s => s.Title.Contains(partialTitle)).ToList();
            return resultSearch;
        }
        public async Task<List<JournalViewModel>> GetJournal(string partialTitle)
        {
            _journalService = new JournalService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());

            var allJournal = await _journalService.GetAllAsync();
            var SearchList = (from m in allJournal
                              select m).ToList();
            var resultSearch = SearchList.Where(s => s.Title.Contains(partialTitle)).ToList();
            return resultSearch;
        }
    }
}
