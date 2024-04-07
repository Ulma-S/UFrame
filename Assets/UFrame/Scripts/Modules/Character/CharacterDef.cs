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
			/// ���G
			/// </summary>
			NO_DAMAGE,

			/// <summary>
			/// �ړ��֎~
			/// </summary>
			DISABLE_MOVE,
		}
	}
}