﻿namespace zClip_Desktop.Helpers
{
    public class OwnIpAddress
    {
        public string IpAddress { get; set; }
        public static readonly string Port = "1705";
        
        public OwnIpAddress(string ipAddress)
        {
            IpAddress = ipAddress;
        }
    }
}