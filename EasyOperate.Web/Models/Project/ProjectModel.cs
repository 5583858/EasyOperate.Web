using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EasyOperate.Web.Models
{
    /// <summary>
    /// 车场项目表
    /// </summary>
    [Table("Projects")]
    public class ProjectModel : BaseModel
    {        
        /// <summary>
        /// 项目所在区域名称
        /// </summary>        
        [Display(Name = "项目所在大区名称")]
        public string RegionName { get; set; }
        /// <summary>
        /// 项目所在城市名称
        /// </summary>        
        [Display(Name = "项目所在城市名称")]
        public string CityName { get; set; }
        /// <summary>
        /// 经度
        /// </summary>        
        [Display(Name = "经度")]
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>        
        [Display(Name = "纬度")]
        public double Latitude { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [Display(Name = "项目名称")]
        public string Name { get; set; }
        /// <summary>
        /// 项目详细地址
        /// </summary>        
        [Display(Name = "项目详细地址")]
        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Note { get; set; }
    }

    public class ProjectDbContext : EasyOperateBaseDbContext
    {
        public DbSet<ProjectModel> Project { get; set; }
    }
}