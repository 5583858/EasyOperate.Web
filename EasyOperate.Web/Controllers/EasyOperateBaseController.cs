using EasyOperate.Web.Models;
using System.Web.Mvc;
namespace EasyOperate.Web.Controllers
{
[Authorize]
    public class EasyOperateBaseController : Controller
    {
        protected EfDbContext db;
        public EasyOperateBaseController()
        {
            db = new EfDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}