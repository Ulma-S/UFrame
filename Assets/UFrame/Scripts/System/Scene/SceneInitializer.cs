using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	[DefaultExecutionOrder((int)EXECUTION_ORDER.SCENE_INITIALIZER)]
	public class SceneInitializer : MonoBehaviour
	{
		[SerializeField]
		private SceneDef.PACK_ID _ScenePackID = SceneDef.PACK_ID.INVALID;

		public void Initialize()
		{
			GlobalService.Scene.Setup(_ScenePackID);
		}
	}
}