using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models
{
    public class EasyOperateBaseDbContext : DbContext
    {
        public EasyOperateBaseDbContext() : base("DefaultConnection") { }
    }
}