using System;
using System.Collections;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public static partial class PlayerAction
	{
		public class cPlayerActionBase : cActionBase
		{
			public bool CheckActionAttribute(PlayerDef.ACTION_ATTR attr)
			{
				return (_ActionAttribute & (uint)attr) != 0;
			}

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
				UpdateStandState();
				return base.OnUpdate();
			}

			protected void SetAnimation(int stateHash)
			{
				PlayerChara.AnimationController.SetAnimation(stateHash);
			}

			protected void SetActionAttribute(PlayerDef.ACTION_ATTR[] actionAttributes)
			{
				foreach (var attribute in actionAttributes)
				{
					var attrNum = (uint)attribute;
					_ActionAttribute |= attrNum;
				}
			}

			private void UpdateStandState()
			{
				if (Chara.IsOnGround())
				{
					var velocity = Rigidbody.velocity;
					if (velocity.y <= 0f)
					{
						PlayerChara.PlayerContext.MoveInfo.AirJumpCount = 0;
					}
				}
				else
				{
					Chara.StandState = CharacterDef.STAND_STATE.FLY;
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

			private uint _ActionAttribute = 0;
		}

		#region スクロール
		public static partial class Scroll
		{
			public class cPlayerScrollActionBase : cPlayerActionBase
			{
				protected override bool OnUpdate()
				{
					if (UpdateFrontEnabled && !PlayerChara.PlayerContext.CheckSafeFlag(PlayerDef.SAFE_FLAG.SYSTEM_JACK))
					{
						UpdateFront();
					}
					return base.OnUpdate();
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

				protected virtual bool UpdateFrontEnabled
				{
					get;
					set;
				} = true;
			}

			public class cPlayerSimpleScrollActionBase : cPlayerScrollActionBase
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

			public class cIdle : cPlayerSimpleScrollActionBase
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

				protected override bool OnUpdate()
				{
					base.OnUpdate();
					return false;
				}
#if UNITY_EDITOR
				public override string ActionName => "待機";
#endif
			}

			#region 移動
			public class cMoveBase : cPlayerScrollActionBase
			{
				protected bool UpdateMove()
				{
					var moveState = PlayerChara.PlayerContext.MoveInfo.MoveState;
					var moveInfo = PlayerChara.Param.Common.MoveInfo;
					var moveSpeedAbs = moveInfo.MoveSpeed;
					if (Chara.StandState == CharacterDef.STAND_STATE.FLY)
					{
						moveSpeedAbs *= moveInfo.MoveSpeedScaleAir;
					}
					switch (moveState)
					{
						case PlayerDef.MOVE_STATE.MOVE_LEFT:
							{
								var velocity = Rigidbody.velocity;
								velocity.x = -moveSpeedAbs;
								Rigidbody.velocity = velocity;
							}
							return false;
						case PlayerDef.MOVE_STATE.MOVE_RIGHT:
							{
								var velocity = Rigidbody.velocity;
								velocity.x = moveSpeedAbs;
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

#if UNITY_EDITOR
				public override string ActionName => "移動";
#endif
			}

			public class cJumpBase : cMoveBase
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
					if (Chara.StandState == CharacterDef.STAND_STATE.GROUND)
					{
						velocity.y = PlayerChara.Param.Common.MoveInfo.JumpForce;
					}
					else if (Chara.StandState == CharacterDef.STAND_STATE.FLY)
					{
						var moveInfo = PlayerChara.Param.Common.MoveInfo;
						velocity.y = moveInfo.JumpForce * moveInfo.JumpForceScaleAir;
					}
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
							if (PlayerChara.CheckGroundDistance(PlayerChara.Param.Common.MoveInfo.JumpEndCheckRayLength))
							{
								_State = STATE.END;
								AnimCtrl.SetAnimation(_JumpEndStateHash);
							}
							break;
						case STATE.END:
							if (PlayerChara.IsOnGround())
							{
								var velocity = Rigidbody.velocity;
								velocity.x = 0f;
								Rigidbody.velocity = velocity;
							}
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

			public class cJump : cJumpBase
			{
#if UNITY_EDITOR
				public override string ActionName => "ジャンプ";
#endif
			}

			public class cAirJump : cJumpBase
			{
				protected override void OnEnter()
				{
					base.OnEnter();
					PlayerChara.PlayerContext.MoveInfo.AirJumpCount++;
				}

#if UNITY_EDITOR
				public override string ActionName => "空中ジャンプ";
#endif
			}

			public class cFastFall : cPlayerScrollActionBase
			{
				private enum STATE
				{
					FALL,
					END,
				}

				protected override void OnSetup()
				{
					base.OnSetup();
					if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.JUMP_LOOP, out var fallStateHash))
					{
						_FallStateHash = fallStateHash;
					}
					if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.JUMP_END, out var endStateHash))
					{
						_EndStateHash = endStateHash;
					}
				}

				protected override void OnEnter()
				{
					base.OnEnter();
					var velocity = Rigidbody.velocity;
					velocity.y = -PlayerChara.Param.Common.MoveInfo.FastFallSpeed;
					Rigidbody.velocity = velocity;
					_State = STATE.FALL;
					SetAnimation(_FallStateHash);
				}

				protected override bool OnUpdate()
				{
					base.OnUpdate();
					switch (_State)
					{
						case STATE.FALL:
							if (PlayerChara.CheckGroundDistance(PlayerChara.Param.Common.MoveInfo.JumpEndCheckRayLength))
							{
								_State = STATE.END;
								SetAnimation(_EndStateHash);
							}
							break;
						case STATE.END:
							if (PlayerChara.IsOnGround())
							{
								var velocity = Rigidbody.velocity;
								velocity.x = 0f;
								Rigidbody.velocity = velocity;
							}
							if (AnimCtrl.IsAnimationEnd(_EndStateHash))
							{
								return true;
							}
							break;
					}
					return false;
				}

				private int _FallStateHash = 0;
				private int _EndStateHash = 0;

				private STATE _State = STATE.FALL;

#if UNITY_EDITOR
				public override string ActionName => "高速落下";
#endif
			}
			#endregion

			#region ダメージ

			public class cDamageBase : cPlayerScrollActionBase
			{
				protected override void OnSetup()
				{
					base.OnSetup();
					SetActionAttribute(new PlayerDef.ACTION_ATTR[] { PlayerDef.ACTION_ATTR.DAMAGE });
				}

				protected override void OnEnter()
				{
					base.OnEnter();
					var velocity = Rigidbody.velocity;
					velocity.x = 0f;
					Rigidbody.velocity = velocity;
				}

				protected override bool UpdateFrontEnabled => false;
			}

			public class cDamageSmall : cDamageBase
			{
				protected override void OnSetup()
				{
					base.OnSetup();
					if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.DAMAGE_SMALL, out var stateHash))
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
					if (AnimCtrl.IsAnimationEnd(_StateHash))
					{
						return true;
					}
					return false;
				}

				private int _StateHash = 0;

#if UNITY_EDITOR
				public override string ActionName => "小ダメージ";
#endif
			}

			public class cDamageSmash : cDamageBase
			{
				private enum STATE
				{
					DOWN,
					UP,
				}

				protected override void OnSetup()
				{
					base.OnSetup();
					if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.DAMAGE_SMASH, out var damageStateHash))
					{
						_DamageSmashStateHash = damageStateHash;
					}
					if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.DAMAGE_SMASH_UP, out var getUpStateHash))
					{
						_GetUpStateStateHash = getUpStateHash;
					}
				}

				protected override void OnEnter()
				{
					base.OnEnter();
					SetAnimation(_DamageSmashStateHash);
					_State = STATE.DOWN;
				}

				protected override bool OnUpdate()
				{
					base.OnUpdate();

					switch (_State)
					{
						case STATE.DOWN:
							if (AnimCtrl.IsAnimationEnd(_DamageSmashStateHash))
							{
								SetAnimation(_GetUpStateStateHash);
								_State = STATE.UP;
							}
							break;
						case STATE.UP:
							if (AnimCtrl.IsAnimationEnd(_GetUpStateStateHash) && Chara.IsOnGround())
							{
								return true;
							}
							break;
					}
					return false;
				}

				private int _DamageSmashStateHash = 0;
				private int _GetUpStateStateHash = 0;

				private STATE _State = STATE.DOWN;

