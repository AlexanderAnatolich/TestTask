using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.Services;
using Kendo.Mvc.UI;

namespace WEB.Controllers.APIController
{
    public class ApiPublishHouseController : ApiController
    {
        private PublishHouseService _publishHouseService;
        public ApiPublishHouseController()
        {
            _publishHouseService = new PublishHouseService(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString());
        }
        [HttpPost]
        public IHttpActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var pulishHouse = _publishHouseService.ShowPublishHouse().ToList();
                return Ok(pulishHouse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
