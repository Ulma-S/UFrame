using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cActionBase
	{
		public void Setup(CharacterBase character)
		{
			Chara = character;
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

		public void Exit()
		{
			OnExit();
		}

		protected virtual void OnSetup() { }
		protected virtual void OnEnter() { }
		protected virtual bool OnUpdate() { return false; }
		protected virtual void OnExit() { }

		protected cActionBase() { }

		protected CharacterBase Chara
		{
			get;
			private set;
		}
	}
}