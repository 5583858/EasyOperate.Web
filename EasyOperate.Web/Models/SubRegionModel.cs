using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EasyOperate.Web.Models
{
    [Table("SubRegions")]
    public class SubRegionModel : BaseModel
    {
        /// <summary>
        /// 所属项目ID
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// 分区名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分区编号
        /// </summary>
        public int SubRegionNumber { get; set; }
    }
    public class SubRegionModelDbContext : EasyOperateBaseDbContext
    {
        public DbSet<SubRegionModel> SubRegion { get; set; }
    }
}