using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class CameraService : GlobalServiceElement<CameraService>
	{
		public void ChangeActiveCamera(int cameraID, float duration = 0.8f)
		{
			if (_VirtualCameraHolder.TryGetValue(cameraID, out var camera))
			{
				PrevVirtualCamera = CurrentVirtualCamera;
				CurrentVirtualCamera = camera;
				CurrentCameraIDInt = cameraID;
				ChangeDurationTimer.Reset();
				ChangeDurationTimer.Limit = duration;
				ChangeDurationTimer.Enabled = true;
			}
			else
			{
				LogService.Error(this, $"{cameraID}ÇÃÉJÉÅÉâÇÕñ¢ìoò^Ç≈Ç∑");
			}
		}

		public void RegisterCamera(int cameraID, cVirtualCamera camera)
		{
			_VirtualCameraHolder.Add(cameraID, camera);
		}

		protected override void OnStart()
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
		}

		protected virtual void OnUpdateChangeDuration()
		{

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
			var lerpScale = ChangeDurationTimer.Timer / ChangeDurationTimer.Limit;
			Camera.transform.position = Vector3.Lerp(PrevVirtualCamera.Position, CurrentVirtualCamera.Position, lerpScale);
			Camera.transform.rotation = Quaternion.Lerp(PrevVirtualCamera.Rotation, CurrentVirtualCamera.Rotation, lerpScale);
			OnUpdateChangeDuration();
			ChangeDurationTimer.Update(Time.deltaTime);
		}

		private void UpdateTransform()
		{
			var lerpScale = Time.deltaTime * UpdateTransformScale;
			Camera.transform.position = Vector3.Lerp(Camera.transform.position, CurrentVirtualCamera.Position, lerpScale);
			Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, CurrentVirtualCamera.Rotation, lerpScale);
			OnUpdateTransform();
		}

		public int CurrentCameraIDInt
		{
			get;
			private set;
		} = -1;

		protected Camera Camera
		{
			get;
			private set;
		} = null;

		protected cVirtualCamera CurrentVirtualCamera
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

		private Dictionary<int, cVirtualCamera> _VirtualCameraHolder = new Dictionary<int, cVirtualCamera>();
	}
}