using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EasyOperate.Web.Models.AccessControl
{
    /// <summary>
    /// 门禁卡授权的节点
    /// </summary>
    [Table("AccessControlAuthorizationTemplates")]
    public class AccessControlAuthorizationTemplateModel : BaseModel
    {
        /// <summary>
        /// 方案名
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 所属项目ID，自动授权时用
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// 所属分区ID，自动授权时用
        /// </summary>
        public int SubRegionId { get; set; }
        /// <summary>
        /// 所属楼ID，自动授权时用
        /// </summary>
        public int HouseId { get; set; }
        /// <summary>
        /// 所属单元ID，自动授权时用
        /// </summary>
        public int HousePartId { get; set; }
        /// <summary>
        /// 所属楼层ID，自动授权时用
        /// </summary>
        public int FloorId { get; set; }
        ///// <summary>
        ///// 设备节点列表
        ///// </summary>
        //public  string NodeList { get; set; }
    }

    public class AccessControlAuthorizationTemplateDbContext : EasyOperateBaseDbContext
    {
        public DbSet<AccessControlAuthorizationTemplateModel> AccessControlAuthorizationTemplate { get; set; }
    }
}