using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace uframe
{
	public class PauseService : GlobalServiceElement<PauseService>
	{
		public void RequestPause()
		{
			if (IsPaused)
			{
				return;
			}
			var pausableObjects = GameObjectExtension.FindObjectsOfInterface<IPausable>();
			foreach (var pausable in pausableObjects)
			{
				pausable.OnPause();
			}
			IsPaused = true;
		}

		public void RequestResume()
		{

		}

		public bool IsPaused
		{
			get;
			private set;
		} = false;
	}
}