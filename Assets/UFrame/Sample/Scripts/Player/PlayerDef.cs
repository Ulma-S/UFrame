using System;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
	public static partial class PlayerDef
	{
		public enum MOVE_STATE
		{
			IDLE,
			MOVE_RIGHT,
			MOVE_LEFT,
		}

		public enum SAFE_FLAG
		{
			INVINCIBLE,
			DAMAGE_FLOW,
		}

		[Flags]
		public enum ACTION_ATTR : uint
		{
			DAMAGE = 1U << 0,
			MAX,
		}
	}
}