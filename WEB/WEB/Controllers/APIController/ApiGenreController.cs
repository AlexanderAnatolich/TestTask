using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BLL.Services;
using Model.Models;
using System.Configuration;
using System.Threading.Tasks;
using Kendo.Mvc.UI;

namespace WEB.Controllers.APIController
{
    [System.Web.Http.RoutePrefix("api/ApiGenre")]
    public class ApiGenreController:ApiController
    {
        private GenreService _genreService;

        public ApiGenreController()
        {
            _genreService = new GenreService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        }

        [HttpPost]
        public IHttpActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var tempBooks = _genreService.ShowAllGeners().ToList();
                return Ok(tempBooks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}