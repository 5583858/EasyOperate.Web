using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models.AccessControlModel
{
    /// <summary>
    /// POST  /LAPI/V1.0/PACS/Controller/HeartReportInfo HTTP/1.1
    /// </summary>
    public class HeartReportInfoModel
    {
        /// <summary>
        /// 请求流水号（UUID）范围：[0 36]
        /// </summary>
        public string RefId { get; set; }
        /// <summary>
        /// 本次心跳上报时间 范围:[0 20]
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 下次心跳上报时间 范围:[0 20]
        /// </summary>
        public DateTime NextTime { get; set; }
        /// <summary>
        /// 设备编码(设备序列号) 范围:[0 24]
        /// </summary>
        public string DeviceCode { get; set; }
        /// <summary>
        /// 设备类型
        ///1：普通门禁；
        ///2：可视对讲门禁;
        ///3：考勤机
        ///4；录入设备
        ///5：室内机
        /// </summary>
        public ulong DeviceType { get; set; }
    }
    public class TimeData
    {
        /// <summary>
        /// 当前时间 范围:[0 20]
        /// </summary>
        public DateTime Time { get; set; }
    }
    /// <summary>
    /// 设备响应模型
    /// </summary>
    public class HeartReportResponseModel
    {
        /// <summary>
        /// 响应URL：即响应哪条URL的请求。范围：[0 128]
        /// </summary>
        public string ResponseURL { get; set; }
        /// <summary>
        /// 响应码（待扩充）
        ///0：成功；
        ///1：失败；
        /// </summary>
        public ulong Code { get; set; }
        /// <summary>
        /// 响应数据
        /// </summary>
        public TimeData Data { get; set; }
    }
}