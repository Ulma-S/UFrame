using System;
using System.Collections.Generic;

namespace uframe
{
	public class cInputCommandHolder<T> where T : Enum
	{
		public static cInputCommandHolder<T> Create()
		{
			return new cInputCommandHolder<T>();
		}

		public void RegisterCommand(T commandType, Func<bool>[] conditions)
		{
			var key = Convert.ToInt32(commandType);
			if (_CommandHolder.ContainsKey(key))
			{
				LogService.Error($"{commandType}ÇÕìoò^çœÇ›Ç≈Ç∑");
				return;
			}
			_CommandHolder.Add(key, conditions);
		}

		public bool IsCommandSuccess(int commandType)
		{
			if (_CommandHolder.TryGetValue(commandType, out var conditions))
			{
				foreach (var condition in conditions)
				{
					if (condition.Invoke())
					{
						return true;
					}
				}
			}
			return false;
		}

		private cInputCommandHolder() { }

		private Dictionary<int, Func<bool>[]> _CommandHolder = new Dictionary<int, Func<bool>[]>();
	}
}