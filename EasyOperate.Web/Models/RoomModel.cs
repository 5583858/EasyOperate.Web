using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EasyOperate.Web.Models
{
    [Table("Rooms")]
    public class RoomModel : BaseModel
    {
        public int FloorId { get; set; }
        /// <summary>
        /// 房间序号
        /// </summary>
        public int RoomNumber { get; set; }
        /// <summary>
        /// 房间名
        /// </summary>
        public string Name { get; set; }
    }
    public class RoomModelDbContext : EasyOperateBaseDbContext
    {
        public DbSet<RoomModel> Room { get; set; }
    }
}