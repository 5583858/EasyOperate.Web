using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections.Generic;

namespace EasyOperate.Web.Models.AccessControl
{
    /// <summary>
    /// 门禁卡授权的节点
    /// </summary>
    [Table("AccessControlCardAuthorizeds")]
    public class AccessControlCardAuthorizedModel : BaseModel
    {
        public int EquipmentId { get; set; }
        public int CardId { get; set; }
        /// <summary>
        /// 代表设备下所有点节
        /// </summary>
        public ulong Nodes { get; set; }
        /// <summary>
        /// 是否下载到设备
        /// </summary>
        public bool IsDistributionSuccess { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpTime { get; set; }
        public void SetCardAuthorizedNodes(List< AccessControlEquipmentNodeModel> EquipmentNodes)
        {
            foreach(AccessControlEquipmentNodeModel EquipmentNode in EquipmentNodes)
            {
                this.Nodes |= EquipmentNode.Node;
            }           
        }
    }

    public class AccessControlCardAuthorizedDbContext : EasyOperateBaseDbContext
    {
        public DbSet<AccessControlCardAuthorizedModel> AccessControlCardAuthorized { get; set; }
    }
}