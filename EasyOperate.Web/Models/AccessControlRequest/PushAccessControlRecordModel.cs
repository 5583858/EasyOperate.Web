using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Models.AccessControlModel
{
    /// <summary>
    /// 设备向平台上传记录的模型 /LAPI/V1.0/System/Event/Notification/PersonVerification
    /// </summary>
    public class PushAccessControlRecord
    {
        public class FeatureListItem
        {
            /// <summary>
            /// 人脸半结构化特征提取算法版本号，比如"ISFRFR259.2.0"。  范围：[0, 20]
            /// </summary>
            public string FeatureVersion { get; set; }
            /// <summary>
            /// 采用base64编码。基于人脸提取出来的特征信息，用于辅助后端服务器进行人脸比对等。目前加密前512 Bytes
            /// </summary>
            public string Feature { get; set; }
        }
        public class PanoImage
        {
            /// <summary>
            /// 文件名称， 长度范围[1, 16]
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Data数据大小，单位:字节。范围:[0, 1M(1048576)]
            /// </summary>
            public ulong Size { get; set; }
            /// <summary>
            /// 文件Base64编码数据，注意数据头中不需要增加base64
            /// </summary>
            public string Data { get; set; }
        }
        public class FileInfo
        {
            /// <summary>
            /// 文件名称， 长度范围[1, 16]
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Data数据大小，单位:字节。范围:[0, 1M(1048576)]
            /// </summary>
            public ulong Size { get; set; }
            /// <summary>
            /// 文件Base64编码数据，注意数据头中不需要增加base64
            /// </summary>
            public string Data { get; set; }
        }

        public class FaceImage
        {
            /// <summary>
            /// 文件名称， 长度范围[1, 16]
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Data数据大小，单位:字节。范围:[0, 1M(1048576)]
            /// </summary>
            public ulong Size { get; set; }
            /// <summary>
            /// 文件Base64编码数据，注意数据头中不需要增加base64
            /// </summary>
            public string Data { get; set; }
        }

        public class FaceArea
        {
            /// <summary>
            /// 左上角x坐标
            /// </summary>
            public ulong LeftTopX { get; set; }
            /// <summary>
            /// 左上角y坐标
            /// </summary>
            public ulong LeftTopY { get; set; }
            /// <summary>
            /// 右下角x坐标
            /// </summary>
            public ulong RightBottomX { get; set; }
            /// <summary>
            /// 右下角y坐标
            /// </summary>
            public ulong RightBottomY { get; set; }
        }

        public class FaceInfoListItem
        {
            /// <summary>
            /// 记录ID
            /// </summary>
            public ulong ID { get; set; }
            /// <summary>
            /// 采集时间，UTC格式，单位秒 如果没有采集时间，可不上报
            /// </summary>
            public ulong Timestamp { get; set; }
            /// <summary>
            /// 采集来源1：人脸识别终端采集的人脸信息;2：读卡器采集的门禁卡信息;3：读卡器采集的身份证信息;4：闸机采集的闸机信息;FaceInfo选择1
            /// </summary>
            public ulong CapSrc { get; set; }
            /// <summary>
            /// 半结构化特征数目  如果没有半结构化特征，可不带相关字段
            /// </summary>
            public ulong FeatureNum { get; set; }
            /// <summary>
            /// 半结构化特征列表 如果没有半结构化特征，可不带相关字段
            /// </summary>
            public List<FeatureListItem> FeatureList { get; set; }
            /// <summary>
            /// 体温 未知或未启用检测时，填0
            /// </summary>
            public float Temperature { get; set; }
            /// <summary>
            /// 是否戴口罩0：未知或未启用检测1：未戴口罩2：戴口罩
            /// </summary>
            public ulong MaskFlag { get; set; }
            /// <summary>
            /// 人脸全景图，可根据需要选择上报,详见FileInfo 注：PTS文件大小范围：[0 1M]
            /// </summary>
            public PanoImage PanoImage { get; set; }
            /// <summary>
            /// 人脸小图，可根据需要选择上报,详见FileInfo  注：PTS文件大小范围：[0 256K]
            /// </summary>
            public FaceImage FaceImage { get; set; }
            /// <summary>
            ///人脸全景图人脸区域坐标  在人脸大图中的人脸位置信息  画面坐标归一化：0-10000 矩形左上和右下点：“138,315,282,684”
            /// </summary>
            public FaceArea FaceArea { get; set; }
        }
        public class CardInfoListItem
        {
            /// <summary>
            /// 记录ID
            /// </summary>
            public ulong ID { get; set; }
            /// <summary>
            /// 采集时间，UTC格式，单位秒 如果没有采集时间，可不上报
            /// </summary>
            public ulong Timestamp { get; set; }
            /// <summary>
            /// 采集来源1：人脸识别终端采集的人脸信息;2：读卡器采集的门禁卡信息;3：读卡器采集的身份证信息;4：闸机采集的闸机信息;FaceInfo选择1
            /// </summary>
            public ulong CapSrc { get; set; }
            /// <summary>
            /// 0：身份证，1：门禁卡
            /// </summary>
            public ulong CardType { get; set; }
            /// <summary>
            /// 门禁卡字段：物理卡号，最长18位
            /// </summary>
            public string CardID { get; set; }
            /// <summary>
            /// 门禁卡字段：卡状态：1：有效，0：无效
            /// </summary>
            public ulong CardStatus { get; set; }
            /// <summary>
            /// 身份证字段：姓名，范围[1,63]
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            ///身份证字段：0-未知的性别 1-男 2-女 9-未说明的性别 
            /// </summary>
            public ulong Gender { get; set; }
            /// <summary>
            /// 身份证字段：民族，参考GB/T 3304 中国各民族名称的罗马字母拼写法和代码01：汉族…

            /// </summary>
            public ulong Ethnicity { get; set; }
            /// <summary>
            /// 身份证字段：出生日期，格式为YYYYMMDD
            /// </summary>
            public string Birthday { get; set; }
            /// <summary>
            /// 身份证字段：住址
            /// </summary>
            public string ResidentialAddress { get; set; }
            /// <summary>
            /// 身份证字段：身份证号码，，最长18位
            /// </summary>
            public string IdentityNo { get; set; }
            /// <summary>
            /// 身份证字段：发证机关，，一般格式为：XX 省 XX 市 XX 区（县）公安分局
            /// </summary>
            public string IssuingAuthority { get; set; }
            /// <summary>
            /// 身份证字段：发证日期，格式为YYYYMMDD
            /// </summary>
            public string IssuingDate { get; set; }
            /// <summary>
            /// 身份证字段：证件有效期开始时间，格式为YYYYMMDD
            /// </summary>
            public string ValidDateStart { get; set; }
            /// <summary>
            /// 身份证字段：证件有效期结束时间，格式为YYYYMMDD
            /// </summary>
            public string ValidDateEnd { get; set; }
            /// <summary>
            /// 身份证字段：身份证照片 注：PTS文件大小范围：[0,32K]
            /// </summary>
            public FileInfo IDImage { get; set; }
        }
        public class GateInfoListItem
        {
            /// <summary>
            /// 记录ID
            /// </summary>
            public ulong ID { get; set; }
            /// <summary>
            /// 采集时间，UTC格式，单位秒 如果没有采集时间，可不上报
            /// </summary>
            public ulong Timestamp { get; set; }
            /// <summary>
            /// 采集来源1：人脸识别终端采集的人脸信息;2：读卡器采集的门禁卡信息;3：读卡器采集的身份证信息;4：闸机采集的闸机信息;FaceInfo选择1
            /// </summary>
            public ulong CapSrc { get; set; }
            /// <summary>
            /// 进入人员计数
            /// </summary>
            public ulong InPersonCnt { get; set; }
            /// <summary>
            /// 出去人员计数
            /// </summary>
            public ulong OutPersonCnt { get; set; }
        }
        public class MatchPersonInfo
        {
            /// <summary>
            /// 成员名字，范围[1,63]
            /// </summary>
            public string PersonName { get; set; }
            /// <summary>
            /// 成员性别 0：未知  1：男性2：女性  9：未说明
            /// </summary>
            public int Gender { get; set; }
            /// <summary>
            /// 门禁卡号，没有填空
            /// </summary>
            public string CardID { get; set; }
            /// <summary>
            /// 身份证卡号，没有填空
            /// </summary>
            public string IdentityNo { get; set; }
        }

        public class LibMatInfoListItem
        {
            /// <summary>
            /// 记录ID
            /// </summary>
            public ulong ID { get; set; }
            /// <summary>
            /// 库ID
            /// </summary>
            public ulong LibID { get; set; }
            /// <summary>
            /// 库类型，可选字段0: 默认无效值;1：黑名单  2: 灰名单/陌生人3：员工    4: 访客
            /// </summary>
            public ulong LibType { get; set; }
            /// <summary>
            /// 匹配状态
            ///0：无核验状态
            ///1：核验成功，
            ///2：核验失败（比对失败），
            ///3：核验失败（对比成功，不在布控时间）
            ///4：核验失败（证件已过期）
            ///5：强制停止
            ///6：非活体
            ///7：刷脸开门模式下，刷脸成功；
            ///8：刷脸开门模式下，刷脸失败；
            ///9：安全帽识别失败；
            ///10：核验失败（对比成功，人脸属性异常）；
            ///11：测温异常；
            ///21：底图录入成功-新增，
            ///22：底图录入成功-更新；
            ///23：底图录入成功-人脸采集；
            ///24：录入失败；
            ///25：录入失败-证件已过期；
            ///26：录入失败-非活体；
            ///27：无效值；
            ///说明：当MatchStatus为10时，需要读取FaceInfoList中的Temperature和MaskFlag字段获取具体的异常信息

            /// </summary>
            public ulong MatchStatus { get; set; }
            /// <summary>
            /// 匹配人员ID
            /// </summary>
            public ulong MatchPersonID { get; set; }
            /// <summary>
            /// 匹配人脸ID
            /// </summary>
            public ulong MatchFaceID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public MatchPersonInfo MatchPersonInfo { get; set; }
        }

        public class PushAccessControlRecordModel
        {
            /// <summary>
            /// 订阅者描述信息，以URL格式体现
            /// </summary>
            public string Reference { get; set; }
            /// <summary>
            /// 通知记录序号
            /// </summary>
            public ulong Seq { get; set; }
            /// <summary>
            /// 设备编码(设备序列号) 范围:[0 24]
            /// </summary>
            public string DeviceCode { get; set; }
            /// <summary>
            /// 通知上报时间，UTC格式，单位秒
            /// </summary>
            public ulong Timestamp { get; set; }
            /// <summary>
            /// 通知类型 0：实时通知1：历史通知
            /// </summary>
            public ulong NotificationType { get; set; }
            /// <summary>
            /// 人脸信息数目，范围：[0, 1]当采集信息没有人脸时，可不带FaceInfo相关字段
            /// </summary>
            public ulong FaceInfoNum { get; set; }
            /// <summary>
            /// 人脸信息列表，详见<FaceInfoList>;当采集信息没有人脸时，可不带FaceInfo相关字段
            /// </summary>
            public List<FaceInfoListItem> FaceInfoList { get; set; }
            /// <summary>
            /// 卡信息数目 范围：[0, 1]  当采集信息没有卡证时，可不带CardInfo相关字段
            /// </summary>
            public ulong CardInfoNum { get; set; }
            /// <summary>
            /// 卡信息列表, 详见<CardInfoList>;当采集信息没有卡证时，可不带CardInfo相关字段
            /// </summary>
            public List<CardInfoListItem> CardInfoList { get; set; }
            /// <summary>
            /// 闸机信息数目 范围：[0, 1] 采集信息没有闸机信息时时，可不带GateInfo相关字段
            /// </summary>
            public ulong GateInfoNum { get; set; }
            /// <summary>
            /// 闸机信息列表, 详见<GateInfoList>;当采集信息没有闸机信息时时，可不带GateInfo相关字段
            /// </summary>
            public List<GateInfoListItem> GateInfoList { get; set; }
            /// <summary>
            /// 库比对信息数目 范围：[0, 16] 当采集类型为人脸采集时，可不带LibMatInfo相关字段
            /// </summary>
            public int LibMatInfoNum { get; set; }
            /// <summary>
            /// 库比对信息列表，详见<CtrlLibMatInfo>;当采集类型为人脸采集时，可不带LibMatInfo相关字段
            /// </summary>
            public List<LibMatInfoListItem> LibMatInfoList { get; set; }
        }
    }
////////////////******订阅推送**********////////////////////////
    public class SubscribePersonConditionInfo
    {
        /// <summary>
        /// 订阅的库ID数目
        ///当订阅类型为0时，可不带LibIDNum、LibIDList字段当订阅类型为1时需要指定订阅的库ID数目和ID值，
        ///LibIDNum为65535时，表示订阅所有库.我们订阅所有库
        /// </summary>
        public int LibIDNum { get; set; }
        /// <summary>
        /// 订阅的库ID列表
        /// </summary>
        public List<int> LibIDList { get; set; }
    }

    public class CreateSubscriptionModel
    {
        /// <summary>
        /// IP地址类型
        ///0: IPv4
        ///1: IPv6
        ///当前仅支持IPV4
        /// </summary>
        public int AddressType { get; set; }
        /// <summary>
        /// 订阅方设备IP地址， 范围：[0, 64]
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 订阅方设备端口 范围：[1, 65535]
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 订阅时间，单位秒  范围：[30, 3600]  Duration为4294967295表示永久订阅
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 订阅类型 1024:人员核验
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 订阅内容
            /// </summary>
        public SubscribePersonConditionInfo SubscribePersonCondition { get; set; }
    }
    public class SubscribePersonConditionResponseData
    {
        /// <summary>
        /// 订阅ID 固定为1000
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 订阅者描述信息，以URL格式体现
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// 当前时间，UTC格式，单位秒
        /// </summary>
        public int CurrrntTime { get; set; }
        /// <summary>
        /// 结束时间，UTC格式，单位秒
        /// </summary>
        public int TerminationTime { get; set; }
    }
    /// <summary>
    /// 订阅响应模型
    /// </summary>
    public class SubscribePersonConditionResponseModel
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
        public SubscribePersonConditionResponseData Data { get; set; }
    }
    /// <summary>
    /// 设备回复的订阅响应模型
    /// </summary>
    public class SubscribePersonConditionResponse
    {
        /// <summary>
        /// 订阅响应模型
        /// </summary>
        public SubscribePersonConditionResponseModel Response { get; set; }
    }
    ////////////////******刷新订阅**********////////////////////////
    public class RefreshSubscriptionRequest
    {
        /// <summary>
        /// 刷新订阅JOSN参数(模型)
        /// </summary>
        public int Duration { get; set; }
    }
    public class RefreshSubscriptionResponseData
    {
        /// <summary>
        /// 订阅者描述信息，以URL格式体现
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// 当前时间，UTC格式，单位秒
        /// </summary>
        public int CurrrntTime { get; set; }
        /// <summary>
        /// 结束时间，UTC格式，单位秒
        /// </summary>
        public int TerminationTime { get; set; }
    }

    public class RefreshSubscriptionResponseModel
    {/// <summary>
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
        public RefreshSubscriptionResponseData Data { get; set; }
    }

    public class RefreshSubscription
    {
        /// <summary>
        /// 刷新订阅对象
        /// </summary>
        public RefreshSubscriptionResponseModel Response { get; set; }
    }
}