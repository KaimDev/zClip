using System;
using zClip_Desktop.CustomEventArgs;

namespace zClip_Desktop.Interfaces
{
    public interface ISecurityService
    {
        event EventHandler<SecurityEventArgs> OnSecurityChange;
        void Start();
        void Stop();
        void GenerateSecurityCode();
        void ResetSecurityCode();
        void SaveTargetSecurityCode();
        void DeleteTargetSecurityCode();
    }
}