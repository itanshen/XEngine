using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiCORS.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://localhost:16961", headers: "*", methods: "GET,POST,PUT,DELETE")]
    public class ChargingController : ApiController
    {
        [HttpGet]
        public string GetAllChargingData()
        {
            return "Success";
        }
    }
}
