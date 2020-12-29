using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyOperate.Common.Enums;

namespace EasyOperate.Web.Models.AccessControl
{
    /// <summary>
    /// 门禁设备信息表
    /// </summary>
    [Table("AccessControlEquipments")]
    public class AccessControlEquipmentModel : BaseModel
    {
        [Display(Name = "所属分区")]
        public string SubRegionName { get; set; }

        [Display(Name = "楼号")]
        public string HouseName { get; set; }

        [Display(Name = "单元号")]
        public string HousePartName { get; set; }

        [Display(Name = "设备名称")]
        public string Name { get; set; }

        [Display(Name = "设备IP")]
        public string IP { get; set; }

        [Display(Name = "设备端口")]
        public int Port { get; set; }

        [Display(Name = "设备序列号")]
        public string Serialno { get; set; }

        [Display(Name = "出入口")]
        public DirectionEnum Direction { get; set; }

        [Display(Name = "设备类型")]
        public EquipmentTypeEnum EquipmentType { get; set; }

        [Display(Name = "控制器IP")]
        public string SubControllerIP { get; set; }

        [Display(Name = "控制器端口")]
        public  int SubControllerPort { get; set; }

        [Display(Name = "通道数量")]
        public int NodeCount{ get; set; }

        /// <summary>
        /// 门禁名前缀
        /// </summary>
        [Display(Name = "门禁点前缀")]
        public string EquipmentNodePrefix { get; set; }

        /// <summary>
        /// 门禁名后缀
        /// </summary>
        [Display(Name = "门禁名后缀")]
        public string EquipmentNodeSuffix { get; set; }
        /// <summary>
        /// 分区Id
        /// </summary>
        public int SubRegionId { get; set; }
        /// <summary>
        /// 楼Id
        /// </summary>
        public int HouseId { get; set; }
        /// <summary>
        /// 单元Id
        /// </summary>
        public int HousePartId { get; set; }
        /// <summary>
        /// 设备父级ID 0：主机
        /// </summary>
        public int ParentId { get; set; }      
        /// <summary>
        /// 设备状态
        /// </summary>
        public RunningStateEnum state { get; set; }
    }

    public class AccessControlEquipmentDbContext : EasyOperateBaseDbContext
    {
        public DbSet<AccessControlEquipmentModel> AccessControlEquipment { get; set; }
    }
}
