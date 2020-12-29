using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models
{
    /// <summary>
    /// 返回给前端Vue的信息
    /// </summary>
    public class ResponseInfo
    {
         public int Code { get; set; }
         public string Message { get; set; }
         public object Entity { get; set; }
         public ResponseInfo(int code, string message, object obj)
         {
             this.Code = code;
             this.Message = message;
             this.Entity = obj;
         }
     }
}