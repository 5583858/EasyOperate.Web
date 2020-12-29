using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EasyOperate.Web.Models
{
    [Table("Floors")]
    public class FloorModel : BaseModel
    {
        /// <summary>
        /// 所属单元Id
        /// </summary>
        public int HousePartId { get; set; }
        /// <summary>
        /// 楼层名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 实际楼层号
        /// </summary>
        public int FloorNumber { get; set; }
    }
    public class FloorModelModelDbContext : EasyOperateBaseDbContext
    {
        public DbSet<FloorModel> Floor { get; set; }
    }
}