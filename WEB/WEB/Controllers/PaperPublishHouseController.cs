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
    public class PaperPublishHouseController : Controller
    {
        private PaperPublishHouseService _publishHouseService;
        public PaperPublishHouseController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString();
            _publishHouseService = new PaperPublishHouseService(connectionString);
        }
        public PartialViewResult Index()
        {
            var x = _publishHouseService.ShowPublishHouse();
            return PartialView(x);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperPublishHouseViewModel paperPublishHous = _publishHouseService.GetPaperPublishHosuse(id);
            if (paperPublishHous == null)
            {
                return HttpNotFound();
            }
            return View(paperPublishHous);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePaperPublishHousViewModel paperPublishHous)
        {
            if (ModelState.IsValid)
            {
                _publishHouseService.CreatePaperPublichHouse(paperPublishHous);               
                return RedirectToAction("Index","Base");
            }
            return View(paperPublishHous);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperPublishHouseViewModel paperPublishHous = _publishHouseService.GetPaperPublishHosuse(id);
            if (paperPublishHous == null)
            {
                return HttpNotFound();
            }
            return View(paperPublishHous);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPaperPublishHouseViewModel paperPublishHous)
        {
            if (ModelState.IsValid)
            {
                _publishHouseService.UpdatePaperPublishHouse(paperPublishHous);
                return RedirectToAction("Index", "Base");
            }
            return View(paperPublishHous);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var paperPublishHous = _publishHouseService.GetPaperPublishHosuse(id);
            if (paperPublishHous == null)
            {
                return HttpNotFound();
            }
            return View(paperPublishHous);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(PaperPublishHouseViewModel model)
        {
            _publishHouseService.DeletePublishHouse(model.Id);
            return RedirectToAction("Index", "Base");
        }
    }
}