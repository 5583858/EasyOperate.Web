using DotNetty.Transport.Channels;
using EasyOperate.Web.Manager;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EasyOperate.Web.DotNetty.Factory
{
    public class ChannelFactory
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static Dictionary<string, DeviceChannelContext> ChannelDictionary = new Dictionary<string, DeviceChannelContext>(256);

        public static void AddChannel(string serialNo, IChannelHandlerContext ctx)
        {
            if (!String.IsNullOrEmpty(serialNo) && ctx != null)
            {
                if (ChannelDictionary.ContainsKey(serialNo))
                {
                    DeviceChannelContext deviceChannelContext = ChannelDictionary[serialNo];
                    deviceChannelContext.Context = ctx;
                    deviceChannelContext.IsBound = false;
                }
                else
                {
                    DeviceChannelContext deviceChannelContext = new DeviceChannelContext(ctx, false, serialNo);
                    ChannelDictionary.Add(serialNo, deviceChannelContext);
                }
            }
            else
            {

            }
        }

        public static void FreshChannel(string serialNo, IChannelHandlerContext ctx)
        {
            DeviceChannelContext deviceChannelContext = GetChannel(serialNo);
            if (deviceChannelContext != null)
            {
                deviceChannelContext.Context = ctx;
            }
            else
            {
                AddChannel(serialNo, ctx);
            }
        }

        public static DeviceChannelContext GetChannel(string serialNo)
        {

            if (String.IsNullOrEmpty(serialNo))
            {
                return null;
            }

            return ChannelDictionary.ContainsKey(serialNo) ? ChannelDictionary[serialNo] : null;
        }

        public static void UnlockChannel(string serialNo)
        {
            DeviceChannelContext deviceChannelContext = GetChannel(serialNo);
            if (deviceChannelContext != null)
            {
                deviceChannelContext.IsLock = false;
            }
            else
            {
                Thread.Sleep(1);
            }
        }
    }
}