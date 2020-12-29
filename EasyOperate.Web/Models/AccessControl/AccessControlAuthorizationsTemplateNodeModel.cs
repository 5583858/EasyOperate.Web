using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EasyOperate.Web.Models.AccessControl
{
    /// <summary>
    /// 门禁卡授权的节点
    /// </summary>
    [Table("AccessControlAuthorizationsTemplateNodes")]
    public class AccessControlAuthorizationsTemplateNodeModel : BaseModel
    {
        public int TemplateId { get; set; }
        public int NodeId { get; set; }
    }

    public class AccessControlAuthorizationsTemplateNodeDbContext : EasyOperateBaseDbContext
    {
        public DbSet<AccessControlAuthorizationsTemplateNodeModel> AccessControlAuthorizationsTemplateNode { get; set; }
    }
}