using DotNetty.Codecs;
using DotNetty.Codecs.Http;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.DotNetty.Codecs
{
    public class HttpEncoder<T> : MessageToMessageEncoder<T>
    {
        private UnvHttpRequestEncoder httpRequestEncoder = new UnvHttpRequestEncoder();

        private UnvHttpResponseEncoder httpResponseEncoder = new UnvHttpResponseEncoder();

        protected override void Encode(IChannelHandlerContext context, T message, List<object> output)
        {
            if (message is IFullHttpRequest)
            {
                httpRequestEncoder.UnvEncode(context, message, output);
            }
            else if (message is IFullHttpResponse)
            {
                httpResponseEncoder.UnvEncode(context, message, output);
            }
        }
    }
}