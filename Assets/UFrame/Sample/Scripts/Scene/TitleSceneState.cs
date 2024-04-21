using System;
using System.Collections.Generic;
using UnityEngine;
using uframe;

namespace app
{
	public class cTitleSceneState : cSceneStateBase
	{
		protected override void OnEnter()
		{
			base.OnEnter();
			GlobalService.Camera.ChangeActiveCamera(CameraDef.ID.DEFAULT);
		}

		public override bool CheckSceneTransition(out SceneDef.PACK_ID nextScenePackID)
		{
			nextScenePackID = SceneDef.PACK_ID.INVALID;
			if (GlobalService.Input.IsCommandSuccess(UI_COMMAND_TYPE.DECIDE))
			{
				nextScenePackID = SceneDef.PACK_ID.SAMPLE_GAME;
				return true;
			}
			return false;
		}
	}
}