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
        public SearchService(string _connectionString)
        {
            _journalService = new JournalService(_connectionString);
            _bookService = new BookService(_connectionString);
            _newsPaperService = new NewsPaperService(_connectionString);
        }
        public async Task<List<BookViewModel>> FindBookByTitleAsync(string partialTitle)
        {           
            var resultSearch = await _bookService.FirndByTitleAsync(partialTitle);
            return resultSearch;
        }
        public async Task<List<NewsPaperViewModel>> FindNewsPaperByTitleAsync(string partialTitle)
        {           
            var resultSearch = await _newsPaperService.FirndByTitleAsync(partialTitle);
            return resultSearch;
        }
        public async Task<List<JournalViewModel>> FindJournalByTitleAsync(string partialTitle)
        {
            var resultSearch = await _journalService.FirndByTitleAsync(partialTitle);
            return resultSearch;
        }
    }
}
