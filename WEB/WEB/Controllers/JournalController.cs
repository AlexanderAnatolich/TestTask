using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using BLL.Models;
using System.Configuration;
using System.Net;

namespace WEB.Controllers
{
    public class JournalController : Controller
    {
        JournalService _journalService;
        public JournalController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString();
            _journalService = new JournalService(connectionString);
        }
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult GetSearchResult(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JournalViewModel result = _journalService.Get(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return PartialView(result);
        }
    }
}