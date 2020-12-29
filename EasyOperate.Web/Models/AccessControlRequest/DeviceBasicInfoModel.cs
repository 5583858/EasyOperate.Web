using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models.AccessControlModel
{
    /// <summary>
    /// 设备信息查询
    /// GET /LAPI/V1.0/System/DeviceBasicInfo HTTP/1.1
    /// </summary>
    public class DeviceBasicInfoResponse
    {
        /// <summary>
        /// 查询设备信息响应对象
        /// </summary>
        public DeviceBasicInfoResponseModel Response { get; set; }
    }
    /// <summary>
    /// 响应数据
    /// </summary>
    public class DeviceBasicInfoResponseData
    {
        /// <summary>
        /// 厂商信息
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string DeviceModel { get; set; }
        /// <summary>
        /// 设备配置信息
        /// </summary>
        public string DeviceConfig { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 设备MAC地址
        /// </summary>
        public string MAC { get; set; }
        /// <summary>
        /// 软件版本
        /// </summary>
        public string FirmwareVersion { get; set; }
        /// <summary>
        /// 硬件标识
        /// </summary>
        public string HardewareID { get; set; }
        /// <summary>
        /// PCB版本
        /// </summary>
        public string PCBVersion { get; set; }
        /// <summary>
        /// UBOOT引导版本
        /// </summary>
        public string UbootVersion { get; set; }
        /// <summary>
        /// 机芯版本
        /// </summary>
        public string CameraVersion { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 子网掩码
        /// </summary>
        public string Netmask { get; set; }
        /// <summary>
        /// 网关
        /// </summary>
        public string Gateway { get; set; }
    }

    public class DeviceBasicInfoResponseModel
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
        public DeviceBasicInfoResponse Data { get; set; }
    }
}