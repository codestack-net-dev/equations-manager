using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xarial.Community.EqMgr.Core.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Xarial.Community.EqMgr.Core.Container.Tests
{
    [TestClass()]
    public class ServiceResolverTests
    {
        #region Mocks

        public interface IServiceMock
        {
        }

        public interface IService1Mock : IServiceMock
        {
        }

        public interface IService2Mock : IServiceMock
        {
            IService1Mock S1
            {
                get;
            }
        }

        public interface IService3Mock
        {
            IService1Mock S1
            {
                get;
            }

            IService2Mock S2
            {
                get;
            }
        }

        public interface IService4Mock
        {
            IServiceMock[] Services { get; }
        }

        public class Service1Mock : IService1Mock
        {
        }

        public class Service2Mock : IService2Mock
        {
            public IService1Mock S1 { get; private set; }

            public Service2Mock(IService1Mock s1)
            {
                S1 = s1;
            }
        }

        public class Service3Mock : IService3Mock
        {
            public IService1Mock S1 { get; private set; }
            public IService2Mock S2 { get; private set; }

            public Service3Mock(IService1Mock s1, IService2Mock s2)
            {
                S1 = s1;
                S2 = s2;
            }
        }

        public class Service4Mock : IService4Mock
        {
            public IServiceMock[] Services { get; private set; }

            public Service4Mock(IServiceMock[] services)
            {
                Services = services;
            }
        }

        #endregion

        [TestMethod]
        public void RegisterTypeTest()
        {
            var cont = new UnityContainer();

            var res = new ServiceResolver<IService1Mock>(cont);

            res.RegisterType<Service1Mock>();

            var i1 = cont.Resolve<IService1Mock>();
            var i2 = cont.Resolve<IService1Mock>();

            Assert.AreEqual(i1, i2);
        }

        [TestMethod]
        public void RegisterTypeChildContainerTest()
        {
            var cont = new UnityContainer();

            var res = new ServiceResolver<IService1Mock>(cont);

            res.RegisterType<Service1Mock>();

            var i1 = cont.Resolve<IService1Mock>();
            var i2 = cont.CreateChildContainer().Resolve<IService1Mock>();

            Assert.AreNotEqual(i1, i2);
        }

        [TestMethod]
        public void RegisterTypesWithDependenciesContainerTest()
        {
            var cont = new UnityContainer();

            var res1 = new ServiceResolver<IService1Mock>(cont);
            var res2 = new ServiceResolver<IService2Mock>(cont);
            var res3 = new ServiceResolver<IService3Mock>(cont);

            res1.RegisterType<Service1Mock>();
            res2.RegisterType<Service2Mock>();
            res3.RegisterType<Service3Mock>();

            var s1_1 = cont.Resolve<IService1Mock>();
            var s1_2 = cont.Resolve<IService2Mock>();

            var c = cont.CreateChildContainer();
            var s3_1 = c.Resolve<IService3Mock>();
            var s2_1 = c.Resolve<IService2Mock>();
            var s1_3 = c.Resolve<IService1Mock>();

            Assert.AreEqual(s1_1, s1_2.S1);
            Assert.AreEqual(s1_3, s2_1.S1);
            Assert.AreEqual(s3_1.S1, s1_3);
            Assert.AreEqual(s3_1.S2, s2_1);
            Assert.AreNotEqual(s1_2.S1, s2_1.S1);
            Assert.AreNotEqual(s1_1, s1_3);
        }

        [TestMethod]
        public void RegisterMultipleTypeTest()
        {
            var cont = new UnityContainer();

            var res1 = new ServiceResolver<IServiceMock>(cont);
            var res2 = new ServiceResolver<IService4Mock>(cont);

            res1.RegisterMultipleType<Service1Mock>();
            res1.RegisterMultipleType<Service2Mock>();
            res2.RegisterType<Service4Mock>();

            var s4 = cont.Resolve<IService4Mock>();

            Assert.IsNotNull(s4.Services);
            Assert.AreEqual(2, s4.Services.Length);
            //Assert.IsTrue(s4.Services.Contains(cont.Resolve<IService1Mock>()));
            //Assert.IsTrue(s4.Services.Contains(cont.Resolve<IService2Mock>()));
        }

        [TestMethod]
        public void RegisterInstanceTest()
        {
            var cont = new UnityContainer();

            var res = new ServiceResolver<IService1Mock>(cont);

            var inst = new Service1Mock();

            res.RegisterInstance(inst);

            var i1 = cont.Resolve<IService1Mock>();
            var i2 = cont.Resolve<IService1Mock>();
            var i3 = cont.CreateChildContainer().Resolve<IService1Mock>();

            Assert.AreEqual(inst, i1);
            Assert.AreEqual(i1, i2);
            Assert.AreEqual(i2, i3);
        }

        [TestMethod]
        public void RegisterSingletonTest()
        {
            var cont = new UnityContainer();

            var res = new ServiceResolver<IService1Mock>(cont);

            res.RegisterSingleton<Service1Mock>();

            var i1 = cont.Resolve<IService1Mock>();
            var i2 = cont.Resolve<IService1Mock>();
            var i3 = cont.CreateChildContainer().Resolve<IService1Mock>();

            Assert.AreEqual(i1, i2);
            Assert.AreEqual(i2, i3);
        }
    }
}