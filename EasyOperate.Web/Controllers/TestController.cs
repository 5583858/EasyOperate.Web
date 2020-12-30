using EasyOperate.Web.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyOperate.Web.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("GetDeviceInfo")]
        public IHttpActionResult GetDeviceInfo()
        {
            DeviceBasicInfoManager deviceBasicInfoManager = new DeviceBasicInfoManager();
            deviceBasicInfoManager.GetInfo();

            return Ok();
        }

        [HttpGet]
        [Route("AddPeople")]
        public IHttpActionResult AddPeople()
        {
            //PeopleManager peopleManager = new PeopleManager();
            //peopleManager.AddPeopleInfo();

            return Ok();
        }
    }
}
