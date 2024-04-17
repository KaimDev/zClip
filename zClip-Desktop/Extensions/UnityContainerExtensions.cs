﻿using System.Net;
using Unity;
using zClip_Desktop.Helpers;
using System.Linq;
using Unity.Injection;
using zClip_Desktop.Interfaces;
using zClip_Desktop.Services;

namespace zClip_Desktop.Extensions
{
    public static class UnityContainerExtensions
    {
        public static void ConfigureOwnIp(this IUnityContainer container)
        {
            var IPHost = Dns.GetHostEntry(Dns.GetHostName());

            string IpAddress = (from ipAddress in IPHost.AddressList
                where ipAddress.ToString().StartsWith("192.168")
                select ipAddress).First().ToString();

            container.RegisterInstance(new OwnIpAddress { IpAddress = IpAddress.ToString() });
        }

        /// <summary>
        ///     Register the target IP address
        /// </summary>
        /// <param name="container"></param>
        /// <param name="targetIp"></param>
        public static void RegisterTargetIp(this IUnityContainer container, string targetIp)
        {
            container.RegisterType<TargetIpAddress>(new InjectionConstructor(targetIp));
        }

        public static void RegisterClientType(this IUnityContainer container)
        {
            var targetIp = container.Resolve<TargetIpAddress>();
            container.RegisterType<IClientService, ClientService>(new InjectionConstructor(targetIp));
        }

        public static void ConfigureSyncService(this IUnityContainer container)
        {
            var client = container.Resolve(typeof(IClientService));
            var clipboard = container.Resolve(typeof(IClipboardService));
            var listener = container.Resolve(typeof(IListenerService));
            var security = container.Resolve(typeof(ISecurityService));

            container.RegisterSingleton<ISyncService, SyncService>(new InjectionConstructor(
                client,
                clipboard,
                listener,
                security
            ));
        }

        public static void ConfigureHttpServer(this IUnityContainer container)
        {
            var ownIpAddress = (OwnIpAddress)container.Resolve(typeof(OwnIpAddress));
            container.RegisterInstance(new ListenerService(ownIpAddress));
        }

        public static void ConfigureClipboardService(this IUnityContainer container)
        {
            container.RegisterSingleton(typeof(IClipboardService), typeof(ClipboardService));
        }
    }
}