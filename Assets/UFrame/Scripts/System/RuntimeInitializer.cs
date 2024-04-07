using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace uframe
{
	public static class RuntimeInitializer
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void InitializeBeforeSceneLoad()
		{
			bool isResidentSceneLoaded = false;
			for (int i = 0, count = SceneManager.sceneCount; i < count; i++)
			{
				var scene = SceneManager.GetSceneAt(i);
				if (scene.name == SceneDef.ResidentSceneName)
				{
					isResidentSceneLoaded = true;
					break;
				}
			}
			if (!isResidentSceneLoaded)
			{
				SceneManager.LoadScene(SceneDef.ResidentSceneName, LoadSceneMode.Additive);
			}
		}
	}
}