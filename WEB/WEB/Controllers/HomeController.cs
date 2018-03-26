using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace WEB.Controllers
{
    public class HomeController : Controller
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
            var result = Json(test, JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}