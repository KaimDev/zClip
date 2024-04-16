using System;
using zClip_Desktop.CustomEventArgs;
using zClip_Desktop.Interfaces;

namespace zClip_Desktop.Services
{
    public class SecurityService : ISecurityService
    {
        public event EventHandler<SecurityEventArgs> OnSecurityChange;
        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void GenerateSecurityCode()
        {
            throw new NotImplementedException();
        }

        public void ResetSecurityCode()
        {
            throw new NotImplementedException();
        }

        public void SaveTargetSecurityCode()
        {
            throw new NotImplementedException();
        }

        public void DeleteTargetSecurityCode()
        {
            throw new NotImplementedException();
        }
    }
}