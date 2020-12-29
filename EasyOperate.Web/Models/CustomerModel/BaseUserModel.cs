using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyOperate.Common.Enums;
using System.Data.Entity;
using System.Linq;
using System;

namespace EasyOperate.Web.Models
{
    [Table("BaseUsers")]
    public class BaseUserModel:BaseModel
    {
        public string IdentityUserId { get; set; }
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
        /// 房号ID
        /// </summary>
        public int RoomId { get; set; }

        ////////////////////////////////////////////////////////
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Display(Name = "性别")]
        public GenderEnum Gender { get; set; }

        [Display(Name = "身份证号")]
        public string IdentityCardNumber { get; set; }

        //[Display(Name = "驾驶证号")]
        //public string DrivingLicenseNumber { get; set; }

        [Display(Name = "电话号码")]
        public string PhoneNumber { get; set; }

        [Display(Name = "通信地址")]
        public string Address { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "更改时间")]
        public DateTime? UpdateTime { get; set; }
    }
    public class BaseUserModelDbContext : EasyOperateBaseDbContext
    {
        public DbSet<BaseUserModel> BaseUser { get; set; }
    }
    public class BaseUserInit : CreateDatabaseIfNotExists<BaseUserModelDbContext>
    {
        protected override void Seed(BaseUserModelDbContext context)
        {
            InitSetup(context);
            base.Seed(context);
        }

        public void InitSetup(BaseUserModelDbContext context)
        {
            BaseUserModel model = new BaseUserModel();
            EfDbContext efDbContext = new EfDbContext();
            ApplicationUser applicationUser = efDbContext.Users.FirstOrDefault();
            model.IdentityUserId = applicationUser.Id;
            model.Email = applicationUser.Email;
            model.PhoneNumber = applicationUser.PhoneNumber;
            model.CreateTime = applicationUser.CreateTime;
            model.UpdateTime = applicationUser.UpdateTime;
            model.RealName = applicationUser.UserName;
            context.BaseUser.Add(model);
        }
    }
}