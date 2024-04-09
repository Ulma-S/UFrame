using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class BehaviourBase : MonoBehaviour
	{
		public Vector3 Pos
		{
			get => transform.position;
			set => transform.position = value;
		}

		public Quaternion Rot
		{
			get => transform.rotation;
			set => transform.rotation = value;
		}
	}
}