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
		/// �V�[���J�ڂ��L�q����
		/// </summary>
		/// <param name="nextScenePackID"></param>
		/// <returns>�J�ڂ�����������true��Ԃ��Aout�����őJ�ڐ�V�[����ݒ肷��</returns>
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