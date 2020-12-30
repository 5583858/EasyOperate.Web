using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models.AccessControlModel
{
    /// <summary>
    /// 新增人员请求模型
    /// POST /LAPI/V1.0/PeopleLibraries/<ID>/People
    /// ID:人员库ID,ULong 
    /// </summary>
    public class PersonRequestModel
    {
        /// <summary>
        /// 人员库人员个数 批量单次最多6个
        /// </summary>
        public ulong Num { get; set; }
        /// <summary>
        /// 人员信息列表
        /// </summary>
        public List<PersonInfo> PersonInfoList { get; set; }

        public void AddPersonInfo(PersonInfo personInfo)
        {
            if (PersonInfoList == null)
            {
                PersonInfoList = new List<PersonInfo>();
            }

            PersonInfoList.Add(personInfo);
            Num++;
        }
    }
    public class TimeTemplateList
    {
        /// <summary>
        /// 时间模板生效起始时间(unix时间戳)  若未配置，填写0
        /// </summary>
        public ulong BeginTime { get; set; }
        /// <summary>
        /// 时间模板生效结束时间(unix时间戳)若未配置，填写4294967295(0xFFFFFFFF)
        /// </summary>
        public ulong EndTime { get; set; }
        /// <summary>
        /// 时间模板ID索引 若未配置，填写0；
        /// </summary>
        public ulong Index { get; set; }
    }

    public class Identification
    {
        /// <summary>
        /// 证件类型  0:身份证  1:IC卡  99:其他
        /// </summary>
        public ulong Type { get; set; }
        /// <summary>
        /// 证件号，范围:[1, 20]，大小写英文字母、数字；
        /// </summary>
        public string Number { get; set; }
    }

    public class FaceImage
    {
        /// <summary>
        /// 人脸照片ID，不可重复，具有唯一性  ImageNum为0 时，此字段可选。
        /// </summary>
        public ulong FaceID { get; set; }
        /// <summary>
        /// 文件名称，范围[1, 16]。下发的照片名称最终会转化为PersonID_FaceID.jpg的格式
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Data字段：图片转为base64后的字符串长度，单位:字节  范围:[0, 1M(1048576)]
        /// </summary>
        public ulong Size { get; set; }
        /// <summary>
        /// 照片Base64编码数据，注意数据头不需要增加base64，以/9j/开头
        /// </summary>
        public string Data { get; set; }
    }

    public class PersonInfo
    {
        /// <summary>
        /// 人员ID，不可重复，具有唯一性; 长度范围:[1, 4294967295]
        /// </summary>
        public ulong PersonID { get; set; }
        /// <summary>
        /// 人员信息最后修改时间(Unix时间戳)
        /// </summary>
        public ulong LastChange { get; set; }
        /// <summary>
        /// 人员编码，可填写学号或工号，长度范围:[1, 15]
        /// </summary>
        public string PersonCode { get; set; }
        /// <summary>
        /// 人员名字， 长度范围:[1, 63]
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 备注信息  长度范围:[1-63]
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 时间模板个数， 该字段必填，若未新增时间模板，则默认填0
        /// </summary>
        public ulong TimeTemplateNum { get; set; }
        /// <summary>
        /// 时间模板信息列表  若TimeTemplateNum为0，则该数组可不填；
        /// </summary>
        public List<TimeTemplateList> TimeTemplateList { get; set; }
        /// <summary>
        /// 证件信息个数  范围:[0, 2]
        /// </summary>
        public ulong IdentificationNum { get; set; }
        /// <summary>
        /// 成员证件信息
        /// </summary>
        public List<Identification> IdentificationList { get; set; }
        /// <summary>
        /// 人脸图片个数 范围:[0, 6]
        /// </summary>
        public ulong ImageNum { get; set; }
        /// <summary>
        /// 人脸图片信息列表
        /// </summary>
        public List<FaceImage> ImageList { get; set; }
    }

    public class FaceList
    {
        /// <summary>
        /// 人脸ID
        /// </summary>
        public ulong FaceID { get; set; }
        /// <summary>
        /// 处理结果状态码
        ///0：新增成功；
        ///1000：算法初始化失败;
        ///1001：人脸检测失败;
        ///1002：图片未检测到人脸;
        ///1003：jpeg照片解码失败;
        ///1004：图片质量分数不满足;
        ///1005：图片缩放失败;
        ///（即图片分辨率不满足要求，请将图片缩放后再下发）
        ///1006：未启用智能;
        ///1007：导入图片过小；
        ///1008：导入图片过大；
        ///1009：导入图片分辨率超过1920*1080
        ///1010：导入图片不存在；
        ///1011：人脸元素个数已达到上限；
        ///1012：智能棒算法模型不匹配；
        ///1013：人脸导入库成员证件号非法；
        ///1014：人脸导入库成员图片格式错误；
        ///1015：通道布控已达设备能力上限；
        ///1016：其它客户端正在进行操作人脸库
        ///1017：人脸库文件正在更新中
        ///1018：Json反序列化失败
        ///1019：Base64解码失败
        ///1020：人脸照片，编码后的大小和实际接收到的长度不一致，关注Data和Size大小是否正确。
        ///FaceNum为0时，此字段可选。
        ///判断是否为0
        /// </summary>
        public ulong ResultCode { get; set; }
    }

    public class PersonList
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public ulong PersonID { get; set; }
        /// <summary>
        /// 人脸个数
        /// </summary>
        public ulong FaceNum { get; set; }
        /// <summary>
        /// 人脸信息结果列表
        /// </summary>
        public List<FaceList> FaceList { get; set; }
    }

    public class PersonResponseData
    {
        /// <summary>
        /// 人员个数
        /// </summary>
        public ulong Num { get; set; }
        /// <summary>
        /// 人员信息结果列表
        /// </summary>
        public List<PersonList> PersonList { get; set; }
    }

    public class PersonResponseModel
    {
        /// <summary>
        /// 收到的请求的URL
        /// </summary>
        public string ResponseURL { get; set; }
        /// <summary>
        /// 传输新对象的ID，该ID由服务器为新对象创建
        /// </summary>
        public ulong CreatedID { get; set; }
        /// <summary>
        /// 处理系统结果 是否成功
        /// </summary>
        public ulong ResponseCode { get; set; }
        /// <summary>
        /// 处理系统结果的提示信息
        /// </summary>
        public string ResponseString { get; set; }
        /// <summary>
        /// 处理业务结果
        /// </summary>
        public ulong StatusCode { get; set; }
        /// <summary>
        /// 处理业务结果的信息提示
        /// </summary>
        public string StatusString { get; set; }
        /// <summary>
        /// 请求响应数据
        /// </summary>
        public PersonResponseData Data { get; set; }
    }
    /// <summary>
    /// 新增人员响应模型
    /// </summary>
    //public class PersonResponse
    //{
    //    /// <summary>
    //    /// 请求响应结果
    //    /// </summary>
    //    public PersonResponseModel Response { get; set; }
    //}

    /// <summary>
    /// 查询人员请求模型
    /// POST /LAPI/V1.0/PeopleLibraries/<ID>/People
    /// ID:人员库ID,ULong 
    /// </summary>
    public class PeopleInfoQueryRequestModel
    {
        /// <summary>
        /// 查询条件数量
        /// </summary>
        public ulong Num { get; set; }
        /// <summary>
        /// 查询条件列表，Num为0时，不带此字段
        /// </summary>
        public List<QueryInfos> QueryInfos { get; set; }
        /// <summary>
        /// 每次查询的数量，最大6条
        /// </summary>
        public ulong Limit { get; set; }
        /// <summary>
        /// 从当前序号开始查询，序号从0开始
        /// </summary>
        public ulong Offset { get; set; }
    }
    public class QueryInfos
    {
        /// <summary>
        /// 查询条件
        ///27：员工编号
        ///55：人员姓名
        ///58：证件号
        ///无查询条件则获取所有人员信息
        /// </summary>
        public ulong QryType { get; set; }
        /// <summary>
        /// 查询条件逻辑类型    0: 等于
        /// </summary>
        public ulong QryCondition { get; set; }
        /// <summary>
        /// 查询条件右值
        /// </summary>
        public string QryData { get; set; }
    }
    /// <summary>
    /// 请求查询人员信息
    /// </summary>
    public class PeopleInfoQuery
    {
        /// <summary>
        /// 人员信息查询请求模型
        /// </summary>
        public PeopleInfoQueryResponseModel Response { get; set; }
    }
    public class PersonQueryList
    {
        /// <summary>
        /// 
        /// </summary>
        public ulong Num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PersonInfo> PersonInfoList { get; set; }
    }

    public class PeopleInfoQueryData
    {
        /// <summary>
        /// 符合查询条件的总数
        /// </summary>
        public ulong Total { get; set; }
        /// <summary>
        /// 当前序号，序号从0开始
        /// </summary>
        public ulong Offset { get; set; }
        /// <summary>
        /// 查询人员列表 
        /// </summary>
        public PersonQueryList PersonList { get; set; }
    }

    public class PeopleInfoQueryResponseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string ResponseURL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ulong CreatedID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ulong ResponseCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ulong SubResponseCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ResponseString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ulong StatusCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StatusString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PeopleInfoQueryData Data { get; set; }
    }
}