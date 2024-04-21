using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class cGoalVirtualCamera : cOrthographicCamera
	{
		protected override void OnSetup()
		{
			base.OnSetup();
			Position = new Vector3(8f, 2f, -10f);
			Size = 2f;
		}
	}
}