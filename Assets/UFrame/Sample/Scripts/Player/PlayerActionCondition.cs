using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class PlayerActionCondition
	{
		public static bool Judge(ACTION_ID actionID, PlayerCharacter chara)
		{
			var currentActionID = chara.ActionController.CurrentActionID;
			if (currentActionID == actionID)
			{
				return false;
			}

			switch (actionID.Index)
			{
				case PlayerAction.ID.Idle:
					return true;
				case PlayerAction.ID.Move:
					if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_LEFT))
					{
						return true;
					}
					if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_RIGHT))
					{
						return true;
					}
					break;
				case PlayerAction.ID.Jump:
					if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.JUMP))
					{
						return true;
					}
					break;
			}
			return false;
		}
	}
}