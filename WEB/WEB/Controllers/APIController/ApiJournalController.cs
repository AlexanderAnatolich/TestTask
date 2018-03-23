﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BLL.Services;
using DAL.DataContext;
using DAL.Models;
using BLL.Models;
using BLL.Models.DTO;
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
                var tempPaper = _journalService.GetAll();
                return Ok(tempPaper);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IHttpActionResult Create(CreateJournalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                _journalService.Create(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IHttpActionResult Update(JournalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Send Data incorrect!");
            }
            try
            {
                _journalService.Update(model);
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
                await _journalService.DeleteRange(model.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}