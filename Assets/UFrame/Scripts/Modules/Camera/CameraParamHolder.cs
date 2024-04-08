using System;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
	[CreateAssetMenu(menuName = "UFrame/Sample/Camera")]
	public class CameraParamHolder : ScriptableObject
	{
		public cScrollCamera ScrollCamera => _ScrollCamera;

		[SerializeField]
		private cScrollCamera _ScrollCamera = null;

		public float UpdateTransformScale => _UpdateTransformScale;

		[SerializeField]
		private float _UpdateTransformScale = 5f;
	}

	[Serializable]
	public class cScrollCamera
	{
		public Vector3 OriginPosition => _OriginPosition;

		public float Size => _Size;

		[SerializeField]
		private Vector3 _OriginPosition = new Vector3(0f, 2.5f, -10f);

		[SerializeField]
		private float _Size = 5f;
	}
}