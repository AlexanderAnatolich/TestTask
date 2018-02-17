using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace WEB.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RenderGrid()
        {
            return PartialView("RenderGrid");
        }
        public async Task<JsonResult> ReadSummaryKendoGrid([DataSourceRequest]DataSourceRequest request)
        {
            SummaryService t = new SummaryService();
            var test = await t.Calculate();
            var c = Json(test, JsonRequestBehavior.AllowGet);

            return c;
        }       
        public ActionResult ToNewsPapers()
        {
            return RedirectToAction("Index", "NewsPapers");              
        }
        public ActionResult ToPublishHouse()
        {
            return RedirectToAction("Index", "PaperPublishHouse");
        }
        public ActionResult ToBooks()
        {
            return PartialView("Index", "Book");
        }
    }
}