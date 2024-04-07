using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cMicroModuleProcessor
	{
		private class cModuleHolder
		{
			public cMicroModule Module
			{
				get;
				set;
			} = null;

			public int ExecutionOrder
			{
				get;
				set;
			} = 0;
		}

		public void UpdateModules()
		{
			foreach (var moduleHolder in _Modules)
			{
				var module = moduleHolder.Module;
				if (!module.IsEnabled)
				{
					continue;
				}
				var result = module.Update();
				if (result)
				{
					module.Deactivate();
				}
			}
		}

		public void Register(cMicroModule module, int executionOrder = 0)
		{
			var index = 0;
			for (int i = 0, count = _Modules.Count; i < count; i++)
			{
				if (executionOrder <= _Modules[i].ExecutionOrder)
				{
					index = i;
					break;
				}
			}
			var holder = new cModuleHolder()
			{
				Module = module,
				ExecutionOrder = executionOrder,
			};
			_Modules.Insert(index, holder);
		}

		private List<cModuleHolder> _Modules = new List<cModuleHolder>();
	}
}