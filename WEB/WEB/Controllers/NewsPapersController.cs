using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using Model.Models;
using System.Net;
using System.Configuration;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    public class NewsPapersController : Controller
    {
        NewsPaperService paperService;
        public NewsPapersController() :base()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString();
            paperService = new NewsPaperService(connectionString);
        }
        public ActionResult GetSearchResult(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tempNewPaper = paperService.GetAsync(id);

            if (tempNewPaper == null)
            {
                return HttpNotFound();
            }

            return PartialView(tempNewPaper);
        }
    }
}