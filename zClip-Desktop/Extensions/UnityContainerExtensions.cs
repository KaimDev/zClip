using System;
using System.Net;
using Unity;
using zClip_Desktop.Helpers;
using System.Linq;
using System.Net.Http;
using Unity.Injection;
using zClip_Desktop.Interfaces;
using zClip_Desktop.Services;

namespace zClip_Desktop.Extensions
{
    public static class UnityContainerExtensions
    {
        public static void RegisterOwnIp(this IUnityContainer container)
        {
            try
            {
                var iPHost = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = (from ip in iPHost.AddressList
                    where ip.ToString().StartsWith("192.168")
                    select ip).First().ToString();

                container.RegisterType<OwnIpAddress>(new InjectionConstructor(ipAddress));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ZClipSettings.IsEthernet = false;
                string ipAddress = "LAN NETWORK IS NOT DETECTED";
                container.RegisterType<OwnIpAddress>(new InjectionConstructor(ipAddress));
            }
        }

        public static void RegisterListenerService(this IUnityContainer container)
        {
            if (!ZClipSettings.IsEthernet) return;
            
            var ownIpAddress = container.Resolve(typeof(OwnIpAddress));
            container.RegisterSingleton<IListenerService, ListenerService>(
                new InjectionConstructor(ownIpAddress, typeof(HttpListener)));
        }

        public static void RegisterClipboardService(this IUnityContainer container)
        {
            container.RegisterSingleton(typeof(IClipboardService), typeof(ClipboardService));
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
            container.RegisterType<IClientService, ClientService>(
                new InjectionConstructor(targetIp, typeof(HttpClient)));
        }

        public static void RegisterSecurityService(this IUnityContainer container)
        {
            container.RegisterType<ISecurityService, SecurityService>();
        }

        public static void RegisterSyncService(this IUnityContainer container)
        {
            var client = container.Resolve(typeof(IClientService));
            var clipboard = container.Resolve(typeof(IClipboardService));
            var listener = container.Resolve(typeof(IListenerService));
            var security = container.Resolve(typeof(ISecurityService));

            container.RegisterSingleton<ISyncService, SyncService>(new InjectionConstructor(
                client,
                listener,
                clipboard,
                security
            ));
        }
    }
}