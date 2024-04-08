using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class AppCameraService : CameraService
	{
		public void ChangeActiveCamera(CameraDef.ID cameraID, float duration = 0.8f)
		{
			ChangeActiveCamera((int)cameraID, duration);
		}

		public void RegisterCamera(CameraDef.ID cameraID, cVirtualCamera camera)
		{
			RegisterCamera((int)cameraID, camera);
		}

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

		protected override void OnEndChangeDuration()
		{
			var currentCamera = CurrentVirtualCamera as cOrthographicVirtualCamera;
			Camera.orthographicSize = currentCamera.Size;
		}

		public CameraDef.ID CurrentCameraID => (CameraDef.ID)CurrentCameraIDInt;

		protected override float UpdateTransformScale => _Param.UpdateTransformScale;

		public CameraParamHolder Param => _Param;

		[SerializeField]
		private CameraParamHolder _Param = null;
	}
}