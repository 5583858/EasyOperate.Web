using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using System;
using System.Text;

namespace EasyOperate.Web.Manager
{
    public class RequestDeviceManager
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IFullHttpRequest CreateRequestDevice(string api, string json, HttpMethod httpMethod)
        {
            IFullHttpRequest request = null;

            try
            {
                if (String.IsNullOrEmpty(json))
                {
                    request = new DefaultFullHttpRequest(HttpVersion.Http11, httpMethod, api);
                }
                else
                {
                    request = new DefaultFullHttpRequest(HttpVersion.Http11, httpMethod, api, Unpooled.WrappedBuffer(Encoding.UTF8.GetBytes(json)));
                    request.Headers.Set(HttpHeaderNames.ContentLength, request.Content.ReadableBytes);
                }

                request.Headers
                    .Set(HttpHeaderNames.ContentType, HttpHeaderValues.ApplicationJson)
                    .Set(HttpHeaderNames.Connection, HttpHeaderValues.KeepAlive);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
            }

            return request;
        }
    }
}