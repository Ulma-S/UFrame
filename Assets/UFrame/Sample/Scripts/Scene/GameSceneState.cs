using System;
using System.Collections.Generic;
using UnityEngine;
using uframe;

namespace app
{
	public class cGameSceneState : cSceneStateBase
	{
		protected override void OnEnter()
		{
			base.OnEnter();
			var scrollCamera = cVirtualCamera.Create<cScrollVirtualCamera>();
			var eventCamera = cVirtualCamera.Create<cEventVirtualCamera>();
			GlobalService.Camera.RegisterCamera(CameraDef.ID.SCROLL, scrollCamera);
			GlobalService.Camera.RegisterCamera(CameraDef.ID.EVENT, eventCamera);
			GlobalService.Camera.ChangeActiveCamera(CameraDef.ID.SCROLL);
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
			if (GlobalService.Input.IsCommandSuccess(UI_COMMAND_TYPE.DECIDE))
			{
				switch (GlobalService.Camera.CurrentCameraID)
				{
					case CameraDef.ID.SCROLL:
						GlobalService.Camera.ChangeActiveCamera(CameraDef.ID.EVENT);
						break;

					case CameraDef.ID.EVENT:
						GlobalService.Camera.ChangeActiveCamera(CameraDef.ID.SCROLL);
						break;
				}
			}
		}

		public override bool CheckSceneTransition(out SceneDef.PACK_ID nextScenePackID)
		{
			nextScenePackID = SceneDef.PACK_ID.INVALID;
			if (GlobalService.Input.IsCommandSuccess(UI_COMMAND_TYPE.CANCEL))
			{
				GlobalService.Scene.LoadScenePackWithFade(SceneDef.PACK_ID.SAMPLE_TITLE);
			}
			return false;
		}
	}
}