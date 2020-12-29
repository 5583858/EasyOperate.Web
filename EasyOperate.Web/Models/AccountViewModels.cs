using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyOperate.Common.Enums;

namespace EasyOperate.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "代码")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "记住此浏览器?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "登录名不能为空！")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "密码不能为空！")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }

    public class CustomerViewModel
    {
        public string ID { get; set; }
        #region 新建使用
        [Display(Name = "账号")]
        public string UserName { get; set; }
        ////////[Display(Name = "密码")]
        ////////[DataType(DataType.Password)]
        ////////[StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        ////////public string Password { get; set; }
        ////////[Display(Name = "确认密码")]
        ////////[DataType(DataType.Password)]
        ////////[Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        ////////public string ConfirmPassword { get; set; }
        #endregion
        [Required]
        [Display(Name = "真实姓名")]
        public string RealName { get; set; }
        [Display(Name = "性别")]
        public GenderEnum Gender { get; set; }
        [Required]
        [Display(Name = "手机号")]
        public string PhoneNumber { get; set; }
        [Display(Name = "楼号")]
        public string BuildingNumber { get; set; }
        [Display(Name = "房号")]
        public string RoomNumber { get; set; }
        [Display(Name = "身份证号")]
        public string IdentityCardNumber { get; set; }
        [Display(Name = "驾驶证号")]
        public string DrivingLicenseNumber { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "备注")]
        public string Comment { get; set; }
    }

    public class RegisterViewModel
    {
        public string ID { get; set; }

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "手机号")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "权限")]
        public string AdminRole { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }
}
