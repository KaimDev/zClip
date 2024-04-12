using System.Net;
using Unity;
using zClip_Desktop.Helpers;
using System.Linq;

namespace zClip_Desktop.Extensions
{
    public static class UnityContainerExtensions
    {
        public static void ConfigureOwnIp(this IUnityContainer container)
        {
            var IPHost = Dns.GetHostEntry(Dns.GetHostName());

            string IpAddress = (from ipAddres in IPHost.AddressList
                where ipAddres.ToString().StartsWith("192.168")
                select ipAddres).First().ToString();
            
            container.RegisterInstance(new OwnIpAddress { IpAddress = IpAddress.ToString() });
        }

        public static void ConfigureSyncService(this IUnityContainer container, string deviceIP)
        {
            container.RegisterInstance(SyncService.GetInstance(deviceIP));
        }

        public static void ConfigureHttpServer(this IUnityContainer container, string ipAddress)
        {
            container.RegisterInstance(new HttpServer(ipAddress));
        }
    }
}