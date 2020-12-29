using System;
using EasyOperate.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace EasyOperate.Web.Models.AccessControl
{
    [Table("AccessControlRecords")]
    public class AccessControlRecordModel : BaseModel
    {
        #region 记录信息
        /************ 用于显示  ***********************************/
        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Display(Name = "性别")]
        public GenderEnum Gender { get; set; }

        [Display(Name = "项目名")]
        public int ProjectName { get; set; }

        [Display(Name = "分区名")]
        public int SubRegionName { get; set; }

        [Display(Name = "楼号")]
        public string HouseNumber { get; set; }

        [Display(Name = "单元号")]
        public string HousePartName { get; set; }

        [Display(Name = "楼层")]
        public string FloorName { get; set; }

        [Display(Name = "房号")]
        public string RoomNumber { get; set; }

        [Display(Name = "设备名")]
        public string AccessControlEquipmentName { get; set; }

        [Display(Name = "门禁名")]
        public string NodeName { get; set; }

        [Display(Name = "卡类型")]
        public string CardTypeName { get; set; }

        [Display(Name = "进出")]
        public DirectionEnum Direction { get; set; }

        [Display(Name = "开门方式")]
        public string DoorOpeningTypeName { get; set; }

        [Display(Name = "用户名")]
        public string OperationUserName { get; set; }

        [Display(Name = "发生时间")]
        public DateTime CreateTime { get; set; }
        /************ 用于筛选  ***********************************/
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>        
        public int ProjectId { get; set; }
        /// <summary>
        /// 分区ID
        /// </summary>        
        public int SubRegionId { get; set; }
        /// <summary>
        /// 楼ID
        /// </summary>       
        public int HouseId { get; set; }
        /// <summary>
        /// 单元ID
        /// </summary>        
        public int HousePartId { get; set; }
        /// <summary>
        /// 楼层ID
        /// </summary>        
        public int FloorId { get; set; }
        /// <summary>
        /// 房间ID
        /// </summary>        
        public int RoomId { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>        
        public string AccessControlEquipmentId { get; set; }
        /// <summary>
        /// 门禁ID
        /// </summary>        
        public int NodeId { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>       
        public CardTypeEnum CardType { get; set; }
        /// <summary>
        /// 开门类型
        /// </summary>      
        public DoorOpeningTypeEnum DoorOpeningType { get; set; }
        /// <summary>
        ///操作用户ID 
        /// </summary>      
        public string OperationUserId { get; set; }
        #endregion
    }
    public class AccessControlRecordDbContext : EasyOperateBaseDbContext
    {
        public DbSet<AccessControlRecordModel> AccessControlRecord { get; set; }
    }
}