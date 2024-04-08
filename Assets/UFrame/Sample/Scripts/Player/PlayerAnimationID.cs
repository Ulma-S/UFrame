using System;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
	public static partial class PlayerAnimation
	{
		public enum ID
		{
			INVALID,
			IDLE,
			WALK,
			RUN,
			MOVE_BLEND,
			JUMP_START,
			JUMP_LOOP,
			JUMP_END,
			DAMAGE_SMALL,
			DAMAGE_SMASH,
			DAMAGE_SMASH_UP,
			KNEEL_DOWN,
			KNEEL_DOWN_UP,
			VICTORY,
		}
	}
}