using Model.Models;
using Model.Models.DTO;
using BLL.Services;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WEB.Controllers.APIController
{
    public class ApiNewsPaperController : ApiController
    {
        private NewsPaperService _newsPaperService;
        public ApiNewsPaperController()
        {
            _newsPaperService = new NewsPaperService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        }

        [HttpPost]
        public async Task<IHttpActionResult> Read([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var tempPaper = await _newsPaperService.GetAllAsync();
                tempPaper = tempPaper.ToList();
                return Ok(tempPaper);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateNewsPaperViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                await _newsPaperService.CreateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Update(NewsPaperViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                await _newsPaperService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Delete(GetIdViewModel Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                await _newsPaperService.DeleteAsync(Id.Id);
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
                var tempBooks = await _newsPaperService.GetAsync(request.Id);
                return Ok(tempBooks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
