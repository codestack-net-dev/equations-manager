using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swpublished;
using System;
using System.Runtime.InteropServices;

namespace CodeStack.Community.Sw.EqMgr
{
	[ComVisible(true), Guid("3F6A5F25-1C89-4B90-B818-93AD08760FCF")]
	public class SwPropPlusPlusMacroFeature : ISwComFeature
	{
		private ChangeEventsManager m_EventsMgr;

		private ChangeEventsManager EventsManager
		{
			get
			{
				return m_EventsMgr ?? (m_EventsMgr = new ChangeEventsManager());
			}
		}
		
		public object Edit(object app, object modelDoc, object feature)
		{
			return null;
		}

		public object Regenerate(object app, object modelDoc, object feature)
		{
			EventsManager.RegisterModel(modelDoc as IModelDoc2, true);

			return null;
		}

		public object Security(object app, object modelDoc, object feature)
		{
            //subscribe for events when model first loaded so properties can be updated without regenerate
			EventsManager.RegisterModel(modelDoc as IModelDoc2);

			return (int)swMacroFeatureSecurityOptions_e.swMacroFeatureSecurityByDefault;
		}
	}
}
