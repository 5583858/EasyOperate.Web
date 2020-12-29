using System.ComponentModel.DataAnnotations;

namespace EasyOperate.Web.Models
{
    public class BaseModel
    {
        [Key]
        [Required]
        public int ID { get; set; }
    }
}