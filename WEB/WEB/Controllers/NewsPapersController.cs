using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using BLL.Models;
using System.Net;
using System.Configuration;

namespace WEB.Controllers
{
    public class NewsPapersController : Controller
    {
        NewsPaperService paperService;
        PaperPublishHouseService publishHouseService;
        public NewsPapersController() :base()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString();
            paperService = new NewsPaperService(connectionString);
            publishHouseService = new PaperPublishHouseService(connectionString);
            var t = publishHouseService.ShowPublishHouse().ToList();
        }
        public PartialViewResult Index()
        {           
            var papers = paperService.ShowNewsPapers();
            return PartialView("Index", papers);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tempNewsPaper = paperService.GetNewsPaper(id);

            if (tempNewsPaper == null)
            {
                return HttpNotFound();
            }                    
            return View(tempNewsPaper);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paperService.DeleteNewsPaper(id);
            return RedirectToAction("Index", "Base");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tempNewPaper = paperService.GetNewsPaper(id);

            if (tempNewPaper == null)
            {
                return HttpNotFound();
            }

            return View(tempNewPaper);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditNewsPaperViewModel newsPaper)
        {
            if (ModelState.IsValid)
            {
                paperService.UpdateNewsPapers(newsPaper);
                return RedirectToAction("Index", "Base");
            }
            return View();
        }
        public ActionResult Create()
        {
            var tempNewsPaper = paperService.BeforeCreateNewsPaper();
            return View(tempNewsPaper);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewsPaperViewModel newsPaper)
        {
            if (ModelState.IsValid)
            {
                paperService.CreateNewsPaper(newsPaper);
                return RedirectToAction("Index", "Base");
            }
            return View();
        }
        //--------------------------------------------------------------
        //public JsonResult Test([DataSourceRequest]DataSourceRequest request)
        //{
        //    var temp = Json(b.GelAllBooks(), JsonRequestBehavior.AllowGet);
        //    return temp;
        //}
    }
}