using DotNetty.Transport.Channels;

namespace EasyOperate.Web.Manager
{
    public class DeviceChannelContext
    {
        public IChannelHandlerContext Context { get; set; }

        public bool IsBound { get; set; }

        public string DeviceCode { get; set; }

        public bool IsLock { get; set; }

        public DeviceChannelContext(IChannelHandlerContext ctx, bool isBound, string deviceCode)
        {
            Context = ctx;
            IsBound = isBound;
            DeviceCode = deviceCode;
        }
    }
}