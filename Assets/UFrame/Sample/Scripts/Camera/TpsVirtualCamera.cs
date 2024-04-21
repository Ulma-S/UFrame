using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class cTpsVirtualCamera : cPerspectiveCamera
	{
		protected override void OnSetup()
		{
			base.OnSetup();
			var param = GlobalService.Camera.Param.TpsCamera;
			LookAtTarget = GlobalService.Pl.PlayerCharacter.gameObject;
			Position = LookAtTarget.transform.position + param.BaseOffset;
			Rotation = param.BaseRotation;
			FOV = param.FOV;
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
			
			GlobalService.Camera.OnSafeFlag(uframe.CameraDef.SAFE_FLAG.DISABLE_LERP);

			var param = GlobalService.Camera.Param;
			var deltaAngle = param.TpsCamera.MoveSpeed * Time.deltaTime;
			var targetToCameraDir = Position - LookAtTarget.transform.position;
			if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_CAMERA_UP))
			{
				var rot = Quaternion.AngleAxis(deltaAngle, Transform.right);
				targetToCameraDir = rot * targetToCameraDir;
			}
			else if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_CAMERA_DOWN))
			{
				var rot = Quaternion.AngleAxis(-deltaAngle, Transform.right);
				targetToCameraDir = rot * targetToCameraDir;
			}
			else if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_CAMERA_RIGHT))
			{
				var rot = Quaternion.AngleAxis(-deltaAngle, Vector3.up);
				targetToCameraDir = rot * targetToCameraDir;
			}
			else if (GlobalService.Input.IsCommandSuccess(GAME_COMMAND_TYPE.MOVE_CAMERA_LEFT))
			{
				var rot = Quaternion.AngleAxis(deltaAngle, Vector3.up);
				targetToCameraDir = rot * targetToCameraDir;
			}
			var normalizedDir = targetToCameraDir.normalized;
			var projectionVec = new Vector3(normalizedDir.x, 0f, normalizedDir.z).normalized;
			var angleVertical = Vector3.Angle(projectionVec, normalizedDir);
			if (angleVertical < param.TpsCamera.MaxAngleVertical && angleVertical > param.TpsCamera.MinAngleVertical)
			{// äpìxêßå¿
				Position = LookAtTarget.transform.position + targetToCameraDir;
				Rotation = Quaternion.LookRotation(-normalizedDir, Vector3.up);
			}
		}
	}
}