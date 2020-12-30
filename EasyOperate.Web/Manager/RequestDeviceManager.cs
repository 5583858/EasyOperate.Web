using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using System;
using System.Text;

namespace EasyOperate.Web.Manager
{
    public class RequestDeviceManager
    {
        public static IFullHttpRequest CreateRequestDevice(string api, string json, HttpMethod httpMethod)
        {
            IFullHttpRequest request = null;
            //Uri uri = new Uri(api);

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

            return request;
        }
    }
}