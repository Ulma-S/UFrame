using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class AppCameraManager : CameraManager
	{
		protected override void OnUpdateChangeDuration()
		{
			var lerpScale = ChangeDurationTimer.Timer / ChangeDurationTimer.Limit;
			var prevCamera = PrevVirtualCamera as cOrthographicVirtualCamera;
			var currentCamera = CurrentVirtualCamera as cOrthographicVirtualCamera;
			if (prevCamera != null && currentCamera != null)
			{
				Camera.orthographicSize = Mathf.Lerp(prevCamera.Size, currentCamera.Size, lerpScale);
			}
		}

		protected override float UpdateTransformScale => 4f;

		public CameraParamHolder Param => _Param;

		[SerializeField]
		private CameraParamHolder _Param = null;
	}
}