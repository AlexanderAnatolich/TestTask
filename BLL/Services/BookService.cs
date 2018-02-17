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
        public BookService(string connectionString)
        {
            _modelsContext = new DataContex(connectionString);
            _bookRepository = new BookRepository(_modelsContext);
            _generRepository = new GenerRepository(_modelsContext);
        }
        public CreateBookViewModel BeforeCreateBook()
        {
            var tempBookModel = new CreateBookViewModel();

            tempBookModel.ListGeners = new SelectList(_generRepository.Get(), "Id", "Genre");
            tempBookModel.DateInsert = DateTime.Now.Date;

            return tempBookModel;
        }
        public async Task CreateBookAsync(CreateBookViewModel cNPDTO)
        {
            Book tempPaper = Mapper.Map<CreateBookViewModel,Book> (cNPDTO);

            await _bookRepository.CreateAsync(tempPaper);
        }
        public void CreateBook(CreateBookViewModel cNPDTO)
        {
            Book tempPaper = Mapper.Map<CreateBookViewModel, Book>(cNPDTO);

            _bookRepository.Create(tempPaper);
        }
        public IEnumerable<BookViewModel> GelAllBooks()
        {
            IEnumerable<Book> tempBooks = _bookRepository.Get();
            return Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(tempBooks);
        }
        public async Task<IEnumerable<BookViewModel>> GelAllBooksAsync()
        {
            IEnumerable<Book> tempBooks = await _bookRepository.GetAsync();
            return Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(tempBooks);
        }
        public void UpdateBook(EditBookViewModel tempNewsPaper)
        {
            var newsBook = new Book();
            Mapper.Map(tempNewsPaper, newsBook);
            _bookRepository.Update(newsBook);
        }
        public async Task UpdateBookAsync(EditBookViewModel tempNewsPaper)
        {
            var newsBook = new Book();
            Mapper.Map(tempNewsPaper, newsBook);
            await _bookRepository.UpdateAsync(newsBook);
        }
        public EditBookViewModel GetBook(int? id)
        {
            Book tempBook = _bookRepository.FindById(id);
            EditBookViewModel book = new EditBookViewModel();

            Mapper.Map(tempBook, book);

            book.ListGeners = new SelectList(_generRepository.Get(), "Id", "Genre");

            return book;
        }
        public async Task<EditBookViewModel> GetBookAsync(int? id)
        {
            Book tempBook = await _bookRepository.FindByIdAsync(id);
            EditBookViewModel book = new EditBookViewModel();

            Mapper.Map(tempBook, book);

            book.ListGeners = new SelectList(await _generRepository.GetAsync(), "Id", "Genre");

            return book;
        }
        public void Delete(int id)
        {
            var tebpBook = _bookRepository.FindById(id);
            _bookRepository.Remove(tebpBook);
        }
        public async Task DeleteAsync(int id)
        {
            var tebpBook = await _bookRepository.FindByIdAsync(id);
            await _bookRepository.RemoveAsync(tebpBook);
        }
    }
}