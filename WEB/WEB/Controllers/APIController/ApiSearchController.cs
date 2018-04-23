using BLL.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model.Models;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Newtonsoft.Json;

namespace WEB.Controllers.APIController
{
    [System.Web.Http.RoutePrefix("api/ApiSearch")]
    public class ApiSearchController : ApiController
    {        
        private SearchService _searchService;
        public ApiSearchController()
        {
            _searchService = new SearchService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        }
        [HttpGet]
        public async Task<IHttpActionResult> SearchJournal(string anotherParam)
        {
            try
            {
                if (String.IsNullOrEmpty(anotherParam)) throw new FormatException();
                var resultSearch = await _searchService.FindJournalByTitleAsync(anotherParam);
                return Ok(resultSearch);
            }
            catch (Exception ex)
            {
                return BadRequest("Error" + ex.Message);
            }
        }
        [HttpGet]
        public async Task<IHttpActionResult> SearchBooks(string anotherParam)
        {
            try
            {
                if (String.IsNullOrEmpty(anotherParam)) throw new FormatException();
                var resultSearch = await _searchService.FindBookByTitleAsync(anotherParam);
                return Ok(resultSearch);
            }
            catch (Exception ex)
            {
                return BadRequest("Error" + ex.Message);
            }
        }
        [HttpGet ]
        public async Task<IHttpActionResult> SearchNewspapers(string anotherParam)
        {
            try
            {
                if (String.IsNullOrEmpty(anotherParam)) throw new FormatException();
                var resultSearch = await _searchService.FindNewsPaperByTitleAsync(anotherParam);
                return Ok(resultSearch);
            }
            catch (Exception ex)
            {
                return BadRequest("Error"+ ex.Message);
            }
        }
    }
}
