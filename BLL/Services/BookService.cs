using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using DAL.Repositories;
using DAL.Models;
using DAL.DataContext;
using BLL.Models;
using AutoMapper;
using System.Net;
using System.Web.Mvc;

namespace BLL.Services
{
    public class BookService
    {
        private DataContex _modelsContext;
        private BookRepository _bookRepository;
        private GenerRepository _generRepository;
        private BookGenerRelationsRepository _bookGenerRelationsRepository;

        public BookService(string connectionString)
        {
            _modelsContext = new DataContex(connectionString);
            _bookRepository = new BookRepository(_modelsContext);
            _generRepository = new GenerRepository(_modelsContext);
            _bookGenerRelationsRepository = new BookGenerRelationsRepository(_modelsContext);
        }
        public void CreateBook(BookViewModel inputModel)
        {
            Book tempBook = Mapper.Map<BookViewModel, Book>(inputModel);
            _bookRepository.Create(tempBook);
        }
        public List<BookViewModel> GelAllBooks()
        {
            var tempBooks =  _bookRepository.Get();
            var listGeners = _generRepository.Get();

            var result = _bookGenerRelationsRepository.Get(x =>
            tempBooks.Select((y => y.Id)).Contains(x.BookId))
            .GroupBy(x => x.BookId).Select(g => new BookViewModel
            {
                Author = tempBooks.Where(n => n.Id == g.Key).Select(n => n.Author).First(),
                DateInsert = tempBooks.Where(n => n.Id == g.Key).Select(n => n.DateInsert).First(),
                Price = tempBooks.Where(n => n.Id == g.Key).Select(n => n.Price).First(),
                PublishHouse = Mapper.Map<PublishHouse, PublishHouseViewModel>(
                    tempBooks.Where(n => n.Id == g.Key).
                    Select(n => n.PublishHouse).First()),
                Title = tempBooks.Where(n => n.Id == g.Key).Select(n => n.Title).First(),
                YearOfPublish = tempBooks.Where(n => n.Id == g.Key).Select(n => n.YearOfPublish).First(),
                Id = g.Key,
                Genre = Mapper.Map<List<Gener>, List<GenerViewModel>>(listGeners.Where(b => g.Where(f => f.BookId == g.Key).
                 Select(u => u.GenreId).Contains(b.Id))
                .Select(i => i).ToList()),
            }
            ).ToList();

            return result;
        }
        public async Task<List<BookViewModel>> GelAllBooksAsync()
        {
            List<Book> tempBooks = await _bookRepository.GetAsync();
            List<Gener> listGeners = await _generRepository.GetAsync();
            
            var result = _bookGenerRelationsRepository.Get(x =>
            tempBooks.Select((y => y.Id)).Contains(x.BookId))
            .GroupBy(x => x.BookId).Select(g => new BookViewModel
            {
                Author = tempBooks.Where(n=>n.Id==g.Key).Select(n=>n.Author).First(),
                DateInsert= tempBooks.Where(n => n.Id == g.Key).Select(n => n.DateInsert).First(),
                Price=tempBooks.Where(n => n.Id == g.Key).Select(n => n.Price).First(),               
                PublishHouse = Mapper.Map<PublishHouse,PublishHouseViewModel>(
                    tempBooks.Where(n => n.Id == g.Key).
                    Select(n => n.PublishHouse).First()),
                Title= tempBooks.Where(n => n.Id == g.Key).Select(n => n.Title).First(),
                YearOfPublish = tempBooks.Where(n => n.Id == g.Key).Select(n => n.YearOfPublish).First(),
                Id = g.Key,
                Genre = Mapper.Map<List<Gener>,List<GenerViewModel>>(listGeners.Where(b => g.Where(f => f.BookId == g.Key).
                Select(u => u.GenreId).Contains(b.Id))
                .Select(i => i).ToList()),
            }
            ).ToList();
            return result;
        }
        public async Task CreateBookAsync(CreateBookViewModel inputModel)
        {
            Book tempBook = Mapper.Map<Book>(inputModel);
            tempBook.PublishHouse = null;
            var item = await _bookRepository.CreateAsync(tempBook);
            
            var relations = inputModel.Genre.Select(x => new BookGenerRelations()
            {
                BookId = item.Id,
                GenreId = x.Id
            });

            await _bookGenerRelationsRepository.CreateAsync(relations);
        }
        public void UpdateBook(BookViewModel tempNewsPaper)
        {
            var newsBook = new Book();
            Mapper.Map(tempNewsPaper, newsBook);
            _bookRepository.Update(newsBook);
        }
        public async Task UpdateBookAsync(BookViewModel tempNewsPaper)
        {
            var newsBook = new Book();
            Mapper.Map(tempNewsPaper, newsBook);
            await _bookRepository.UpdateAsync(newsBook);
        }
        public async Task<BookViewModel> GetBookAsync(int? id)
        {
            Book tempBook = await _bookRepository.FindByIdAsync(id);

            if (tempBook == null) return null;

            BookViewModel book = new BookViewModel();

            Mapper.Map(tempBook, book);

            var nm = from q in _bookGenerRelationsRepository.Get()
                     where q.BookId== book.Id
                     select q;
            var t = from q in nm
                    where q.BookId == book.Id
                    select q.Gener;
            var g = t.ToList();

            book.Genre = Mapper.Map<IEnumerable<Gener>, List<GenerViewModel>>(g);

            return book;
        }
        public BookViewModel GetBook(int? id)
        {
            Book tempBook = _bookRepository.FindById(id);

            BookViewModel book = new BookViewModel();

            Mapper.Map(tempBook, book);

            var nm = from q in _bookGenerRelationsRepository.Get()
                     where q.BookId == book.Id
                     select q;
            var t = from q in nm
                    where q.BookId == book.Id
                    select q.Gener;
            var g = t.ToList();

            book.Genre = Mapper.Map<IEnumerable<Gener>, List<GenerViewModel>>(g);

            return book;
        }
        public void Delete(int id)
        {
            var tebpBook = _bookRepository.FindById(id);
            _bookRepository.Remove(tebpBook);
        }
        public async Task DeleteAsync(int id)
        {
            var item = await _bookRepository.FindByIdAsync(id);
            await _bookRepository.RemoveAsync(item);
        }
        public async Task<Boolean> DeleteRangeAsync(List<int> id)
        {
            var result = await _bookRepository.RemoveRangeAsync(x => id.Contains(x.Id));
            return result;
        }
    }
}