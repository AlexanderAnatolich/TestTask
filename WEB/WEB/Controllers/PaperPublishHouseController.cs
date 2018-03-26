using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using Model.Models;
using System.Net;
using System.Configuration;

namespace WEB.Controllers
{
    public class PublishHouseController : Controller
    {
        private PublishHouseService _publishHouseService;
        public PublishHouseController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString();
            _publishHouseService = new PublishHouseService(connectionString);
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
            PublishHouseViewModel paperPublishHous = _publishHouseService.GetPublishHouse(id);
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
        public ActionResult Create(CreatePublishHousViewModel paperPublishHous)
        {
            if (ModelState.IsValid)
            {
                _publishHouseService.CreatePublichHouse(paperPublishHous);               
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
            PublishHouseViewModel paperPublishHous = _publishHouseService.GetPublishHouse(id);
            if (paperPublishHous == null)
            {
                return HttpNotFound();
            }
            return View(paperPublishHous);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PublishHouseViewModel paperPublishHous)
        {
            if (ModelState.IsValid)
            {
                _publishHouseService.UpdatePublishHouse(paperPublishHous);
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
            var paperPublishHous = _publishHouseService.GetPublishHouse(id);
            if (paperPublishHous == null)
            {
                return HttpNotFound();
            }
            return View(paperPublishHous);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(PublishHouseViewModel model)
        {
            _publishHouseService.DeletePublishHouse(model.Id);
            return RedirectToAction("Index", "Base");
        }
    }
}