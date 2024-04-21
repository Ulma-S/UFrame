using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class AppCameraManager : CameraManager
	{
		public void ChangeActiveCamera(CameraDef.ID cameraID, float duration = 0.8f, bool immediately = false)
		{
			ChangeActiveCamera((int)cameraID, duration, immediately);
		}

		public void RegisterCamera(CameraDef.ID cameraID, cVirtualCamera camera)
		{
			RegisterCamera((int)cameraID, camera);
		}

		public void UnregisterCamera(CameraDef.ID cameraID)
		{
			UnregisterCamera((int)cameraID);
		}

		public CameraDef.ID CurrentCameraID => (CameraDef.ID)CurrentCameraIDInt;

		protected override float UpdateTransformScale => _Param.UpdateTransformScale;

		public CameraParamHolder Param => _Param;

		[SerializeField]
		private CameraParamHolder _Param = null;
	}
}