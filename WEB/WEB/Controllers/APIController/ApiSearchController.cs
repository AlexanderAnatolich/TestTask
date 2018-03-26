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

namespace WEB.Controllers.APIController
{
    [System.Web.Http.RoutePrefix("api/ApiSearch")]
    public class ApiSearchController : ApiController
    {        
        private SearchService _searchService;
        public ApiSearchController()
        {
            _searchService = new SearchService();
        }
        [HttpPost]
        public IHttpActionResult Search([FromBody]SearchModel mod)
        {
            if (String.IsNullOrEmpty(mod.Name)) return BadRequest();

            if (mod.Type == "Books")
            {
                var resultSearch = _searchService.GetBooks(mod.Name);
                return Ok(resultSearch);
            }
            if (mod.Type == "Newspapers")
            {
                var resultSearch = _searchService.GetNewsPaper(mod.Name);
                return Ok(resultSearch);
            }
            if (mod.Type == "Journal")
            {
                var resultSearch = _searchService.GetJournal(mod.Name);
                return Ok(resultSearch);
            }
            return BadRequest("Incorrect Search Type");
        }                     
    }
}
