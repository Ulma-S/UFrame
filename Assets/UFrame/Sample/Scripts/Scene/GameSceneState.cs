using System;
using System.Collections.Generic;
using UnityEngine;
using uframe;

namespace app
{
	public class cGameSceneState : cSceneStateBase
	{
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