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

			[SerializeField, DisplayName("移動速度")]
			private float _MoveSpeed = 5f;

			[SerializeField, DisplayName("空中での移動速度係数")]
			private float _MoveSpeedScaleAir = 0.7f;

			[SerializeField, DisplayName("ジャンプ力")]
			private float _JumpForce = 6f;

			[SerializeField, DisplayName("空中でのジャンプ係数")]
			private float _JumpForceScaleAir = 0.8f;

			[SerializeField, DisplayName("空中での最大ジャンプ可能回数")]
			private int _MaxAirJumpCount = 1;

			[SerializeField, DisplayName("高速落下速度")]
			private float _FastFallSpeed = 10f;

			[SerializeField, DisplayName("地面判定するRayの発射オフセット")]
			private Vector3 _GroundCheckOriginOffset = new Vector3(0f, 0.5f, 0f);

			[SerializeField, DisplayName("地面判定するRayの長さ")]
			private float _GroundCheckRayLength = 0.55f;

			[SerializeField, DisplayName("ジャンプ終了判定するRayの長さ")]
			private float _JumpEndCheckRayLength = 0.8f;

			[SerializeField, DisplayName("地面判定する球の半径")]
			private float _GroundCheckSphereRadius = 0.2f;
		}

		public cMoveInfo MoveInfo => _MoveInfo;

		[SerializeField]
		private cMoveInfo _MoveInfo = null;
	}
}