using DotNetty.Codecs.Http;
using EasyOperate.Web.Models.AccessControlModel;
using EasyOperate.Web.Models.AccessControlRequest;

namespace EasyOperate.Web.Manager
{
    public class DeviceBasicInfoManager
    {
        public DeviceBasicInfoResponseData GetInfo()
        {
            IFullHttpRequest request = RequestDeviceManager.CreateRequestDevice(BasicRequestUrl.DeviceBasicInfoUrl, string.Empty, HttpMethod.Get);
            BasicResponse<DeviceBasicInfoResponseData> result = HttpKeepAliveManager.SendRequest<DeviceBasicInfoResponseData>("210235C3R0320B001510", request);
            return result.Response.Data;
        }
    }
}