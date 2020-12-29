using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace EasyOperate.Web.Models
{
    [Table("UserPhotos")]
    public class UserPhotoModel : BaseModel
    {
        public int BaseUserId { get; set; }
        public string ImgType { get; set; }
        public string Path { get; set; }
    }
    public class UserPhotoModelDbContext : EasyOperateBaseDbContext
    {
        public DbSet<UserPhotoModel> UserPhoto { get; set; }
    }
}