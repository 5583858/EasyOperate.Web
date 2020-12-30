using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common.Concurrency;
using DotNetty.Transport.Channels;
using EasyOperate.Web.DotNetty.Factory;
using System;
using System.Threading.Tasks;

namespace EasyOperate.Web.Manager
{
    public class HttpKeepAliveManager
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const int KEEP_ALIVE_CONNECTION_TIME_OUT = 10000;

        public async static void SendRequest(string serialNo, IFullHttpRequest request)
        {
            string responseJson = string.Empty;
            IChannelHandlerContext ctx = null;

            try
            {
                DeviceChannelContext deviceChannelContext = ChannelFactory.GetChannel(serialNo);
                if (deviceChannelContext != null)
                {
                    ctx = deviceChannelContext.Context;

                    if (ctx == null)
                    {
                        //return null;
                    }
                }

                ChannelFactory.UnlockChannel(serialNo);

                if (ctx.Channel.IsWritable)
                {
                    await ctx.WriteAndFlushAsync(request);

                    logger.Debug($"Request Header -> {request.ToString()}");

                    responseJson = HttpResponseFactory.GetResponse(ctx, KEEP_ALIVE_CONNECTION_TIME_OUT);

                    logger.Debug($"Response Json -> {responseJson}");
                }
                else
                {
                    await ctx.Channel.CloseAsync();
                    deviceChannelContext.Context = null;
                }

                if (string.IsNullOrEmpty(responseJson))
                {

                }
            }
            catch (Exception e)
            {
                logger.Debug(e.Message, e);
            }
            finally
            {
                HttpResponseFactory.RemoveResponse(ctx);
                ChannelFactory.UnlockChannel(serialNo);
            }
        }
    }
}