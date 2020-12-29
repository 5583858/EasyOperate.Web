using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyOperate.Common.Enums;
using EasyOperate.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyOperate.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "更改时间")]
        public DateTime? UpdateTime { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class ApplicationUserInit : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitSetup(context);
            base.Seed(context);
        }

        public void InitSetup(ApplicationDbContext context)
        {
            // 权限初始化
            var RoleStore = new RoleStore<IdentityRole>(context);
            var RoleManager = new RoleManager<IdentityRole>(RoleStore);

            List<IdentityRole> identityRoles = new List<IdentityRole>();
            identityRoles.Add(new IdentityRole() { Name = RoleType.SYSTEM });
            identityRoles.Add(new IdentityRole() { Name = RoleType.ADMIN });
            identityRoles.Add(new IdentityRole() { Name = RoleType.SENTRY });
            identityRoles.Add(new IdentityRole() { Name=RoleType.FINANCE});
            identityRoles.Add(new IdentityRole() { Name = RoleType.CUSTOMER });

            foreach (IdentityRole role in identityRoles)
            {
                RoleManager.Create(role);
            }

            // 用户初始化
            var UserStore = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(UserStore);

            ApplicationUser admin = new ApplicationUser();
            admin.Email = "haoyikeji@H51.com.cn";
            admin.UserName = "System";
            admin.PhoneNumber = "13000000000";

            UserManager.Create(admin, "Easy.7788");
            UserManager.AddToRole(admin.Id, RoleType.ADMIN);
            
            InitBaseUser(admin);
        }
        public void InitBaseUser(ApplicationUser applicationUser)
        {
            BaseUserModel model = new BaseUserModel();
            BaseUserModelDbContext context=new BaseUserModelDbContext();
            model.IdentityUserId = applicationUser.Id;
            model.Email = applicationUser.Email;
            model.PhoneNumber = applicationUser.PhoneNumber;
            model.CreateTime = applicationUser.CreateTime;
            model.UpdateTime = applicationUser.UpdateTime;
            model.RealName = applicationUser.UserName;
            model.IdentityCardNumber = "";
            model.Address = "";
            model.Note = "";
            context.BaseUser.Add(model);
            context.SaveChanges();
        }
    }
}