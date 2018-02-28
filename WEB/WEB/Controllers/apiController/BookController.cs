using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Threading.Tasks;
using BLL.Services;
using BLL.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Configuration;

namespace WEB.Controllers.apiController
{
    [System.Web.Http.RoutePrefix("api/ApiBook")]
    public class ApiBookController : ApiController
    {
        BookService _bookService;
        public ApiBookController()
        {
            _bookService = new BookService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        }

        [HttpPost]
        public async Task<IHttpActionResult> Read([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var tempBooks = await _bookService.GelAllBooksAsync();
                return Ok(tempBooks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                await _bookService.CreateBookAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
