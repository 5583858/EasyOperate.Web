using System;
using EasyOperate.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace EasyOperate.Web.Models.AccessControl
{
    [Table("AccessControlAuthorizedRecords")]
    public class AccessControlAuthorizedRecordModel : BaseModel
    {
        #region 记录信息
        /************ 用于显示  ***********************************/
        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Display(Name = "性别")]
        public GenderEnum Gender { get; set; }
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

        [Display(Name = "操作员")]
        public string OperationUserName { get; set; }

        [Display(Name = "发生时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "更新时间")]
        public DateTime UpTime { get; set; }

        [Display(Name = "有效开始时间")]
        public DateTime StartTime { get; set; }

        [Display(Name = "有效结束时间")]
        public DateTime EndTime { get; set; }

        [Display(Name = "授权列表")]
        public List<string> Authorizeds { get; set; }
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
        /// 卡类型
        /// </summary>       
        public CardTypeEnum CardType { get; set; }
        /// <summary>
        ///操作用户ID 
        /// </summary>      
        public string OperationUserId { get; set; }
        #endregion
    }
    public class AccessControlAuthorizedRecordDbContext : EasyOperateBaseDbContext
    {
        public DbSet<AccessControlAuthorizedRecordModel> AccessControlAuthorizedRecord { get; set; }
    }
}