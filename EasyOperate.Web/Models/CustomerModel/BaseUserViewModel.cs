using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyOperate.Common.Enums;
using System.Data.Entity;
using System;
using System.Web;

namespace EasyOperate.Web.Models
{
    public class BaseUserViewModel
    {
        public string IdentityUserId { get; set; }
        /// <summary>
        /// 分区ID
        /// </summary>
        public int SubRegionId { get; set; }

        /// <summary>
        /// 分区
        /// </summary>
        public string SubRegion { get; set; }
        /// <summary>
        /// 楼ID
        /// </summary>
        public int HouseId { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
        public string House { get; set; }
        /// <summary>
        /// 单元ID
        /// </summary>
        public int HousePartId { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
        public string HousePart { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int FloorId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 房号ID
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// 房间
        /// </summary>
        public string Room { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }


        ////////////////////////////////////////////////////////
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 自动授权
        /// </summary>
        public bool AutoAuthorizeds { get; set; }
        /// <summary>
        /// 通权
        /// </summary>
        public bool AllAuthorizeds { get; set; }

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
        //public HttpPostedFileBase Image { get; set; }
    }
}