using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace uframe
{
	public class cAnimationController
	{
		public static cAnimationController Create(GameObject owner)
		{
			return new cAnimationController(owner);
		}

		public void SetAnimation(int stateHash, float transitionDuration = 0f)
		{
			Animator.Play(stateHash, 0, 0f);
		}

		public float GetFloat(int hash)
		{
			return Animator.GetFloat(hash);
		}

		public void SetFloat(int hash, float value)
		{
			Animator.SetFloat(hash, value);
		}

		protected cAnimationController(GameObject owner)
		{
			Owner = owner;
			Animator = owner.GetComponent<Animator>();
		}

		public bool IsAnimationEnd(int nameHash)
		{
			if (Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == nameHash)
			{
				return NormalizedTime >= 1f;
			}
			return false;
		}

		public float NormalizedTime
		{
			get
			{
				return Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
			}
		}

		protected GameObject Owner
		{
			get;
			private set;
		} = null;

		protected Animator Animator
		{
			get;
			private set;
		} = null;
	}
}