using System;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
	[CreateAssetMenu(menuName = "UFrame/Sample/PlayerAnimationContainer")]
	public class PlayerAnimationContainer : ScriptableObject
	{
		[Serializable]
		public class cElement
		{
			public PlayerAnimation.ID ID => _ID;

			public AnimationClip Clip => _Clip;

			public string StateName => _StateName;

			[SerializeField]
			private PlayerAnimation.ID _ID = PlayerAnimation.ID.INVALID;

			[SerializeField]
			private AnimationClip _Clip = null;

			[SerializeField]
			private string _StateName = string.Empty;
		}

		public AnimationClip FindClip(PlayerAnimation.ID id)
		{
			var element = Array.Find(_Elements, data => data.ID == id);
			if (element != null)
			{
				return element.Clip;
			}
			return null;
		}


		public bool TryGetStateHash(PlayerAnimation.ID id, out int stateHash)
		{
			stateHash = 0;
			var element = Array.Find(_Elements, data => data.ID == id);
			if (element != null)
			{
				stateHash = Animator.StringToHash(element.StateName);
				return true;
			}
			return false;
			
		}

		[SerializeField]
		private cElement[] _Elements = null;
	}
}