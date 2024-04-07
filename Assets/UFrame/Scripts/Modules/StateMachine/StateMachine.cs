using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	/// <summary>
	/// ���T�v
	/// �ȈՓI�ȃX�e�[�g�}�V��
	/// </summary>
	public class cStateMachine
	{
		public cStateMachine(GameObject owner)
		{
			_Owner = owner;
		}

		public void SetStartState(int stateType)
		{
			CurrentStateType = stateType;
			_CurrentState = _StateHolder[stateType];
		}

		public void Update()
		{
			_CurrentState.OnUpdate();
		}

		public void RequestChangeState(int nextStateType)
		{
			if (_StateHolder.TryGetValue(nextStateType, out var nextState))
			{
				_CurrentState.OnExit();
				CurrentStateType = nextStateType;
				_CurrentState = nextState;
				_CurrentState.OnEnter();
			}
			else
			{
				LogService.Error(_Owner, $"�X�e�[�g{nextStateType}�͖��o�^�ł�");
			}
		}

		public void RegisterState(int stateType, cStateBase state)
		{
			if (_StateHolder.ContainsKey(stateType))
			{
				LogService.Error(_Owner, $"�X�e�[�g{stateType}�͓o�^�ς݂ł�");
				return;
			}
			state.Setup(this);
			_StateHolder.Add(stateType, state);
		}

		public int CurrentStateType
		{
			get;
			private set;
		} = -1;

		private Dictionary<int, cStateBase> _StateHolder = new Dictionary<int, cStateBase>();
		private GameObject _Owner = null;
		private cStateBase _CurrentState = null;
	}
}