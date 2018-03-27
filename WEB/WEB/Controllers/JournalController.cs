using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using Model.Models;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    public class JournalController : Controller
    {
        JournalService _journalService;
        public JournalController()
        {
            _journalService = new JournalService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        }
        public async Task<ActionResult> GetSearchResult(int id)
        {
            JournalViewModel result = await _journalService.Get(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return PartialView(result);
        }
    }
}