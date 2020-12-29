using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models.AccessControlModel
{
    /// <summary>
    /// 基本各应模型，多处调用
    /// 设备在线状态查询
    /// GET /LAPI/V1.0/System/KeepAlive
    /// 
    /// 4.3.4  人员信息的删除
    /// DELETE  /LAPI/V1.0/PeopleLibraries/<ID>/People/<ID>?LastChange=<LastChange>
    /// </summary>
    public class ShowInfoList
    {
        /// <summary>
        ///属性值：范围[0,32] 
        ///“张三”,“123456”，为空不显示； 
        ///注：若需控制健康码显示，属性值填写“CodeStatus”，同时设
        ///备开启远程核验模式
        ///“姓名：”
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 属性值：范围[0,32] 
        ///“张三”,“123456”，为空不显示； 
        ///注：若需控制健康码显示，属性值填充数字0-3，同时设备开启 远程核验模式 
        ///“0”：健康码核验失败 
        ///“1”：绿码 
        ///“2”：黄码 
        ///“3”：红码
        /// </summary>
        public string Value { get; set; }
    }

    public class GUIShowModel
    {
        /// <summary>
        /// 开门指令（可控制开门） 0:不开门， 1:开门，
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 结果描述，范围[0,32] "识别成功"
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// 经过时刻：范围[0, 18]   终端上报过人记录中的时间
        /// </summary>
        public string PassTime { get; set; }
        /// <summary>
        /// 人机显示信息行数最大为2，当前最多显示两行
        /// </summary>
        public int ShowInfoNum { get; set; }
        /// <summary>
        /// 显示信息
        /// </summary>
        public List<ShowInfoList> ShowInfoList { get; set; }
    }
}