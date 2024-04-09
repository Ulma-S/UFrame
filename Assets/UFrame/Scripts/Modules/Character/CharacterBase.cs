using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace uframe
{
	public class CharacterBase : BehaviourBase, IPausable
	{
		public bool IsOnGround()
		{
			return CheckGroundDistance(GroundCheckRayLength);
		}

		public bool CheckGroundDistance(float distance)
		{
			if (Physics.SphereCast(transform.position + GroundCheckOriginOffset, GroundCheckSphereRadius, Vector3.down, out var hitInfo, distance))
			{
				if (hitInfo.collider != null && hitInfo.collider.tag == TagDef.Ground)
				{
					return true;
				}
			}
			return false;
		}

		public bool TryGetGroundPos(Vector3 pos, out Vector3 groundPos)
		{
			groundPos = pos;
			var offset = 3f;
			if (Physics.SphereCast(pos + new Vector3(0f, offset, 0f), GroundCheckSphereRadius, Vector3.down, out var hitInfo, GroundCheckRayLength + offset))
			{
				if (hitInfo.collider != null && hitInfo.collider.tag == TagDef.Ground)
				{
					groundPos = hitInfo.collider.ClosestPoint(pos);
					return true;
				}
			}
			return false;
		}

		public void Warp(Vector3 pos)
		{
			Pos = pos;
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
			_StandState.UpdateValue();
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

		protected CharacterController CharacterController
		{
			get;
			set;
		} = null;

		public CharacterDef.STAND_STATE StandState
		{
			get
			{
				return _StandState.Value;
			}
			set
			{
				_StandState.SetValue(value);
			}
		}

		private cSafeValue<CharacterDef.STAND_STATE> _StandState = new cSafeValue<CharacterDef.STAND_STATE>();

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