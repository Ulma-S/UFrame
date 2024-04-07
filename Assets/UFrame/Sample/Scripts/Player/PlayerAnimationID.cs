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
		}
	}
}