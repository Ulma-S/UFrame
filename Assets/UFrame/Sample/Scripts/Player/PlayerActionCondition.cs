using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class PlayerActionCondition
	{
		public static bool Judge(ACTION_ID actionID, PlayerCharacter chara, bool canSameAction = false)
		{
			var currentActionID = chara.ActionController.CurrentActionID;
			if (!canSameAction)
			{
				if (currentActionID == actionID)
				{
					return false;
				}
			}

			switch (actionID.Index)
			{
				case PlayerAction.Scroll.ID.Idle:
					return true;
				case PlayerAction.Scroll.ID.Move:
					if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_LEFT))
					{
						return true;
					}
					if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_RIGHT))
					{
						return true;
					}
					break;
				case PlayerAction.Scroll.ID.Jump:
					if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.JUMP))
					{
						return true;
					}
					break;
				case PlayerAction.Scroll.ID.AirJump:
					if (chara.PlayerContext.MoveInfo.AirJumpCount > chara.Param.Common.MoveInfo.MaxAirJumpCount)
					{
						return false;
					}
					if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.JUMP))
					{
						return true;
					}
					break;
				case PlayerAction.Scroll.ID.FastFall:
					if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.FALL))
					{
						return true;
					}
					break;
			}
			return false;
		}
	}
}