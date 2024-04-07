using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class AnimationSequence : MonoBehaviour
	{
		public bool GetBool(int hash)
		{
			return GetFloat(hash) >= 1f;
		}

		public float GetFloat(int hash)
		{
			var animCtrl = _Chara.AnimationController;
			return animCtrl.GetFloat(hash);
		}

		private void Start()
		{
			_Chara = GetComponent<CharacterBase>();
		}

		private CharacterBase _Chara = null;
	}
}