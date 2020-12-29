using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EasyOperate.Common.Enums;

namespace EasyOperate.Web.Models.CustomerModel
{
    public class DetailsViewModel
    {
        /// <summary>
        /// 房号ID
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        ////////////////////////////////////////////////////////

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
        public string ImgUrl { get; set; }
    }
}