using DotNetty.Codecs.Http;
using DotNetty.Transport.Channels;
using EasyOperate.Web.DotNetty.Factory;
using EasyOperate.Web.Manager;
using EasyOperate.Web.Models.AccessControlModel;
using EasyOperate.Web.Models.AccessControlRequest;
using Newtonsoft.Json;
using System;
using System.Text;

namespace EasyOperate.Web.DotNetty.Server
{
    public class LapiServerHandler : SimpleChannelInboundHandler<object>
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 通道被添加
        /// </summary>
        /// <param name="context"></param>
        public override void HandlerAdded(IChannelHandlerContext context)
        {
            logger.Debug($"Channel [{context.Channel.RemoteAddress}] Added.");
        }

        /// <summary>
        /// 通道被删除
        /// </summary>
        /// <param name="context"></param>
        public override void HandlerRemoved(IChannelHandlerContext context)
        {
            logger.Debug($"Channel [{context.Channel.RemoteAddress}] Removed.");
        }

        /// <summary>
        /// 服务端接收到客户端掉线通知
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            logger.Debug($"Channel [{context.Channel.RemoteAddress}] Inactive.");
        }

        /// <summary>
        /// 服务端接收到客户端上线通知
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelActive(IChannelHandlerContext context)
        {
            logger.Debug($"Channel [{context.Channel.RemoteAddress}] Active.");
        }

        /// <summary>
        /// 异常断开
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            logger.Debug($"Channel [{context.Channel.RemoteAddress}] ExceptionCaught.");

            if (!context.Channel.IsWritable)
            {
                context.CloseAsync();
            }
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            try
            {
                if (msg is IFullHttpRequest request)
                {
                    string requestUri = request.Uri;
                    logger.Debug($"RequestUri:{requestUri}");

                    string requestContent = request.Content.ToString(Encoding.UTF8);
                    logger.Debug($"RequestContent:{requestContent}");

                    if (requestUri.Contains(BasicRequestUrl.HeartReportInfoUrl))
                    {
                        SaveDeviceCtx(requestContent, ctx);
                        HeartResponse(ctx);
                    }
                    else if (requestUri.Contains(BasicRequestUrl.PushAccessControlRecordUrl))
                    {

                    }
                    else
                    {
                        //ctx.FireChannelRead(msg);
                    }
                }
                else if (msg is IFullHttpResponse response)
                {
                    HttpResponseFactory.SaveResponse(ctx, response);
                }
                else
                {
                    //ctx.FireChannelRead(msg);
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.Message, e);
            }
        }

        private void SaveDeviceCtx(string requestContent, IChannelHandlerContext ctx)
        {
            HeartReportInfoModel heartRequestModels = JsonConvert.DeserializeObject<HeartReportInfoModel>(requestContent);
            string serialNo = heartRequestModels.DeviceCode;
            ChannelFactory.FreshChannel(serialNo, ctx);
        }

        private void HeartResponse(IChannelHandlerContext ctx)
        {
            HeartReportResponseModel heartResponseModels = new HeartReportResponseModel(BasicRequestUrl.HeartReportInfoUrl, 0, DateTime.Now);
            ResponseDeviceManager.ResponseDevice<HeartReportResponseModel>(heartResponseModels, ctx);
        }
    }
}