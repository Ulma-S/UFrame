using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public static class BehaviourExtension
	{
		public static Vector3 Pos(this MonoBehaviour behaviour)
		{
			return behaviour.transform.position;
		}

		public static Quaternion Rot(this MonoBehaviour behaviour)
		{
			return behaviour.transform.rotation;
		}
	}
}