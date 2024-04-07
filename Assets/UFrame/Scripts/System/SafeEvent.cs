using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cSafeEvent
	{
		public cSafeEvent() { }

		public void Invoke()
		{
			_Action?.Invoke();
		}

		public void Register(Action action)
		{
			_Action += action;
			var handler = new cSafeEventHandler(this);
			_ActionHolder.Add(handler, action);
		}

		public void Release(cSafeEventHandler handler)
		{
			if (_ActionHolder.TryGetValue(handler, out var action))
			{
				_Action -= action;
				_ActionHolder.Remove(handler);
			}
		}

		private Action _Action = null;
		private Dictionary<cSafeEventHandler, Action> _ActionHolder = new Dictionary<cSafeEventHandler, Action>();
	}

	public class cSafeEvent<T>
	{
		public cSafeEvent() { }

		public void Invoke(T arg)
		{
			_Action?.Invoke(arg);
		}

		public void Register(Action<T> action)
		{
			_Action += action;
			var handler = new cSafeEventHandler<T>(this);
			_ActionHolder.Add(handler, action);
		}

		public void Release(cSafeEventHandler<T> handler)
		{
			if (_ActionHolder.TryGetValue(handler, out var action))
			{
				_Action -= action;
				_ActionHolder.Remove(handler);
			}
		}

		private Action<T> _Action = null;
		private Dictionary<cSafeEventHandler<T>, Action<T>> _ActionHolder = new Dictionary<cSafeEventHandler<T>, Action<T>>();
	}

	public class cSafeEventHandler
	{
		public cSafeEventHandler(cSafeEvent safeEvent)
		{
			_SafeEvent = safeEvent;
		}

		~cSafeEventHandler()
		{
			_SafeEvent.Release(this);
		}

		private cSafeEvent _SafeEvent = null;
	}

	public class cSafeEventHandler<T> : cSafeEvent
	{
		public cSafeEventHandler(cSafeEvent<T> safeEvent)
		{
			_SafeEvent = safeEvent;
		}

		~cSafeEventHandler()
		{
			_SafeEvent.Release(this);
		}

		private cSafeEvent<T> _SafeEvent = null;
	}
}