using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models.AccessControlModel
{
    /// <summary>
    /// 基本响应模型，多处调用
    /// 设备在线状态查询
    /// GET /LAPI/V1.0/System/KeepAlive
    /// 
    /// 4.3.4  人员信息的删除
    /// DELETE  /LAPI/V1.0/PeopleLibraries/<ID>/People/<ID>?LastChange=<LastChange>
    /// </summary>
    public class BasicResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public BasicResponseModel Response { get; set; }
    }

    public class BasicResponseModel
    {
        /// <summary>
        /// 收到的请求的URL
        /// </summary>
        public string ResponseURL { get; set; }
        /// <summary>
        /// 传输新对象的ID，该ID由服务器为新对象创建
        /// </summary>
        public int CreatedID { get; set; }
        /// <summary>
        /// 处理系统结果 是否成功
        /// </summary>
        public int ResponseCode { get; set; }
        /// <summary>
        /// 处理系统结果的提示信息
        /// </summary>
        public string ResponseString { get; set; }
        /// <summary>
        /// 处理业务结果
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// 处理业务结果的信息提示
        /// </summary>
        public string StatusString { get; set; }
        /// <summary>
        /// 设备信息数据
        /// </summary>
        public string Data { get; set; }
    }
}