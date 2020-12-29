using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyOperate.Common;
namespace EasyOperate.Web.Controllers
{
    [Authorize(Roles = RoleType.ADMIN + "," + RoleType.SYSTEM)]
    public class HomeController : EasyOperateBaseController
    {
        //[AllowAnonymous]
        [Authorize]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}