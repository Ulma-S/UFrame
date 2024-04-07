using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public static partial class PlayerAction
	{
		public class cPlayerActionBase : cActionBase
		{
			protected override void OnSetup()
			{
				PlayerChara = Chara as PlayerCharacter;
				AnimCtrl = Chara.AnimationController;
				AnimSeq = PlayerChara.AnimationSequence;
				AnimContainer = PlayerChara.AnimationContainer;
				Rigidbody = Chara.GetComponent<Rigidbody>();
			}

			protected override bool OnUpdate()
			{
				UpdateFront();
				return base.OnUpdate();
			}

			protected void SetAnimation(int stateHash)
			{
				PlayerChara.AnimationController.SetAnimation(stateHash);
			}

			private void UpdateFront()
			{
				var moveState = PlayerChara.PlayerContext.MoveInfo.MoveState;
				switch (moveState)
				{
					case PlayerDef.MOVE_STATE.MOVE_RIGHT:
						{
							var eulerRot = new Vector3(0f, 90f, 0f);
							PlayerChara.transform.rotation = Quaternion.Euler(eulerRot);
						}
						break;
					case PlayerDef.MOVE_STATE.MOVE_LEFT:
						{
							var eulerRot = new Vector3(0f, -90f, 0f);
							PlayerChara.transform.rotation = Quaternion.Euler(eulerRot);
						}
						break;
				}
			}

			protected PlayerCharacter PlayerChara
			{
				get;
				private set;
			} = null;

			protected cAnimationController AnimCtrl
			{
				get;
				private set;
			} = null;

			protected PlayerAnimationSequence AnimSeq
			{
				get;
				private set;
			} = null;

			protected PlayerAnimationContainer AnimContainer
			{
				get;
				private set;
			} = null;

			protected Rigidbody Rigidbody
			{
				get;
				private set;
			} = null;
		}

		public class cPlayerSimpleActionBase : cPlayerActionBase
		{
			protected void SetAnimationID(PlayerAnimation.ID id)
			{
				if (AnimContainer.TryGetStateHash(id, out var stateHash))
				{
					_SetAnimationID = stateHash;
					_IsAnimationInitialized = true;
				}
				else
				{
					LogService.Error(this, $"{id}ÇÃÉAÉjÉÅÅ[ÉVÉáÉìÇÕñ¢ìoò^Ç≈Ç∑");
				}
			}

			protected override void OnEnter()
			{
				base.OnEnter();
				if (_IsAnimationInitialized)
				{
					SetAnimation(_SetAnimationID);
				}
			}

			private int _SetAnimationID = 0;
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
				}
				Rigidbody.velocity = velocity;
			}
		}

		public class cMoveBase : cPlayerActionBase
		{
			protected bool UpdateMove()
			{
				var moveState = PlayerChara.PlayerContext.MoveInfo.MoveState;
				switch (moveState)
				{
					case PlayerDef.MOVE_STATE.MOVE_LEFT:
						{
							var velocity = Rigidbody.velocity;
							velocity.x = -3f;
							Rigidbody.velocity = velocity;
						}
						return false;
					case PlayerDef.MOVE_STATE.MOVE_RIGHT:
						{
							var velocity = Rigidbody.velocity;
							velocity.x = 3f;
							Rigidbody.velocity = velocity;
						}
						return false;
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
		}

		public class cJump : cMoveBase
		{
			private enum STATE
			{
				START,
				LOOP,
				END,
			}

			protected override void OnSetup()
			{
				base.OnSetup();
				if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.JUMP_START, out var startStateHash))
				{
					_JumpStartStateHash = startStateHash;
				}
				if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.JUMP_LOOP, out var loopStateHash))
				{
					_JumpLoopStateHash = loopStateHash;
				}
				if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.JUMP_END, out var endStateHash))
				{
					_JumpEndStateHash = endStateHash;
				}
			}

			protected override void OnEnter()
			{
				base.OnEnter();
				_State = STATE.START;
				AnimCtrl.SetAnimation(_JumpStartStateHash);
				var velocity = Rigidbody.velocity;
				velocity.y = 8f;
				Rigidbody.velocity = velocity;
			}

			protected override bool OnUpdate()
			{
				base.OnUpdate();
				switch (_State)
				{
					case STATE.START:
						if (AnimCtrl.IsAnimationEnd(_JumpStartStateHash) || Rigidbody.velocity.y < Mathf.Epsilon)
						{
							_State = STATE.LOOP;
							AnimCtrl.SetAnimation(_JumpLoopStateHash);
						}
						break;
					case STATE.LOOP:
						if (PlayerChara.CheckGround())
						{
							_State = STATE.END;
							AnimCtrl.SetAnimation(_JumpEndStateHash);
						}
						break;
					case STATE.END:
						if (AnimCtrl.IsAnimationEnd(_JumpEndStateHash))
						{
							return true;
						}
						break;
				}
				if (_State != STATE.END)
				{
					UpdateMove();
				}
				return false;
			}

			private int _JumpStartStateHash = 0;
			private int _JumpLoopStateHash = 0;
			private int _JumpEndStateHash = 0;

			private STATE _State = STATE.START;
		}
	}
}