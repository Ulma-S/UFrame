using System;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
	public static partial class GameFlowDef
	{
		public enum GAME_CLEAR_FLOW
		{
			INVALID = -1,
			ADJUST_PLAYER,
			WARP_PLAYER,
			ZOOM_CAMERA,
			VICTORY_PERFORMANCE,
			END,
		}
	}
}