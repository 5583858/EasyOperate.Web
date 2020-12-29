using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EasyOperate.Web.Models
{
    [Table("Houses")]
    public class HouseModel : BaseModel
    {
        /// <summary>
        /// 所属分期Id
        /// </summary>
        public int SubRegionId { get; set; }
        /// <summary>
        /// 建筑名（楼）
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 楼号
        /// </summary>
        public int HouseNumber { get; set; }
        /// <summary>
        /// 行政编号
        /// </summary>
        public string AdministrativeNumber { get; set; }

        ///// <summary>
        ///// 单元数量
        ///// </summary>
        //public int HousePartCount { get; set; }
        ///// <summary>
        ///// 单元前缀
        ///// </summary>
        //public string HousePartPrefix { get; set; }
        ///// <summary>
        ///// 单元后缀
        ///// </summary>
        //public string HousePartSuffix { get; set; }
        
    }
    public class HouseModelDbContext : EasyOperateBaseDbContext
    {
        public DbSet<HouseModel> House { get; set; }
    }
}