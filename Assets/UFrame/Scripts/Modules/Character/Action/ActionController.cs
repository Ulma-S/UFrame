using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cActionController
	{
		public static cActionController Create(CharacterBase character)
		{
			return new cActionController(character);
		}

		public void Setup(ACTION_ID actionID)
		{
			CurrentActionID = actionID;
			_CurrentAction = _ActionContainer.find(actionID);
		}

		public void Update()
		{
			if (_CurrentAction == null)
			{
				return;
			}

			if (IsActionEnd)
			{
				IsActionEnd = false;
			}
			if (_IsActionChanged)
			{
				_CurrentAction.Enter();
				_IsActionChanged = false;
			}
			var result = _CurrentAction.Update();
			if (result || _RequestedActionID != ACTION_ID.Invalid)
			{
				_CurrentAction.Exit();
			}
			if (result)
			{
				IsActionEnd = true;
			}
			if (_RequestedActionID != ACTION_ID.Invalid)
			{
				var nextAction = _ActionContainer.find(_RequestedActionID);
				_CurrentAction = nextAction;
				PrevActionID = CurrentActionID;
				CurrentActionID = _RequestedActionID;
				_RequestedActionID = ACTION_ID.Invalid;
				_IsActionChanged = true;
			}
		}

		public void Register<T>(ACTION_ID actionID) where T : cActionBase
		{
			_ActionContainer.Register<T>(actionID);
		}

		public void RequestSetAction(ACTION_ID actionID)
		{
			_RequestedActionID = actionID;
		}

		private cActionController(CharacterBase character)
		{
			_ActionContainer = cActionContainer.Create(character);
		}

		public ACTION_ID CurrentActionID
		{
			get;
			private set;
		} = ACTION_ID.Invalid;

		public ACTION_ID PrevActionID
		{
			get;
			private set;
		} = ACTION_ID.Invalid;

		public bool IsActionEnd
		{
			get;
			private set;
		} = false;

		private cActionContainer _ActionContainer = null;
		private cActionBase _CurrentAction = null;
		private ACTION_ID _RequestedActionID = ACTION_ID.Invalid;
		private bool _IsActionChanged = false;
	}
}