#if UNITY_EDITOR
				public override string ActionName => "大ダメージ";
#endif
			}

			public class cKneelDown : cDamageBase
			{
				private enum STATE
				{
					DOWN,
					UP,
				}

				protected override void OnSetup()
				{
					base.OnSetup();
					if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.KNEEL_DOWN, out var kneelDownStateHash))
					{
						_KneelDownStateHash = kneelDownStateHash;
					}
					if (AnimContainer.TryGetStateHash(PlayerAnimation.ID.KNEEL_DOWN_UP, out var kneelDownUpStateHash))
					{
						_KneelDownUpStateHash = kneelDownUpStateHash;
					}
				}

				protected override void OnEnter()
				{
					base.OnEnter();
					_State = STATE.DOWN;
				}

				protected override bool OnUpdate()
				{
					base.OnUpdate();
					switch (_State)
					{
						case STATE.DOWN:
							if (AnimCtrl.IsAnimationEnd(_KneelDownStateHash))
							{
								SetAnimation(_KneelDownUpStateHash);
								_State = STATE.UP;
							}
							break;
						case STATE.UP:
							if (AnimCtrl.IsAnimationEnd(_KneelDownUpStateHash) && Chara.IsOnGround())
							{
								return true;
							}
							break;
					}
					return false;
				}

				private int _KneelDownStateHash = 0;
				private int _KneelDownUpStateHash = 0;

				private STATE _State = STATE.DOWN;

#if UNITY_EDITOR
				public override string ActionName => "膝崩れ";
#endif
			}
			#endregion

			public class cVictory : cPlayerSimpleScrollActionBase
			{
				protected override void OnSetup()
				{
					base.OnSetup();
					SetAnimationID(PlayerAnimation.ID.VICTORY);
				}

#if UNITY_EDITOR
				public override string ActionName => "勝利";
#endif
			}
		}
		#endregion
	}
}