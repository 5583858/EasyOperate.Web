using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models.AccessControlRequest
{
    public class BasicRequestUrl
    {
        /// <summary>
        /// 人脸设备->服务器 心跳请求URL
        /// </summary>
        public const string HeartReportInfoUrl = "/LAPI/V1.0/PACS/Controller/HeartReportInfo";
        /// <summary>
        /// 服务器->人脸设备 设备信息请求URL
        /// </summary>
        public const string DeviceBasicInfoUrl = "/LAPI/V1.0/System/DeviceBasicInfo";
        /// <summary>
        /// 服务器->人脸设备 设备在线信息请求URL get
        /// </summary>
        public const string KeepAliveInfoUrl = "/LAPI/V1.0/System/KeepAlive";
        /// <summary>
        /// 服务器->人脸设备 人员库查询请求URL
        /// </summary>
        public const string PeopleLibrariesoUrl = "/LAPI/V1.0/PeopleLibraries/BasicInfo";
        /// <summary>
        /// 服务器->人脸设备 人员库新增人员请求URL
        /// </summary>
        public string PeopleInfoProcessingUrl { get; set; }
        /// <summary>
        /// 服务器->人脸设备 获取人员库新增POST、修改PUT人员请求URL
        /// </summary>
        public string GetPeopleInfoProcessingUrl(ulong LibId)
        {
            return PeopleInfoProcessingUrl = "/LAPI/V1.0/PeopleLibraries/" + LibId + "/People";
        }
        /// <summary>
        /// 服务器->人脸设备 人员库删除人员请求URL
        /// </summary>
        public string PeopleInfoDeleteUrl { get; set; }
        /// <summary>
        /// 服务器->人脸设备 删除指定人员库人员请求URL
        /// 参数：人员库ID，人员ID,时间戳
        /// </summary>
        public string GetPeopleInfoDeleteUrlUrl(ulong LibId, ulong PersonId,ulong LastChange)
        {
            return PeopleInfoDeleteUrl = "/LAPI/V1.0/PeopleLibraries/" + LibId + "/People/"+ PersonId + "? LastChange="+LastChange;
        }
        /// <summary>
        /// 服务器->人脸设备 人员库查询人员请求URL
        /// </summary>
        public string PeopleInfoQueryUrl { get; set; }
        /// <summary>
        /// 服务器->人脸设备 查询指定人员库人员请求URL
        /// 参数：人员库ID
        public string GetPeopleInfoQueryUrl(ulong LibId)
        {
            return PeopleInfoQueryUrl = "/LAPI/V1.0/PeopleLibraries/" + LibId + "/People/Info";
        }
        /// <summary>
        /// 时间模板信息单个查询ULR
        /// </summary>
        public string TimeTemplateUrl { get; set; }
        /// <summary>
        /// 服务器->人脸设备 查询指定时间模板请求URL
        /// 参数：时间模板ID
        /// 每次仅处理一个时间模板
        /// </summary>
        public string GetTimeTemplateUrl(ulong TimeTemplateId)
        {
            return TimeTemplateUrl = "/LAPI/V1.0/PeopleLibraries/TimeTemplates/"+ TimeTemplateId;
        }
        /// <summary>
        /// 服务器->人脸设备 查询所有时间模板请求URL GET
        /// </summary>
        public const string AllTimeTemplateUrl = "/LAPI/V1.0/PeopleLibraries/TimeTemplates/UpdateTime";
        /// <summary>
        /// 添加时间模板信息查询ULR
        /// </summary>
        public string TimeTemplateAddUrl { get; set; }
        /// <summary>
        /// 服务器->人脸设备 增加时间模板请求URL POST
        /// 参数：时间模板ID
        /// </summary>
        public string GetTimeTemplateAddUrl(ulong TimeTemplateId)
        {
            return TimeTemplateAddUrl = "/LAPI/V1.0/PeopleLibraries/TimeTemplates/" + TimeTemplateId;
        }
        /// <summary>
        /// 服务器->人脸设备 修改时间模板请求URL 
        /// </summary>
        public string TimeTemplatePutUrl { get; set; }
        /// <summary>
        /// 服务器->人脸设备 修改时间模板请求URL  PUT
        /// 参数：时间模板ID
        /// </summary>
        public string GetTimeTemplatePutUrl(ulong TimeTemplateId)
        {
            return TimeTemplatePutUrl = "/LAPI/V1.0/PeopleLibraries/TimeTemplates/" + TimeTemplateId;
        }
        /// <summary>
        /// 服务器->人脸设备 修改时间模板请求URL 
        /// </summary>
        public string TimeTemplateDeleteUrl { get; set; }
        /// <summary>
        /// 服务器->人脸设备 删除时间模板请求URL  DELETE
        /// 参数：时间模板ID
        /// </summary>
        public string GetTimeTemplateDeleteUrl(ulong TimeTemplateId)
        {
            return TimeTemplateDeleteUrl = "/LAPI/V1.0/PeopleLibraries/TimeTemplates/" + TimeTemplateId;
        }
        /// <summary>
        /// 人脸设备->服务器 向服务器上传记录URL  POST
        /// </summary>
        public const string PushAccessControlRecordUrl = "/LAPI/V1.0/System/Event/Notification/PersonVerification";
        /// <summary>
        /// 服务器->人脸设备 创建订阅URL  POST
        /// </summary>
        public const string CreateSubscriptionUrl = "/LAPI/V1.0/System/Event/Subscription";
        /// <summary>
        /// 服务器->人脸设备 刷新订阅URL
        /// </summary>
        public string RefreshSubscriptionUrl { get; set; }
        /// <summary>
        /// 服务器->人脸设备 刷新订阅URL  PUT
        /// 参数：订阅ID
        /// </summary>
        public string GetRefreshSubscriptionUrl(ulong SubscriptionId)
        {
            return RefreshSubscriptionUrl = "/LAPI/V1.0/System/Event/Subscription/" + SubscriptionId;
        }
        /// <summary>
        /// 服务器->人脸设备 删除订阅URL
        /// </summary>
        public string DeleteRefreshSubscriptionUrl { get; set; }
        /// <summary>
        /// 服务器->人脸设备 删除订阅URL  DELETE
        /// 参数：订阅ID
        /// </summary>
        public string GetDeleteRefreshSubscriptionUrl(ulong SubscriptionId)
        {
            return DeleteRefreshSubscriptionUrl = "/LAPI/V1.0/System/Event/Subscription/" + SubscriptionId;
        }
        /// <summary>
        /// 服务器->人脸设备 远程开门URL  PUT
        /// </summary>
        public const string RemoteOpenedUrl = "/LAPI/V1.0/PACS/Controller/RemoteOpened";
        /// <summary>
        /// 服务器->人脸设备 远程显示URL  PUT
        /// </summary>
        public const string GUIShowUrl = "/LAPI/V1.0/PACS/Controller/GUIShowInfo";
    }
}