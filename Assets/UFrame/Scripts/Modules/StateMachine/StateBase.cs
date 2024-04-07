using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public abstract class cStateBase
	{
		public void Setup(cStateMachine stateMachine)
		{
			StateMachine = stateMachine;
		}

		public virtual void OnEnter() { }

		public virtual void OnUpdate() { }

		public virtual void OnExit() { }

		protected cStateMachine StateMachine
		{
			get;
			private set;
		} = null;
	}
}