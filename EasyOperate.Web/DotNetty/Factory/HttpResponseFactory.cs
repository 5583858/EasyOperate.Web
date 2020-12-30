using DotNetty.Codecs.Http;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace EasyOperate.Web.DotNetty.Factory
{
    public class HttpResponseFactory
    {
        protected static ConcurrentDictionary<int, string> ResponseData = new ConcurrentDictionary<int, string>();

        protected static ConcurrentDictionary<int, ConcurrentDictionary<int, string>> KeepAliveResponseData = new ConcurrentDictionary<int, ConcurrentDictionary<int, string>>();

        public static void SaveResponse(IChannelHandlerContext ctx, IFullHttpResponse response)
        {
            try
            {
                ConcurrentDictionary<int, string> tempResponseData = null;
                string responseBody = response.Content.ToString(Encoding.UTF8);
                int channelHashCode = ctx.Channel.GetHashCode();

                if (KeepAliveResponseData.ContainsKey(channelHashCode))
                {
                    tempResponseData = KeepAliveResponseData[channelHashCode];
                }

                if (tempResponseData != null)
                {
                    tempResponseData.TryUpdate(channelHashCode, responseBody, null);
                }
                else
                {
                    ResponseData.TryAdd(channelHashCode, responseBody);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void RemoveResponse(IChannelHandlerContext ctx)
        {
            try
            {
                if (ctx != null)
                {
                    string outData = string.Empty;
                    ResponseData.TryRemove(ctx.Channel.GetHashCode(), out outData);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string GetResponse(IChannelHandlerContext ctx, long timeout)
        {
            try
            {
                DateTime startDateTime = DateTime.Now;
                int channelHashCode = ctx.Channel.GetHashCode();

                while (!ResponseData.ContainsKey(channelHashCode) && Convert.ToInt64((DateTime.Now - startDateTime).TotalMilliseconds) < timeout)
                {
                    Thread.Sleep(1);
                }

                return GetResponse(ctx);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static string GetResponse(IChannelHandlerContext ctx)
        {
            string outString = string.Empty;

            try
            {
                int channelHashCode = ctx.Channel.GetHashCode();
                if (ResponseData.ContainsKey(channelHashCode))
                {
                    ResponseData.TryGetValue(channelHashCode, out outString);
                }
            }
            catch (Exception)
            {
                throw; 
            }

            return outString;
        }
    }
}