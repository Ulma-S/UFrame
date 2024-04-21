using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class cTpsGameState : cSceneStateBase
	{
		protected override void OnEnter()
		{
			base.OnEnter();
			var tpsCamera = cVirtualCamera.Create<cTpsVirtualCamera>();
			GlobalService.Camera.RegisterCamera(CameraDef.ID.THIRD_PERSON, tpsCamera);
			GlobalService.Camera.ChangeActiveCamera(CameraDef.ID.THIRD_PERSON, immediately: true);
		}
	}
}