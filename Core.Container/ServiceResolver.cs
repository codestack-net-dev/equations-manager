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

        /// <summary>
        /// Registers service via type per <see cref="IEquationsManager"/>
        /// </summary>
        /// <typeparam name="TServiceImp">Type of the service</typeparam>
        public void RegisterType<TServiceImp>()
            where TServiceImp : class, TService
        {
            m_Container.RegisterType<TService, TServiceImp>(
                new HierarchicalLifetimeManager());
        }

        /// <summary>
        /// Registers multiple instances of the service
        /// </summary>
        /// <typeparam name="TServiceImp">Type of the service</typeparam>
        /// <remarks>This will be injected as the array of <see cref="TService"/></remarks>
        public void RegisterMultipleType<TServiceImp>()
            where TServiceImp : class, TService
        {
            //NOTE: for resolving the array of dependencies it is required to specify the name

            m_Container.RegisterType<TService, TServiceImp>(
                Guid.NewGuid().ToString(),
                new HierarchicalLifetimeManager());
        }

        /// <summary>
        /// Registers service as an instance per <see cref="EquationsManagerContainer"/>
        /// </summary>
        /// <param name="inst">Instance to register</param>
        public void RegisterInstance(TService inst)
        {
            m_Container.RegisterInstance(inst);
        }

        /// <summary>
        /// Registers the service by type as a singleton per <see cref="EquationsManagerContainer"/>
        /// </summary>
        /// <typeparam name="TServiceImp">Service type</typeparam>
        public void RegisterSingleton<TServiceImp>()
            where TServiceImp : class, TService
        {
            m_Container.RegisterType<TService, TServiceImp>(
                new ContainerControlledLifetimeManager());
        }
    }
}
