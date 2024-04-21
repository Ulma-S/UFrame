using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;
using static app.PlayerAction.Scroll;

namespace app
{
	public static partial class PlayerAction
	{
		#region TPS
		public static partial class Tps
		{
			public class cPlayerSimpleActionBase : cPlayerActionBase
			{
				protected void SetAnimationID(PlayerAnimation.ID id)
				{
					if (AnimContainer.TryGetStateHash(id, out var stateHash))
					{
						_SetAnimationHash = stateHash;
						_IsAnimationInitialized = true;
					}
					else
					{
						LogService.Error(this, $"{id}のアニメーションは未登録です");
					}
				}

				protected override void OnEnter()
				{
					base.OnEnter();
					if (_IsAnimationInitialized)
					{
						SetAnimation(_SetAnimationHash);
					}
				}

				protected override bool OnUpdate()
				{
					base.OnUpdate();
					if (Chara.AnimationController.IsAnimationEnd(_SetAnimationHash))
					{
						return true;
					}
					return false;
				}

				private int _SetAnimationHash = 0;
				private bool _IsAnimationInitialized = false;
			}

			public class cIdle : cPlayerSimpleActionBase
			{
				protected override void OnSetup()
				{
					base.OnSetup();
					SetAnimationID(PlayerAnimation.ID.IDLE);
				}

				protected override void OnEnter()
				{
					base.OnEnter();
					var velocity = Rigidbody.velocity;
					if (Chara.StandState == CharacterDef.STAND_STATE.FLY)
					{

					}
					else
					{
						velocity.x = 0f;
						velocity.z = 0f;
					}
					Rigidbody.velocity = velocity;
				}

				protected override bool OnUpdate()
				{
					base.OnUpdate();
					return false;
				}
#if UNITY_EDITOR
				public override string ActionName => "待機";
#endif
			}

			public class cMoveBase : cPlayerActionBase
			{
				protected bool UpdateMove()
				{
					var currentCamera = GlobalService.Camera.CurrentVirtualCamera;
					var cameraForward = currentCamera.Transform.forward;
					var cameraForwardFixed = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
					var moveInfo = PlayerChara.Param.Common.MoveInfo;
					Rigidbody.velocity = cameraForwardFixed * moveInfo.MoveSpeed;
					if (Chara.StandState == CharacterDef.STAND_STATE.FLY)
					{
						Rigidbody.velocity *= moveInfo.MoveSpeedScaleAir;
					}
					return true;
				}
			}

			public class cMove : cMoveBase
			{
				protected override void OnSetup()
				{
					base.OnSetup();
					if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.MOVE_BLEND, out var stateHash))
					{
						_StateHash = stateHash;
					}
				}

				protected override void OnEnter()
				{
					base.OnEnter();
					SetAnimation(_StateHash);
				}

				protected override bool OnUpdate()
				{
					base.OnUpdate();
					AnimCtrl.SetFloat(MoveBlendHash, 1f);
					return UpdateMove();
				}

				protected override void OnExit()
				{
					base.OnExit();
					AnimCtrl.SetFloat(MoveBlendHash, 0f);
				}

				protected readonly int MoveBlendHash = Animator.StringToHash("MoveBlendValue");
				private int _StateHash = 0;

#if UNITY_EDITOR
				public override string ActionName => "移動";
#endif
			}
		}
		#endregion
	}
}