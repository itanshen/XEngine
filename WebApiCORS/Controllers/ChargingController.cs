using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiCORS.Controllers
{
    public class ChargingController : ApiController
    {
        [HttpGet]
        public string GetAllChargingData()
        {
            return "Success";
        }
    }
}
