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

		public cTpsCamera TpsCamera => _TpsCamera;

		[SerializeField]
		private cTpsCamera _TpsCamera = null;

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

	[Serializable]
	public class cTpsCamera
	{
		public Vector3 BaseOffset => _BaseOffset;

		public Quaternion BaseRotation => _BaseRotation;

		public float FOV => _Fov;

		public float MoveSpeed => _MoveSpeed;

		public float MinAngleVertical => _MinAngleVertical;

		public float MaxAngleVertical => _MaxAngleVertical;

		[SerializeField]
		private Vector3 _BaseOffset = new Vector3(0f, 1f, -3f);

		[SerializeField]
		private Quaternion _BaseRotation = Quaternion.Euler(10f, 0f, 0f);

		[SerializeField]
		private float _Fov = 80f;

		[SerializeField]
		private float _MoveSpeed = 5f;

		[SerializeField]
		private float _MinAngleVertical = 10f;

		[SerializeField]
		private float _MaxAngleVertical = 50f;
	}
}