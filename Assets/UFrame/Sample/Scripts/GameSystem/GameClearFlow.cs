using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public static partial class GameClearFlow
	{
		public class cBase : cGameFlowBase
		{
			protected override bool OnUpdate()
			{
				var playerChara = GlobalService.Pl.PlayerCharacter;
				playerChara.PlayerContext.OnSafeFlag(PlayerDef.SAFE_FLAG.SYSTEM_JACK);
				return base.OnUpdate();
			}
		}

		public class cStopPlayer : cBase
		{
			protected override void OnEnter()
			{
				base.OnEnter();
				var playerChara = GlobalService.Pl.PlayerCharacter;
				playerChara.RequestSetAction(PlayerAction.SetID.Idle);
				playerChara.SetSpeed(Vector3.zero);
			}
		}

		public class cAdjustPlayer : cBase
		{
			protected override void OnEnter()
			{
				base.OnEnter();
				var currentGoal = GlobalService.GameFlow.CurrentGoal;
				var playerChara = GlobalService.Pl.PlayerCharacter;
				if (playerChara.Pos.x > currentGoal.Pos.x)
				{
					_MoveState = PlayerDef.MOVE_STATE.MOVE_LEFT;
				}
				else if (playerChara.Pos.x < currentGoal.Pos.x)
				{
					_MoveState = PlayerDef.MOVE_STATE.MOVE_RIGHT;
				}
				playerChara.RequestSetAction(PlayerAction.SetID.Move);
			}

			protected override bool OnUpdate()
			{
				base.OnUpdate();
				var playerChara = GlobalService.Pl.PlayerCharacter;
				playerChara.PlayerContext.OnSafeFlag(PlayerDef.SAFE_FLAG.FORCE_MOVE_STATE);
				return false;
			}

			private PlayerDef.MOVE_STATE _MoveState = PlayerDef.MOVE_STATE.IDLE;
		}

		public class cWarpPlayer : cBase
		{
			protected override void OnEnter()
			{
				base.OnEnter();
				var currentGoal = GlobalService.GameFlow.CurrentGoal;
				var playerChara = GlobalService.Pl.PlayerCharacter;
				var warpPos = currentGoal.Pos;
				if (playerChara.TryGetGroundPos(warpPos, out var groundPos))
				{
					warpPos = groundPos;
				}
				playerChara.Warp(warpPos);
				playerChara.Rot = Quaternion.Euler(0f, 180f, 0f);
				playerChara.RequestSetAction(PlayerAction.SetID.Idle);
			}
		}

		public class cZoomCamera : cBase
		{
			protected override void OnEnter()
			{
				base.OnEnter();
				GlobalService.Camera.ChangeActiveCamera(CameraDef.ID.GOAL);
			}

			protected override bool OnUpdate()
			{
				base.OnUpdate();
				if (!GlobalService.Camera.IsCameraTransition)
				{// ƒJƒƒ‰‘JˆÚ‚ªI—¹
					return true;
				}
				return false;
			}
		}

		public class cVictoryPerformance : cBase
		{
			protected override void OnEnter()
			{
				base.OnEnter();
				var playerChara = GlobalService.Pl.PlayerCharacter;
				playerChara.RequestSetAction(PlayerAction.SetID.Victory);
			}

			protected override bool OnUpdate()
			{
				base.OnUpdate();
				var playerChara = GlobalService.Pl.PlayerCharacter;
				if (playerChara.ActionController.IsActionEnd)
				{
					return true;
				}
				return false;
			}
		}

		public class cEnd : cBase
		{
			protected override void OnEnter()
			{
				base.OnEnter();
				GlobalService.Scene.LoadScenePackWithFade(SceneDef.PACK_ID.SAMPLE_RESULT);
			}

			protected override bool OnUpdate()
			{
				base.OnUpdate();
				return false;
			}
		}
	}
}