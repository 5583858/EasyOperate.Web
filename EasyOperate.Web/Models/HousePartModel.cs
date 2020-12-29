using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EasyOperate.Web.Models
{
    [Table("HouseParts")]
    public class HousePartModel : BaseModel
    {
        // <summary>
        /// 所属楼ID
        /// </summary>
        public int HouseId { get; set; }
        /// <summary>
        /// 单元号
        /// </summary>
        public int HousePartNumber { get; set; }
        /// <summary>
        /// 单元名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 楼层数量
        /// </summary>
        public int FloorCount { get; set; }

        /// <summary>
        /// 楼层前缀
        /// </summary>
        public string FloorPrefix { get; set; }
        /// <summary>
        /// 楼层后缀
        /// </summary>
        public string FloorSuffix { get; set; }
        /// <summary>
        /// 单层房间数量
        /// </summary>
        public int SingleFloorRoomCount { get; set; }
        /// <summary>
        /// 房间前缀
        /// </summary>
        public string RoomPrefix { get; set; }
        /// <summary>
        /// 房间后缀
        /// </summary>
        public string RoomSuffix { get; set; }
    }
    public class HousePartModelDbContext : EasyOperateBaseDbContext
    {
        public DbSet<HousePartModel> HousePart { get; set; }
    }
}