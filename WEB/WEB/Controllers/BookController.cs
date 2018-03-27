using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using Model.Models;
using System.Net;
using System.Configuration;
using Kendo.Mvc.UI;

namespace WEB.Controllers
{
    public class BookController : Controller
    {
        BookService bookService;
        public BookController()
        {
            bookService = new BookService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        }
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id)
        {
            BookViewModel book = await bookService.GetAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return PartialView(book);
        }
    }
}