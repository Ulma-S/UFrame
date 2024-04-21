using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	[DefaultExecutionOrder((int)EXECUTION_ORDER.PLAYER_CHARACTER)]
	public class PlayerCharacter : CharacterBase
	{
		public void RequestSetAction(ACTION_ID id)
		{
			ActionController.RequestSetAction(id);
		}

		public void SetSpeed(Vector3 velocity)
		{
			_Rigidbody.velocity = velocity;
		}

		public void ForceSetMoveState(PlayerDef.MOVE_STATE moveState)
		{
			PlayerContext.MoveInfo.MoveState = moveState;
		}

		protected override void OnStart()
		{
			AnimationSequence = GetComponent<PlayerAnimationSequence>();
			BuildAction();
			_Rigidbody = GetComponent<Rigidbody>();
		}

		protected override void OnUpdate()
		{
			ActionController.Update();
			PlayerContext.UpdateSafeFlag();

			LogService.Info(this, $"{ActionController.CurrentAction.ActionName}");
		}

		protected override void OnLateUpdate()
		{
		}

		private void BuildAction()
		{
			ActionController.Register<PlayerAction.Scroll.cIdle>(PlayerAction.Scroll.SetID.Idle);
			ActionController.Register<PlayerAction.Scroll.cMove>(PlayerAction.Scroll.SetID.Move);
			ActionController.Register<PlayerAction.Scroll.cJump>(PlayerAction.Scroll.SetID.Jump);
			ActionController.Register<PlayerAction.Scroll.cAirJump>(PlayerAction.Scroll.SetID.AirJump);
			ActionController.Register<PlayerAction.Scroll.cFastFall>(PlayerAction.Scroll.SetID.FastFall);
			ActionController.Register<PlayerAction.Scroll.cDamageSmall>(PlayerAction.Scroll.SetID.DamageSmall);
			ActionController.Register<PlayerAction.Scroll.cDamageSmash>(PlayerAction.Scroll.SetID.DamageSmash);
			ActionController.Register<PlayerAction.Scroll.cKneelDown>(PlayerAction.Scroll.SetID.KneelDown);
			ActionController.Register<PlayerAction.Scroll.cVictory>(PlayerAction.Scroll.SetID.Victory);

			ActionController.Register<PlayerAction.Tps.cIdle>(PlayerAction.Tps.SetID.Idle);
			ActionController.Register<PlayerAction.Tps.cMove>(PlayerAction.Tps.SetID.Move);
			
			ActionController.Setup(PlayerAction.Scroll.SetID.Idle);
		}

		public cPlayerContext PlayerContext
		{
			get;
			private set;
		} = new cPlayerContext();

		public override Vector3 GroundCheckOriginOffset => _Param.Common.MoveInfo.GroundCheckOriginOffset;

		public override float GroundCheckRayLength => _Param.Common.MoveInfo.GroundCheckRayLength;

		public override float GroundCheckSphereRadius => _Param.Common.MoveInfo.GroundCheckSphereRadius;

		public PlayerAnimationSequence AnimationSequence
		{
			get;
			private set;
		} = null;

		public PlayerAnimationContainer AnimationContainer => _AnimationContainer;

		[SerializeField]
		private PlayerAnimationContainer _AnimationContainer = null;

		public PlayerParamHolder Param => _Param;

		[SerializeField]
		private PlayerParamHolder _Param = null;

		private Rigidbody _Rigidbody = null;
	}
}