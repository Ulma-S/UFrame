using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cDebugCommandHolder
	{
		public void RegisterDebugCommand(DEBUG_COMMAND_TYPE commandType, Func<bool>[] conditions)
		{
			if (_DebugCommandHolder.ContainsKey((int)commandType))
			{
				LogService.Error($"{commandType}ÇÕìoò^çœÇ›Ç≈Ç∑");
				return;
			}
			_DebugCommandHolder.Add((int)commandType, conditions);
		}

		public bool IsCommandSuccess(DEBUG_COMMAND_TYPE commandType)
		{
#if UNITY_EDITOR
			if (_DebugCommandHolder.TryGetValue((int)commandType, out var conditions))
			{
				foreach (var condition in conditions)
				{
					if (condition.Invoke())
					{
						return true;
					}
				}
			}
#endif
			return false;
		}

		private Dictionary<int, Func<bool>[]> _DebugCommandHolder = new Dictionary<int, Func<bool>[]>();
	}
}