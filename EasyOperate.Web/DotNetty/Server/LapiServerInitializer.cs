using DotNetty.Codecs.Http;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using EasyOperate.Web.DotNetty.Codecs;
using System;

namespace EasyOperate.Web.DotNetty.Server
{
    public class LapiServerInitializer : ChannelInitializer<IChannel>
    {
        private const int READ_TIME_OUT = 60;

        protected override void InitChannel(IChannel channel)
        {
            try
            {
                IChannelPipeline pipeline = channel.Pipeline;
                //通道空闲超时时间
                pipeline.AddLast(new IdleStateHandler(READ_TIME_OUT, READ_TIME_OUT, READ_TIME_OUT));
                pipeline.AddLast(new HttpDecoder()); 
                pipeline.AddLast(new HttpEncoder<object>());
                pipeline.AddLast(new HttpObjectAggregator(10 * 1024 * 1024));
                pipeline.AddLast(new LapiServerHandler());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}