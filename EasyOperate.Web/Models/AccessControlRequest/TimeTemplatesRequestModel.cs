using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models.AccessControlModel
{
    /// <summary>
    /// 时间模板请求模型 Add
    /// POST  /LAPI/V1.0/PeopleLibraries/TimeTemplates/<ID>
    /// 时间模板请求模型 修改
    /// PUT LAPI/V1.0/PeopleLibraries/TimeTemplates/<ID>
    public class TimeSectionInfos
    {
        /// <summary>
        /// 
        /// </summary>
        public string Begin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string End { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ArmingType { get; set; }
    }
    public class TimeTemplatesRequestModel
    {
        ///// <summary>
        ///// 人员库时间模板布控任务序号，人员库ID使用GET请求发送，Get必带
        ///// </summary>
        //public ulong ID { get; set; }
        /// <summary>
        /// 时间模板名称； 范围[1, 63]
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 时间模板最后修改时间
        /// </summary>
        public int LastChange { get; set; }
        /// <summary>
        /// 布控任务布防计划，详见 <WeekPlanInfo>
        /// </summary>
        public WeekPlanInfo WeekPlan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ExceptionInfo Exception { get; set; }
    }

    /// <summary>
    /// 4.4.1  时间模板请求响应模型
    /// GET /LAPI/V1.0/PeopleLibraries/TimeTemplates/<ID>
    /// ID:时间模板ID,ULong 
    /// </summary>

    public class TimeSectionInfosItem
    {
        /// <summary>
        /// 开始时间, 格式hh:mm:ss  长度范围[0, 11]
        /// </summary>
        public string Begin { get; set; }
        /// <summary>
        /// 结束时间，格式hh:mm:ss 长度范围[0, 11]
        /// </summary>
        public string End { get; set; }
        /// <summary>
        /// 布防类型
        ///0: 定时
        ///1: 动检
        ///2: 报警
        ///3: 动检和报警
        ///4: 动检或报警
        ///5: 无计划
        ///10: 事件
        ///各类告警布防只支持定时类型
        ///当前不支持此字段。
        /// </summary>
        public int ArmingType { get; set; }
    }
    public class DayPlanInfo
    {
        /// <summary>
        /// 星期索引：从0开始
        ///0：周一；1：周二；
        ///2：周三；3：周四；
        ///4：周五；5：周六；
        ///6：周日；
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 每天时间段个数  默认为8个时间段
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 布防配置信息, 当Num为0时可选  同一天各段时间不允许有重合  布防配置具体信息，当Num为0时可选，详见<TimeSectionInfo>
        /// </summary>
        public List<TimeSectionInfosItem> TimeSectionInfos { get; set; }
    }
    public class WeekPlanInfo
    {
        /// <summary>
        /// 布放计划是否使能0:不使能   1：使能
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 计划天数   最大为7(一周七天)
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 一周内每天的布防计划 列表
        /// </summary>
        public List<DayPlanInfo> Days { get; set; }
    }
    public class ExceptionInfo
    {
        /// <summary>
        /// 例外日期是否使能0:不使能  1：使能
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 例外日期个数
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 一周内每天的布防计划列表
        /// </summary>
        public List<ExceptionDayInfo> ExceptionDays { get; set; }
    }
    public class ExceptionDayInfo
    {
        /// <summary>
        /// 例外日期索引:从0开始
        /// </summary>
        public ulong ID { get; set; }
        /// <summary>
        /// 例外日期是否使能0:不使能  1：使能
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 例外日期   格式："year-month-day"
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 例外时间段个数 0表示一天内不设置布放信息 最大为8个时间段
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 布防配置信息, 当Num不为0时可选  同一天各段时间不允许有重合
        /// </summary>
        public List<TimeSectionInfosItem> TimeSectionInfos { get; set; }
    }
    public class TimeTemplatesResponseData
    {
        /// <summary>
        /// 人员库时间模板布控任务序号，Get必带
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 时间模板名称； 范围[1, 63]
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 时间模板最后修改时间
        /// </summary>
        public int LastChange { get; set; }
        /// <summary>
        /// 控任务布防计划，详见 <WeekPlanInfo>
        /// </summary>
        public WeekPlanInfo WeekPlan { get; set; }
        /// <summary>
        /// 布控任务例外计划，详见 <ExceptionInfo>
        /// </summary>
        public ExceptionInfo Exception { get; set; }
    }
    public class TimeTemplatesResponseModel
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
        /// 请求响应数据
        /// </summary>
        public TimeTemplatesResponseData Data { get; set; }
    }
    public class TimeTemplatesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public TimeTemplatesResponseModel Response { get; set; }
    }

}