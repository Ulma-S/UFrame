using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cSceneStateBase
	{
		public void Enter()
		{
			OnEnter();
		}

		public void Update()
		{
			OnUpdate();
		}

		public void Exit()
		{
			OnExit();
		}

		/// <summary>
		/// シーン遷移を記述する
		/// </summary>
		/// <param name="nextScenePackID"></param>
		/// <returns>遷移させたい時はtrueを返し、out引数で遷移先シーンを設定する</returns>
		public virtual bool CheckSceneTransition(out SceneDef.PACK_ID nextScenePackID)
		{
			nextScenePackID = SceneDef.PACK_ID.INVALID;
			return false;
		}

		protected virtual void OnEnter() { }

		protected virtual void OnUpdate() { }

		protected virtual void OnExit() { }
	}
}