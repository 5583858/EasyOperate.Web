using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using EasyOperate.Web.Models.AccessControl;


namespace EasyOperate.Web.Models
{
    public class EfDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public EfDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<EfDbContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static EfDbContext Create()
        {
            return new EfDbContext();
        }

        /// <summary>
        /// 项目（来自数据库）
        /// </summary>
        public DbSet<ProjectModel> Project { get; set; }
        /// <summary>
        /// 分区
        /// </summary>
        public DbSet<SubRegionModel> SubRegion { get; set; }
        /// <summary>
        /// 楼
        /// </summary>
        public DbSet<HouseModel> House { get; set; }
        /// <summary>
        /// 单元
        /// </summary>
        public DbSet<HousePartModel> HousePart { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public DbSet<FloorModel> Floor { get; set; }
        /// <summary>
        /// 房间
        /// </summary>
        public DbSet<RoomModel> Room { get; set; }
        /// <summary>
        /// 基本用户模型
        /// </summary>
        public DbSet<BaseUserModel> BaseUser { get; set; }
        /// <summary>
        /// 用户照片
        /// </summary>
        public DbSet<UserPhotoModel> UserPhoto { get; set; }
        /// <summary>
        /// 授权模板
        /// </summary>
        public DbSet<AccessControlAuthorizationTemplateModel> AccessControlAuthorizationTemplate { get; set; }
        /// <summary>
        /// 授权模板中的节点
        /// </summary>
        public DbSet <AccessControlAuthorizationsTemplateNodeModel> AccessControlAuthorizationsTemplateNode { get; set; }
        /// <summary>
        /// 授权记录表
        /// </summary>
        public DbSet <AccessControlAuthorizedRecordModel> AccessControlAuthorizedRecord { get; set; }
        /// <summary>
        /// 卡授权模型
        /// </summary>
        public DbSet <AccessControlCardAuthorizedModel> AccessControlCardAuthorized { get; set; }
        /// <summary>
        /// 门禁卡
        /// </summary>
        public DbSet<AccessControlCardModel> AccessControlCard { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public DbSet<AccessControlEquipmentModel> AccessControlEquipment { get; set; }
        /// <summary>
        /// 设备节点（门禁）
        /// </summary>
        public DbSet<AccessControlEquipmentNodeModel> AccessControlEquipmentNode { get; set; }
        /// <summary>
        /// 门禁记录
        /// </summary>
        public DbSet<AccessControlRecordModel> AccessControlRecord { get; set; }
    }
}