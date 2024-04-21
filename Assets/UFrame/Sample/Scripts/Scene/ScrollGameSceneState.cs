using System;
using System.Collections.Generic;
using UnityEngine;
using uframe;

namespace app
{
	public class cScrollGameSceneState : cSceneStateBase
	{
		protected override void OnEnter()
		{
			base.OnEnter();
			var scrollCamera = cVirtualCamera.Create<cScrollVirtualCamera>();
			var eventCamera = cVirtualCamera.Create<cEventVirtualCamera>();
			var goalCamera = cVirtualCamera.Create<cGoalVirtualCamera>();
			GlobalService.Camera.RegisterCamera(CameraDef.ID.SCROLL, scrollCamera);
			GlobalService.Camera.RegisterCamera(CameraDef.ID.EVENT, eventCamera);
			GlobalService.Camera.RegisterCamera(CameraDef.ID.GOAL, goalCamera);
			GlobalService.Camera.ChangeActiveCamera(CameraDef.ID.SCROLL, immediately: true);
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

		protected override void OnExit()
		{
			base.OnExit();
			GlobalService.Camera.UnregisterCamera(CameraDef.ID.SCROLL);
			GlobalService.Camera.UnregisterCamera(CameraDef.ID.EVENT);
			GlobalService.Camera.UnregisterCamera(CameraDef.ID.GOAL);
			GlobalService.Camera.ChangeActiveCamera(CameraDef.ID.DEFAULT);
		}

		public override bool CheckSceneTransition(out SceneDef.PACK_ID nextScenePackID)
		{
			nextScenePackID = SceneDef.PACK_ID.INVALID;
			if (GlobalService.Input.IsCommandSuccess(UI_COMMAND_TYPE.CANCEL))
			{
				nextScenePackID = SceneDef.PACK_ID.SAMPLE_TITLE;
				return true;
			}
			return false;
		}
	}
}