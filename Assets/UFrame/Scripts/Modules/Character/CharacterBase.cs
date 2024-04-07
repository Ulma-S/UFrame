using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class CharacterBase : MonoBehaviour, IPausable
	{
		public void SetStandState(CharacterDef.STAND_STATE standState)
		{
			StandState.SetValue(standState);
		}

		public bool CheckGround()
		{
			if (Physics.SphereCast(transform.position + GroundCheckOriginOffset, GroundCheckSphereRadius, Vector3.down, out var hitInfo, GroundCheckRayLength))
			{
				if (hitInfo.collider != null && hitInfo.collider.tag == TagDef.Ground)
				{
					return true;
				}
			}
			return false;
		}

		public void OnPause()
		{

		}

		public void OnResume()
		{

		}

		private void Start()
		{
			ActionController = cActionController.Create(this);
			AnimationController = cAnimationController.Create(gameObject);
			OnStart();
		}

		private void Update()
		{
			OnUpdate();
		}

		private void LateUpdate()
		{
			OnLateUpdate();
			Context.UpdateFlags();
		}

		private void OnDrawGizmos()
		{
			Debug.DrawLine(transform.position, transform.position + Vector3.down * GroundCheckRayLength);

			Gizmos.DrawWireSphere(transform.position + GroundCheckOriginOffset + Vector3.down * GroundCheckRayLength, GroundCheckSphereRadius);
		}

		protected virtual void OnStart() { }

		protected virtual void OnUpdate() { }

		protected virtual void OnLateUpdate() { }

		public cCharacterContext Context
		{
			get;
			private set;
		} = new cCharacterContext();

		public cActionController ActionController
		{
			get;
			private set;
		} = null;

		public cAnimationController AnimationController
		{
			get;
			protected set;
		} = null;

		protected cCharacterController CharacterController
		{
			get;
			set;
		} = null;

		public cSafeValue<CharacterDef.STAND_STATE> StandState
		{
			get;
			private set;
		} = new cSafeValue<CharacterDef.STAND_STATE>();

		public virtual Vector3 GroundCheckOriginOffset
		{
			get;
			protected set;
		} = Vector3.zero;

		public virtual float GroundCheckRayLength
		{
			get;
			protected set;
		} = 1f;

		public virtual float GroundCheckSphereRadius
		{
			get;
			private set;
		} = 0.5f;
	}
}