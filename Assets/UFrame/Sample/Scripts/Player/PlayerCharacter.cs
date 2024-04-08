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

		protected override void OnStart()
		{
			AnimationSequence = GetComponent<PlayerAnimationSequence>();
			BuildAction();
		}

		protected override void OnUpdate()
		{
			ActionController.Update();
			PlayerContext.UpdateSafeFlag();

			LogService.Info(this, $"{ActionController.CurrentAction.ActionName}");
		}

		protected override void OnLateUpdate()
		{
			StandState.UpdateValue();
		}

		private void BuildAction()
		{
			ActionController.Register<PlayerAction.cIdle>(PlayerAction.SetID.Idle);
			ActionController.Register<PlayerAction.cMove>(PlayerAction.SetID.Move);
			ActionController.Register<PlayerAction.cJump>(PlayerAction.SetID.Jump);
			ActionController.Register<PlayerAction.cAirJump>(PlayerAction.SetID.AirJump);
			ActionController.Register<PlayerAction.cDamageSmall>(PlayerAction.SetID.DamageSmall);
			ActionController.Register<PlayerAction.cDamageSmash>(PlayerAction.SetID.DamageSmash);
			ActionController.Register<PlayerAction.cKneelDown>(PlayerAction.SetID.KneelDown);
			ActionController.Register<PlayerAction.cVictory>(PlayerAction.SetID.Victory);
			ActionController.Setup(PlayerAction.SetID.Idle);
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
	}
}