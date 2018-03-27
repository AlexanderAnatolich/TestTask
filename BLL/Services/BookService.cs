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
        public async Task<IQueryable<BookViewModel>> GelAllAsync()
        {
            var result = await _bookRepository.GetAllAsync();
            return result;
        }
        public async Task CreateAsync(CreateBookViewModel inputModel)
        {
            var tempBook = Mapper.Map<Book>(inputModel);
            var item = await _bookRepository.CreateAsync(tempBook);
            
            var relations = inputModel.Genre.Select(x => new BookGenerRelations()
            {
                BookId = item.Id,
                GenreId = x.Id
            });

            await _bookGenerRelationsRepository.CreateAsync(relations);
        }
        public async Task UpdateAsync(BookViewModel tempNewsPaper)
        {
            var newsBook = new Book();
            Mapper.Map(tempNewsPaper, newsBook);
            await _bookRepository.UpdateAsync(newsBook);
        }
        public async Task<BookViewModel> GetAsync(int id)
        {
            Book tempBook = await _bookRepository.FindByIdAsync(id);

            if (tempBook == null) return null;

            var book = new BookViewModel();

            Mapper.Map(tempBook, book);
            var resultSelect = await _bookGenerRelationsRepository.GetAllAsync();
            var nm = from q in resultSelect
                     where q.BookId== book.Id
                     select q;
            var t = from q in nm
                    where q.BookId == book.Id
                    select q.Gener;
            var g = t.ToList();
            book.Genre = Mapper.Map<IEnumerable<Gener>, List<GenerViewModel>>(g);
            return book;
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