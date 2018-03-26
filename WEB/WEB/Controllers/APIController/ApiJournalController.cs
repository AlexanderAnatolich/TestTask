﻿using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.Services;
using Model.Models;
using Model.Models.DTO;
using Kendo.Mvc.UI;

namespace WEB.Controllers.APIController
{
    public class ApiJournalController : ApiController
    {
        private JournalService _journalService; 
        
        public ApiJournalController()
        {
            _journalService = new JournalService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());

        }
        [HttpPost]
        public IHttpActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var tempPaper = _journalService.GetAllAsync();
                return Ok(tempPaper);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateJournalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                await _journalService.CreateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Update(JournalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                await _journalService.UpdateAsync(model);
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
                await _journalService.DeleteRangeAsync(model.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}