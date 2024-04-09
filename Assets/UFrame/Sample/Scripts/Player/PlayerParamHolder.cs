using System;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
	[CreateAssetMenu(menuName = "UFrame/Sample/PlayerParam")]
	public class PlayerParamHolder : ScriptableObject
	{
		public cPlayerCommonParam Common => _Common;

		[SerializeField]
		private cPlayerCommonParam _Common = null;
	}

	[Serializable]
	public class cPlayerCommonParam
	{
		[Serializable]
		public class cMoveInfo
		{
			public float MoveSpeed => _MoveSpeed;

			public float MoveSpeedScaleAir => _MoveSpeedScaleAir;

			public float JumpForce => _JumpForce;

			public float JumpForceScaleAir => _JumpForceScaleAir;

			public int MaxAirJumpCount => _MaxAirJumpCount;

			public float FastFallSpeed => _FastFallSpeed;

			public Vector3 GroundCheckOriginOffset => _GroundCheckOriginOffset;

			public float GroundCheckRayLength => _GroundCheckRayLength;

			public float JumpEndCheckRayLength => _JumpEndCheckRayLength;

			public float GroundCheckSphereRadius => _GroundCheckSphereRadius;

			[SerializeField, DisplayName("�ړ����x")]
			private float _MoveSpeed = 5f;

			[SerializeField, DisplayName("�󒆂ł̈ړ����x�W��")]
			private float _MoveSpeedScaleAir = 0.7f;

			[SerializeField, DisplayName("�W�����v��")]
			private float _JumpForce = 6f;

			[SerializeField, DisplayName("�󒆂ł̃W�����v�W��")]
			private float _JumpForceScaleAir = 0.8f;

			[SerializeField, DisplayName("�󒆂ł̍ő�W�����v�\��")]
			private int _MaxAirJumpCount = 1;

			[SerializeField, DisplayName("�����������x")]
			private float _FastFallSpeed = 10f;

			[SerializeField, DisplayName("�n�ʔ��肷��Ray�̔��˃I�t�Z�b�g")]
			private Vector3 _GroundCheckOriginOffset = new Vector3(0f, 0.5f, 0f);

			[SerializeField, DisplayName("�n�ʔ��肷��Ray�̒���")]
			private float _GroundCheckRayLength = 0.55f;

			[SerializeField, DisplayName("�W�����v�I�����肷��Ray�̒���")]
			private float _JumpEndCheckRayLength = 0.8f;

			[SerializeField, DisplayName("�n�ʔ��肷�鋅�̔��a")]
			private float _GroundCheckSphereRadius = 0.2f;
		}

		public cMoveInfo MoveInfo => _MoveInfo;

		[SerializeField]
		private cMoveInfo _MoveInfo = null;
	}
}