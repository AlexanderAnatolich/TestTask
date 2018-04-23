using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Threading.Tasks;
using BLL.Services;
using Model.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Configuration;
using Model.Models.DTO;

namespace WEB.Controllers.APIController
{
    [System.Web.Http.RoutePrefix("api/ApiBook")]
    public class ApiBookController : ApiController
    {
        private BookService _bookService;
        public ApiBookController()
        {
            _bookService = new BookService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        }

        [HttpPost]
        public async Task<IHttpActionResult> Read([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var tempBooks = await _bookService.GelAllAsync();
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
                await _bookService.CreateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Update(BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                await _bookService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Delete(GetIdViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                await _bookService.DeleteAsync(model.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetById(GetIdViewModel request)
        {
            try
            {
                var tempBooks = await _bookService.GetAsync(request.Id);
                return Ok(tempBooks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
