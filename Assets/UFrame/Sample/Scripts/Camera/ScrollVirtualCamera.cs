using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class cScrollVirtualCamera : cOrthographicVirtualCamera
	{
		protected override void OnSetup()
		{
			base.OnSetup();
			var param = GlobalService.Camera.Param;
			Position = param.ScrollCamera.OriginPosition;
			Size = param.ScrollCamera.Size;
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
			var position = Position;
			position.x = GlobalService.Pl.PlayerCharacter.transform.position.x;
			Position = position;
		}
	}
}