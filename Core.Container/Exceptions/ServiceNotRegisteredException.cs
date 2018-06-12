using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xarial.Community.EqMgr.Core.Container.Exceptions
{
    public class ServiceNotRegisteredException : NullReferenceException
    {
        public ServiceNotRegisteredException(Type serviceType) : 
            base($"{serviceType.FullName} service is not registered. User {typeof(ServiceResolver<>).FullName} to register the dependency")
        {
        }
    }
}
