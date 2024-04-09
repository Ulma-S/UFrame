using System;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
	public class cGameFlowBase
	{
		public static cGameFlowBase Create<T>() where T : cGameFlowBase, new()
		{
			var flow = new T();
			flow.Setup();
			return flow;
		}

		public void Setup()
		{
			OnSetup();
		}

		public void Enter()
		{
			OnEnter();
		}

		public bool Update()
		{
			return OnUpdate();
		}

		protected virtual void OnSetup() { }

		protected virtual void OnEnter() { }

		protected virtual bool OnUpdate() { return true; }

		protected cGameFlowBase() { }
	}
}