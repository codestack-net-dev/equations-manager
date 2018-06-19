using SolidWorks.Interop.sldworks;
using System;
using System.Windows.Forms;

namespace CodeStack.Community.Sw.EqMgr
{
	internal class ModelEventsManager : IDisposable
	{
		private IModelDoc2 m_Model;

		internal event Action<IModelDoc2> Destroy;

		internal ModelEventsManager(IModelDoc2 model)
		{
			m_Model = model;
			AttachEvents();
		}

		private void AttachEvents()
		{
			if (m_Model is IPartDoc)
			{
				(m_Model as PartDoc).AddCustomPropertyNotify += OnCustomPropertyAdded;
				(m_Model as PartDoc).ChangeCustomPropertyNotify += OnCustomPropertyChanged;
				(m_Model as PartDoc).DeleteCustomPropertyNotify += OnCustomPropertyDeleted;

				(m_Model as PartDoc).DimensionChangeNotify += OnDimensionChanged;

				(m_Model as PartDoc).DestroyNotify2 += OnDestroyNotify2;
			}
			else if (m_Model is IAssemblyDoc)
			{
				(m_Model as AssemblyDoc).AddCustomPropertyNotify += OnCustomPropertyAdded;
				(m_Model as AssemblyDoc).ChangeCustomPropertyNotify += OnCustomPropertyChanged;
				(m_Model as AssemblyDoc).DeleteCustomPropertyNotify += OnCustomPropertyDeleted;

				(m_Model as AssemblyDoc).DimensionChangeNotify += OnDimensionChanged;

				(m_Model as AssemblyDoc).DestroyNotify2 += OnDestroyNotify2;
			}
			else if (m_Model is IDrawingDoc)
			{
				(m_Model as DrawingDoc).AddCustomPropertyNotify += OnCustomPropertyAdded;
				(m_Model as DrawingDoc).ChangeCustomPropertyNotify += OnCustomPropertyChanged;
				(m_Model as DrawingDoc).DeleteCustomPropertyNotify += OnCustomPropertyDeleted;

				(m_Model as DrawingDoc).DimensionChangeNotify += OnDimensionChanged;

				(m_Model as DrawingDoc).DestroyNotify2 += OnDestroyNotify2;
			}
		}

		private int OnDestroyNotify2(int DestroyType)
		{
			Destroy?.Invoke(m_Model);

			return 0;
		}

		private int OnDimensionChanged(object displayDim)
		{
			var dispDim = displayDim as IDisplayDimension;
			var dim = (displayDim as IDisplayDimension).GetDimension2(0);

			MessageBox.Show($"Dimension '{dim.Name}' changed in '{m_Model.GetTitle()}'");

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			GC.WaitForPendingFinalizers();

			return 0;
		}

		private int OnCustomPropertyDeleted(string propName, string Configuration, string Value, int valueType)
		{
			MessageBox.Show($"'{propName}' property deleted in '{m_Model.GetTitle()}'");
			return 0;
		}

		private int OnCustomPropertyChanged(string propName, string Configuration, string oldValue, string NewValue, int valueType)
		{
			MessageBox.Show($"'{propName}' property changed in '{m_Model.GetTitle()}'");
			return 0;
		}

		private int OnCustomPropertyAdded(string propName, string Configuration, string Value, int valueType)
		{
			MessageBox.Show($"'{propName}' property added in '{m_Model.GetTitle()}'");
			return 0;
		}

		private void DetachEvents()
		{
			if (m_Model is IPartDoc)
			{
				(m_Model as PartDoc).AddCustomPropertyNotify -= OnCustomPropertyAdded;
				(m_Model as PartDoc).ChangeCustomPropertyNotify -= OnCustomPropertyChanged;
				(m_Model as PartDoc).DeleteCustomPropertyNotify -= OnCustomPropertyDeleted;

				(m_Model as PartDoc).DimensionChangeNotify -= OnDimensionChanged;

				(m_Model as PartDoc).DestroyNotify2 += OnDestroyNotify2;
			}
			else if (m_Model is IAssemblyDoc)
			{
				(m_Model as AssemblyDoc).AddCustomPropertyNotify -= OnCustomPropertyAdded;
				(m_Model as AssemblyDoc).ChangeCustomPropertyNotify -= OnCustomPropertyChanged;
				(m_Model as AssemblyDoc).DeleteCustomPropertyNotify -= OnCustomPropertyDeleted;

				(m_Model as AssemblyDoc).DimensionChangeNotify -= OnDimensionChanged;

				(m_Model as AssemblyDoc).DestroyNotify2 += OnDestroyNotify2;
			}
			else if (m_Model is IDrawingDoc)
			{
				(m_Model as DrawingDoc).AddCustomPropertyNotify -= OnCustomPropertyAdded;
				(m_Model as DrawingDoc).ChangeCustomPropertyNotify -= OnCustomPropertyChanged;
				(m_Model as DrawingDoc).DeleteCustomPropertyNotify -= OnCustomPropertyDeleted;

				(m_Model as DrawingDoc).DimensionChangeNotify -= OnDimensionChanged;

				(m_Model as DrawingDoc).DestroyNotify2 += OnDestroyNotify2;
			}
		}

		public void Dispose()
		{
			DetachEvents();

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
	}
}
