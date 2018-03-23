using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using BLL.Models;
using System.Net;
using System.Configuration;
using Kendo.Mvc.UI;

namespace WEB.Controllers
{
    public class BookController : Controller
    {
        BookService bookService;
        GenreService genreService;
        public BookController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString();
            bookService = new BookService(connectionString);
            genreService = new GenreService(connectionString);
        }
        public BookController(string connectionString)
        {
            bookService = new BookService(connectionString);
            genreService = new GenreService(connectionString);
        }
        // GET: Book
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult Create()
        {
            return RedirectToAction("");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                await bookService.CreateBookAsync(book);
                return RedirectToAction("Index", "Base");
            }
            return View(book);
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookViewModel book = await bookService.GetBookAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return PartialView(book);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(BookViewModel book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await bookService.UpdateBookAsync(book);
        //        return RedirectToAction("Index", "Base");
        //    }           
        //    return View(book);
        //}
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookViewModel book = await bookService.GetBookAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BookViewModel book = await bookService.GetBookAsync(id);
            await bookService.DeleteAsync(id);
            return RedirectToAction("Index", "Base");
        }
    }
}