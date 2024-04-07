using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cCharacterContext
	{
		public void OnSafeFlag(CharacterDef.SAFE_FLAG flagType)
		{
			_SafeFlag.On(flagType);
		}

		public bool IsOnSafeFlag(CharacterDef.SAFE_FLAG flagType)
		{
			return _SafeFlag.IsOn(flagType);
		}

		public void UpdateFlags()
		{
			_SafeFlag.UpdateFlags();
		}

		private cSafeFlag<CharacterDef.SAFE_FLAG> _SafeFlag = new cSafeFlag<CharacterDef.SAFE_FLAG>();
	}
}