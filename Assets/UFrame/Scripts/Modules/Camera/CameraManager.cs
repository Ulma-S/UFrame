using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class CameraManager : GlobalServiceElement<CameraManager>
	{
		public void ChangeActiveCamera(int cameraID, float duration = 0.8f, bool immediately = false)
		{
			if (_VirtualCameraHolder.TryGetValue(cameraID, out var virtualCamera))
			{
				PrevVirtualCamera = CurrentVirtualCamera;
				CurrentVirtualCamera?.Exit();
				CurrentVirtualCamera = virtualCamera;
				CurrentVirtualCamera.Enter();
				CurrentCameraIDInt = cameraID;
				if (virtualCamera is cPerspectiveCamera)
				{
					Camera.orthographic = false;
				}
				else if (virtualCamera is cOrthographicCamera)
				{
					Camera.orthographic = true;
				}
				if (immediately)
				{
					Camera.transform.position = CurrentVirtualCamera.Position;
					Camera.transform.rotation = CurrentVirtualCamera.Rotation;
					ChangeDurationTimer.Enabled = false;
				}
				else
				{
					ChangeDurationTimer.Reset();
					ChangeDurationTimer.Limit = duration;
					ChangeDurationTimer.Enabled = true;
				}
			}
			else
			{
				LogService.Error(this, $"{cameraID}‚ÌƒJƒƒ‰‚Í–¢“o˜^‚Å‚·");
			}
		}

		public void RegisterCamera(int cameraID, cVirtualCamera camera)
		{
			_VirtualCameraHolder.Add(cameraID, camera);
		}

		public void UnregisterCamera(int cameraID)
		{
			_VirtualCameraHolder.Remove(cameraID);
		}

		public void OnSafeFlag(CameraDef.SAFE_FLAG flag)
		{
			_SafeFlag.On(flag);
		}

		protected override void OnAwake()
		{
			Camera = Camera.main;
		}

		protected override void OnUpdate()
		{
			if (CurrentVirtualCamera == null)
			{
				return;
			}
			if (ChangeDurationTimer.Enabled)
			{
				UpdateChangeDuration();
			}
			else
			{
				CurrentVirtualCamera.Update();
				UpdateTransform();
			}

			_SafeFlag.UpdateFlags();
		}

		protected virtual void OnEndChangeDuration()
		{

		}

		protected virtual void OnUpdateTransform()
		{

		}

		private void UpdateChangeDuration()
		{
			if (PrevVirtualCamera == null)
			{
				Camera.transform.position = CurrentVirtualCamera.Position;
				Camera.transform.rotation = CurrentVirtualCamera.Rotation;
				ChangeDurationTimer.Enabled = false;
				return;
			}
			if (ChangeDurationTimer.IsTimeOut || ChangeDurationTimer.Limit == 0f)
			{
				Camera.transform.position = CurrentVirtualCamera.Position;
				Camera.transform.rotation = CurrentVirtualCamera.Rotation;
				OnEndChangeDuration();
				ChangeDurationTimer.Enabled = false;
				return;
			}
			UpdateOrthographicDuration();
			ChangeDurationTimer.Update(Time.deltaTime);
		}

		private void UpdateTransform()
		{
			var lerpScale = Time.deltaTime * UpdateTransformScale;
			if (_SafeFlag.IsOn(CameraDef.SAFE_FLAG.DISABLE_LERP))
			{
				Camera.transform.position = CurrentVirtualCamera.Position;
				Camera.transform.rotation = CurrentVirtualCamera.Rotation;
			}
			else
			{
				Camera.transform.position = Vector3.Lerp(Camera.transform.position, CurrentVirtualCamera.Position, lerpScale);
				Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, CurrentVirtualCamera.Rotation, lerpScale);
			}
			OnUpdateTransform();
		}

		private void UpdateOrthographicDuration()
		{
			var lerpScale = ChangeDurationTimer.Timer / ChangeDurationTimer.Limit;
			Camera.transform.position = Vector3.Lerp(PrevVirtualCamera.Position, CurrentVirtualCamera.Position, lerpScale);
			Camera.transform.rotation = Quaternion.Lerp(PrevVirtualCamera.Rotation, CurrentVirtualCamera.Rotation, lerpScale);
			var prevCamera = PrevVirtualCamera as cOrthographicCamera;
			var currentCamera = CurrentVirtualCamera as cOrthographicCamera;
			if (prevCamera != null && currentCamera != null)
			{
				Camera.orthographicSize = Mathf.Lerp(prevCamera.Size, currentCamera.Size, lerpScale);
			}
		}

		public int CurrentCameraIDInt
		{
			get;
			private set;
		} = -1;

		public bool IsCameraTransition => ChangeDurationTimer.Enabled;

		public cVirtualCamera CurrentVirtualCamera
		{
			get;
			private set;
		} = null;

		protected Camera Camera
		{
			get;
			private set;
		} = null;

		protected cVirtualCamera PrevVirtualCamera
		{
			get;
			private set;
		} = null;

		protected cTimer ChangeDurationTimer
		{
			get;
			private set;
		} = new cTimer();

		protected virtual float UpdateTransformScale
		{
			get;
			set;
		} = 2f;

		protected Dictionary<int, cVirtualCamera> _VirtualCameraHolder = new Dictionary<int, cVirtualCamera>();

		private cSafeFlag<CameraDef.SAFE_FLAG> _SafeFlag = new cSafeFlag<CameraDef.SAFE_FLAG>();
	}
}