using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class cPlayerController : cCharacterController
	{
		protected override void OnUpdate()
		{
			UpdateMoveState();

			var currentActionID = Character.ActionController.CurrentActionID;
			var requestActionID = ACTION_ID.Invalid;
			switch (currentActionID.Index)
			{
				case PlayerAction.ID.Idle:
					if (PlayerActionCondition.Judge(PlayerAction.SetID.Jump, PlayerChara))
					{
						requestActionID = PlayerAction.SetID.Jump;
					}
					else if (PlayerActionCondition.Judge(PlayerAction.SetID.Move, PlayerChara))
					{
						requestActionID = PlayerAction.SetID.Move;
					}
					break;
				case PlayerAction.ID.Move:
					if (PlayerActionCondition.Judge(PlayerAction.SetID.Jump, PlayerChara))
					{
						requestActionID = PlayerAction.SetID.Jump;
					}
					break;
				case PlayerAction.ID.Jump:
					if (PlayerChara.AnimationSequence.IsCancellable)
					{
						if (PlayerActionCondition.Judge(PlayerAction.SetID.Move, PlayerChara))
						{
							requestActionID = PlayerAction.SetID.Move;
						}
					}
					break;
			}
			if (PlayerChara.ActionController.IsActionEnd)
			{
				if (requestActionID == ACTION_ID.Invalid)
				{
					requestActionID = PlayerAction.SetID.Idle;
				}
			}
			if (requestActionID != ACTION_ID.Invalid)
			{
				PlayerChara.RequestSetAction(requestActionID);
			}
		}

		private void UpdateMoveState()
		{
			var moveInfo = PlayerChara.PlayerContext.MoveInfo;
			moveInfo.PrevMoveState = moveInfo.MoveState;
			if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_RIGHT))
			{
				moveInfo.MoveState = PlayerDef.MOVE_STATE.MOVE_RIGHT;
			}
			else if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_LEFT))
			{
				moveInfo.MoveState = PlayerDef.MOVE_STATE.MOVE_LEFT;
			}
			else
			{
				moveInfo.MoveState = PlayerDef.MOVE_STATE.IDLE;
			}
		}

		private PlayerCharacter PlayerChara => Character as PlayerCharacter;
	}
}