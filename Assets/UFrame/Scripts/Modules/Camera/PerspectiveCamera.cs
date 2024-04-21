using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cPerspectiveCamera : cVirtualCamera
	{
		public float FOV
		{
			get;
			protected set;
		} = 80f;

		public GameObject LookAtTarget
		{
			get;
			protected set;
		} = null;

		public Vector3? LookAtPosition
		{
			get;
			protected set;
		} = null;
	}
}