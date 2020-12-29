using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyOperate.Common.Enums;

namespace EasyOperate.Web.Models.AccessControl
{
    /// <summary>
    /// 设备节点
    /// </summary>
    [Table("AccessControlEquipmentNodes")]
    public class AccessControlEquipmentNodeModel : BaseModel
    {
        /// <summary>
        /// 所属设备ID
        /// </summary>
        public int AccessControlEquipmentId { get; set; }
        public EquipmentTypeEnum equipmentTypeEnum { get; set; }
        /// <summary>
        /// 单元ID
        /// </summary>
        public int HousePartId { get; set; }
        /// <summary>
        /// 门禁名
        /// </summary>
        public string Name { get; set; }
        /// <summary> 
        /// 
        /// 节点号 设备根据节点数量生成的节点序号，值(0-63) 对应实际楼层1-64
        /// </summary>
        public int NodeNumber { get; set; }
        /// <summary>
        /// 门禁节点 节点64位长度 请注意永远!=0;
        /// </summary>
        public ulong Node { get; set; }
        public void SetEquipmentNodeValue(int nodeNumber)
        {
            Node = (ulong)1 << nodeNumber;
            NodeNumber = nodeNumber;
        }
    }

    public class AccessControlEquipmentNodeDbContext : EasyOperateBaseDbContext
    {
        public DbSet<AccessControlEquipmentNodeModel> AccessControlEquipmentNode { get; set; }
    }
}
