using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Transport.Channels;
using Newtonsoft.Json;
using System.Text;

namespace EasyOperate.Web.Manager
{
    public class ResponseDeviceManager
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void ResponseDevice<T>(T obj, IChannelHandlerContext ctx)
        {
            IFullHttpResponse response = null;

            if (obj != null)
            {
                string json = JsonConvert.SerializeObject(obj);
                response = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.OK, Unpooled.WrappedBuffer(Encoding.UTF8.GetBytes(json)));
                logger.Debug($"ResponseContent:{json}");
            }
            else
            {
                response = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.OK);
                logger.Debug($"ResponseContent:NULL");
            }

            response.Headers
                .Set(HttpHeaderNames.ContentLength, response.Content.ReadableBytes)
                .Set(HttpHeaderNames.ContentType, HttpHeaderValues.TextPlain)
                .Set(HttpHeaderNames.AccessControlAllowHeaders, HttpHeaderValues.Close)
                .Set(HttpHeaderNames.Connection, HttpHeaderValues.Close);

            ctx.WriteAndFlushAsync(response);
        }
    }
}