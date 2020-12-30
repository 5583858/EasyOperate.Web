using DotNetty.Codecs.Http;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.DotNetty.Codecs
{
    public class UnvHttpResponseEncoder: HttpResponseEncoder
    {
        protected override void Encode(IChannelHandlerContext context, object message, List<object> output)
        {
            base.Encode(context, message, output);
        }

        public void UnvEncode(IChannelHandlerContext context, object message, List<object> output)
        {
            Encode(context, message, output);
        }
    }
}