using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public static partial class CharacterDef
	{
		public enum STAND_STATE
		{
			GROUND,
			FLY,
		}

		public enum SAFE_FLAG
		{
			/// <summary>
			/// –³“G
			/// </summary>
			NO_DAMAGE,

			/// <summary>
			/// ˆÚ“®‹ÖŽ~
			/// </summary>
			DISABLE_MOVE,
		}
	}
}