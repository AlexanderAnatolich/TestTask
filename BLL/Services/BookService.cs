using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using DAL.Repositories;
using DAL.Models;
using DAL.DataContext;
using Model.Models;
using AutoMapper;
using System.Net;
using System.Web.Mvc;
using DAL.DTO;

namespace BLL.Services
{
    public class BookService
    {
        private BookRepository _bookRepository;
        private GenerRepository _generRepository;
        private BookGenerRelationsRepository _bookGenerRelationsRepository;

        public BookService(string connectionString)
        {
            _bookRepository = new BookRepository(connectionString);
            _generRepository = new GenerRepository(connectionString);
            _bookGenerRelationsRepository = new BookGenerRelationsRepository(connectionString);
        }
        public async Task<List<BookViewModel>> GelAllAsync()
        {
            var resultQuery = await _bookRepository.GetAllAsync();
            var result = Mapper.Map<List<BookViewModel>>(resultQuery.ToList());
            return result;
        }
        public async Task CreateAsync(CreateBookViewModel inputModel)
        {
            var tempBook = Mapper.Map<BookDTO>(inputModel);
            await _bookRepository.CreateAsync(tempBook);
        }
        public async Task UpdateAsync(BookViewModel tempNewsPaper)
        {
            var newsBook = new Book();
            Mapper.Map(tempNewsPaper, newsBook);
            await _bookRepository.UpdateAsync(newsBook);
        }
        public async Task<BookViewModel> GetAsync(int id)
        {
            var tempBook = await _bookRepository.FindByIdAsync(id);
            var result = Mapper.Map<BookDTO, BookViewModel>(tempBook);
            return result;
        }
        public async Task<List<BookViewModel>> FirndByTitleAsync(string title)
        {
            var tempBook = await _bookRepository.FirndByTitleAsync(title);
            var result = Mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(tempBook);
            return result;
        }
        public async Task DeleteAsync(int id)
        {
            await _bookRepository.RemoveAsync(id);
        }
        public async Task<Boolean> DeleteAsync(List<int> id)
        {
            var result = await _bookRepository.RemoveAsync(id);
            return result;
        }
    }
}