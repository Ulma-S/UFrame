using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class PlayerAnimationSequence : AnimationSequence
	{
		public bool IsCancellable => GetBool(CancellableHash);

		private readonly int CancellableHash = Animator.StringToHash("Cancellable");
	}
}