using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xarial.Community.EqMgr.Core.Container
{
    public interface IServiceResolver<TService>
    {
        Type GetServiceType();
        //TODO: allow to register instance
    }

    public class ServiceResolver<TService> : IServiceResolver<TService>
    {
        public static IServiceResolver<TService> Create<TServiceImp>()
            where TServiceImp : class, TService
        {
            return new ServiceResolver<TService, TServiceImp>();
        }

        private Type m_Type;

        internal ServiceResolver(Type type)
        {
            m_Type = type;

            if (!typeof(TService).IsAssignableFrom(type))
            {
                throw new InvalidCastException($"{type.FullName} type must implement {typeof(TService).FullName}");
            }
        }

        public Type GetServiceType()
        {
            return m_Type;
        }
    }

    public class ServiceResolver<TService, TServiceImp> : IServiceResolver<TService>
        where TServiceImp : class, TService
    {
        public static IServiceResolver<TService> Create()
        {
            return new ServiceResolver<TService, TServiceImp>();
        }

        public Type GetServiceType()
        {
            return typeof(TServiceImp);
        }
    }
}
