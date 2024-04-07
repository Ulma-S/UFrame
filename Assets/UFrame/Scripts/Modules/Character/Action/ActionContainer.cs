using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cActionContainer
	{
		public static cActionContainer Create(CharacterBase character)
		{
			return new cActionContainer(character);
		}

		public void Register<T>(ACTION_ID actionID) where T : cActionBase
		{
			var action = Activator.CreateInstance(typeof(T)) as cActionBase;
			action.Setup(_OwnerCharacter);
			_ActionHolder.Add(actionID.Index, action);
		}

		public cActionBase find(ACTION_ID actionID)
		{
			if (_ActionHolder.TryGetValue(actionID.Index, out var action))
			{
				return action;
			}
			LogService.Error(this, $"–¢“o˜^‚ÌAction‚Å‚·->{actionID}");
			return null;
		}

		private cActionContainer(CharacterBase character)
		{
			_OwnerCharacter = character;
		}

		private CharacterBase _OwnerCharacter = null;
		private Dictionary<int, cActionBase> _ActionHolder = new Dictionary<int, cActionBase>();
	}
}