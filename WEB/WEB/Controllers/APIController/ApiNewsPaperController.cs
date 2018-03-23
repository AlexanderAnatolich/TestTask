using BLL.Models;
using BLL.Models.DTO;
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
        public IHttpActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var tempPaper = _newsPaperService.ShowNewsPapers();
                return Ok(tempPaper);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IHttpActionResult Create(CreateNewsPaperViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                _newsPaperService.CreateNewsPaper(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IHttpActionResult Update(NewsPaperViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                _newsPaperService.UpdateNewsPapers(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Delete(DeleteDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                await _newsPaperService.DeleteRangeNewsPaper(model.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
