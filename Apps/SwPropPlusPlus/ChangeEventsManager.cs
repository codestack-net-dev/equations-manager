using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeStack.Community.Sw.EqMgr
{
	internal class ChangeEventsManager : IDisposable
	{
		private Dictionary<IModelDoc2, ModelEventsManager> m_Register;

		internal ChangeEventsManager()
		{
			m_Register = new Dictionary<IModelDoc2, ModelEventsManager>();
		}

		internal void RegisterModel(IModelDoc2 model, bool reregister = false)
		{
			var alreadyRegistered = m_Register.ContainsKey(model);

			if (!alreadyRegistered)
			{
				m_Register.Add(model, CreateModelEventsManager(model));
			}
			else if (reregister)
			{
				DestroyInRegister(model);
				m_Register[model] = CreateModelEventsManager(model);
			}
		}

		private void DestroyInRegister(IModelDoc2 model)
		{
			m_Register[model].Destroy -= OnModelDestroyed;
			m_Register[model].Dispose();
		}

		private void OnModelDestroyed(IModelDoc2 model)
		{
			if (m_Register.ContainsKey(model))
			{
				DestroyInRegister(model);
				m_Register.Remove(model);
			}
			else
			{
				Debug.Assert(false, "Model must be in register");
			}
		}

		private ModelEventsManager CreateModelEventsManager(IModelDoc2 model)
		{
			var evMgr = new ModelEventsManager(model);
			evMgr.Destroy += OnModelDestroyed;

			return evMgr;
		}

		public void Dispose()
		{
			foreach (var model in m_Register.Keys)
			{
				DestroyInRegister(model);
			}

			m_Register.Clear();
			m_Register = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
	}
}
