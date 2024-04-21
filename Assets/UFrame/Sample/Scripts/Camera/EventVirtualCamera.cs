using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class cEventVirtualCamera : cOrthographicCamera
	{
		protected override void OnSetup()
		{
			base.OnSetup();
			Position = new Vector3(4f, 2f, -10f);
			Size = 3f;
		}
	}
}