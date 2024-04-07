using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class PlayerCharacter : CharacterBase
	{
		public void RequestSetAction(ACTION_ID id)
		{
			ActionController.RequestSetAction(id);
		}

		protected override void OnStart()
		{
			AnimationSequence = GetComponent<PlayerAnimationSequence>();
			CharacterController = cCharacterController.Create<cPlayerController>(this);
			InitializeAction();
		}

		protected override void OnUpdate()
		{
			CharacterController.Update();
			ActionController.Update();
		}

		private void InitializeAction()
		{
			ActionController.Register<PlayerAction.cIdle>(PlayerAction.SetID.Idle);
			ActionController.Register<PlayerAction.cMove>(PlayerAction.SetID.Move);
			ActionController.Register<PlayerAction.cJump>(PlayerAction.SetID.Jump);
			ActionController.Setup(PlayerAction.SetID.Idle);
		}

		public cPlayerContext PlayerContext
		{
			get;
			private set;
		} = new cPlayerContext();

		public override Vector3 GroundCheckOriginOffset => new Vector3(0f, 0.5f, 0f);

		public override float GroundCheckRayLength => 0.55f;

		public override float GroundCheckSphereRadius => 0.2f;

		public PlayerAnimationSequence AnimationSequence
		{
			get;
			private set;
		} = null;

		public PlayerAnimationContainer AnimationContainer => _AnimationContainer;

		[SerializeField]
		private PlayerAnimationContainer _AnimationContainer = null;
	}
}