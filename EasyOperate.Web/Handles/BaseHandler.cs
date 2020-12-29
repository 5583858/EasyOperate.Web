using EasyOperate.Web.Models;
using System.Web.Mvc;
namespace EasyOperate.Web.Handle
{
[Authorize]
    public class BaseHandel
    {
        protected EfDbContext db;
        public BaseHandel()
        {
            db = new EfDbContext();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
    }
}