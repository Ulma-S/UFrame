using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	[DefaultExecutionOrder((int)EXECUTION_ORDER.PLAYER_CONTROLLER)]
	public class PlayerController : CharacterControllerBase
	{
		protected override void OnStart()
		{
			_PlayerChara = Character as PlayerCharacter;
		}

		protected override void OnUpdate()
		{
			UpdateMoveState();

			if (!_PlayerChara.PlayerContext.CheckSafeFlag(PlayerDef.SAFE_FLAG.DAMAGE_FLOW))
			{// ダメージ処理中は専用処理
				UpdateAction();
			}
		}

		private void UpdateMoveState()
		{
			var moveInfo = _PlayerChara.PlayerContext.MoveInfo;
			moveInfo.PrevMoveState = moveInfo.MoveState;
			if (_PlayerChara.PlayerContext.CheckSafeFlag(PlayerDef.SAFE_FLAG.FORCE_MOVE_STATE))
			{// 外部から上書きする状態なら更新しない
				return;
			}
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

		private void UpdateAction()
		{
			if (_PlayerChara.PlayerContext.CheckSafeFlag(PlayerDef.SAFE_FLAG.SYSTEM_JACK))
			{
				return;
			}
			var currentActionID = Character.ActionController.CurrentActionID;
			var requestActionID = ACTION_ID.Invalid;
			switch (currentActionID.Index)
			{
				case PlayerAction.Scroll.ID.Idle:
					if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Jump, _PlayerChara))
					{
						requestActionID = PlayerAction.Scroll.SetID.Jump;
					}
					else if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Move, _PlayerChara))
					{
						requestActionID = PlayerAction.Scroll.SetID.Move;
					}
					break;
				case PlayerAction.Scroll.ID.Move:
					if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Jump, _PlayerChara))
					{
						requestActionID = PlayerAction.Scroll.SetID.Jump;
					}
					break;
				case PlayerAction.Scroll.ID.Jump:
					if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.AirJump, _PlayerChara))
					{
						requestActionID = PlayerAction.Scroll.SetID.AirJump;
					}
					else if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.FastFall, _PlayerChara))
					{
						requestActionID = PlayerAction.Scroll.SetID.FastFall;
					}
					else if (_PlayerChara.AnimationSequence.IsCancellable)
					{
						if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Jump, _PlayerChara, true))
						{
							requestActionID = PlayerAction.Scroll.SetID.Jump;
						}
						else if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Move, _PlayerChara))
						{
							requestActionID = PlayerAction.Scroll.SetID.Move;
						}
					}
					break;
				case PlayerAction.Scroll.ID.AirJump:
					if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.AirJump, _PlayerChara, true))
					{
						requestActionID = PlayerAction.Scroll.SetID.AirJump;
					}
					else if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.FastFall, _PlayerChara))
					{
						requestActionID = PlayerAction.Scroll.SetID.FastFall;
					}
					else if (_PlayerChara.AnimationSequence.IsCancellable)
					{
						if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Jump, _PlayerChara))
						{
							requestActionID = PlayerAction.Scroll.SetID.Jump;
						}
						else if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Move, _PlayerChara))
						{
							requestActionID = PlayerAction.Scroll.SetID.Move;
						}
					}
					break;
				case PlayerAction.Scroll.ID.FastFall:
					if (_PlayerChara.AnimationSequence.IsCancellable)
					{
						if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Jump, _PlayerChara))
						{
							requestActionID = PlayerAction.Scroll.SetID.Jump;
						}
						else if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Move, _PlayerChara))
						{
							requestActionID = PlayerAction.Scroll.SetID.Move;
						}
					}
					break;
			}
			if (_PlayerChara.ActionController.IsActionEnd)
			{
				if (requestActionID == ACTION_ID.Invalid)
				{
					requestActionID = PlayerAction.Scroll.SetID.Idle;
				}
			}
			if (requestActionID != ACTION_ID.Invalid)
			{
				_PlayerChara.RequestSetAction(requestActionID);
			}
		}

		private PlayerCharacter _PlayerChara = null;
	}
}