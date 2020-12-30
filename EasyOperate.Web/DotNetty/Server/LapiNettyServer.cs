using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace EasyOperate.Web.DotNetty.Server
{
    public class LapiNettyServer
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static LapiNettyServer Instance => new LapiNettyServer();

        private const int BOSS_GROUP_THREAD_NUM = 32;
        private const int WORK_GROUP_THREAD_NUM = 64;
        private const int DEAL_BLOCK_SIZE = 256;

        private ManualResetEvent ClosingArrivedEvent = new ManualResetEvent(false);

        public void Start()
        {
            try
            {
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    RunServerAsync().Wait();
                });
            }
            catch (Exception e)
            {
                logger.Debug(e.Message, e);
                throw;
            }
        }

        public async Task RunServerAsync()
        {
            IEventLoopGroup bossGroup;
            IEventLoopGroup workerGroup;

            bossGroup = new MultithreadEventLoopGroup(BOSS_GROUP_THREAD_NUM);
            workerGroup = new MultithreadEventLoopGroup(WORK_GROUP_THREAD_NUM);

            try
            {
                var serverPort = Convert.ToInt32(ConfigurationManager.AppSettings["LapiNettyServerPort"]);

                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workerGroup)
                .Channel<TcpServerSocketChannel>()
                //服务初始化通道处理
                .ChildHandler(new LapiServerInitializer())
                //等待处理的队列大小
                .Option(ChannelOption.SoBacklog, DEAL_BLOCK_SIZE)
                //Boss线程内存池配置
                .Option(ChannelOption.Allocator, PooledByteBufferAllocator.Default)
                //Work线程内存池配置
                .ChildOption(ChannelOption.Allocator, PooledByteBufferAllocator.Default)
                //是否启用心跳保活机制。在双方TCP套接字建立连接后（即都进入ESTABLISHED状态）并且在两个小时左右上层没有任何数据传输的情况下，这套机制才会被激活。
                .Option(ChannelOption.SoKeepalive, true);

                logger.Debug($"Server Bind Start. Port:{serverPort}");
                IChannel boundChannel = await bootstrap.BindAsync(serverPort);

                ClosingArrivedEvent.Reset();
                ClosingArrivedEvent.WaitOne();
                await boundChannel.CloseAsync();
            }
            catch (Exception e)
            {
                logger.Debug(e.Message, e);
                throw;
            }
            finally
            {
                await Task.WhenAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
            }
        }
    }
}