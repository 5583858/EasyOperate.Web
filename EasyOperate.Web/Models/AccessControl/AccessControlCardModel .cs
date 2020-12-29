using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EasyOperate.Web.Models.AccessControl
{
    /// <summary>
    /// 门禁卡
    /// </summary>
    [Table("AccessControlCards")]
    public class AccessControlCardModel : BaseModel
    {

        [Display(Name = "客户ID")]
        public int BaseUserId { get; set; }

        [Display(Name = "卡类型ID")]
        public int CardTypeID { get; set; }
        public string CardNo { get; set; }

        private DateTime aCEndTime;
        
        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadImagePath { get; set; }

        [Display(Name = "更新时间")]
        public DateTime AccessControlUpTime { get; set; }

        [Display(Name = "开始时间")]
        public DateTime AccessControlStartTime { get; set; }

        [Display(Name = "结束时间")]
        public DateTime AccessControlEndTime 
        {
            get
            {
                return aCEndTime;
            }
            set
            {
                aCEndTime = value;
                AccessControlAvailableDays = aCEndTime.Subtract(DateTime.Now).Days;
            }
        }
        /// <summary>
        ///门禁剩余可用天数
        /// </summary>
        [NotMappedAttribute]
        public int AccessControlAvailableDays { get; set; }
    }

    public class AccessControlCardDbContext : EasyOperateBaseDbContext
    {
        public DbSet<AccessControlCardModel> AccessControlCard { get; set; }
    }
}