using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace Xarial.Community.EqMgr.Core.Container
{
    public class ServiceResolver<TService>
    {
        private UnityContainer m_Container;

        internal ServiceResolver(UnityContainer container)
        {
            m_Container = container;
        }

        public void RegisterType<TServiceImp>()
            where TServiceImp : class, TService
        {
            m_Container.RegisterType<TService, TServiceImp>(
                new HierarchicalLifetimeManager());
        }

        public void RegisterMultipleType<TServiceImp>()
            where TServiceImp : class, TService
        {
            //NOTE: for resolving the array of dependencies it is required to specify the name

            m_Container.RegisterType<TService, TServiceImp>(
                Guid.NewGuid().ToString(),
                new HierarchicalLifetimeManager());
        }

        public void RegisterInstance(TService inst)
        {
            m_Container.RegisterInstance(inst);
        }

        public void RegisterSingleton<TServiceImp>()
            where TServiceImp : class, TService
        {
            m_Container.RegisterType<TService, TServiceImp>(
                new ContainerControlledLifetimeManager());
        }
    }
}
