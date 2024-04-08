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
			_PlayerChara = GameObject.FindObjectOfType<PlayerCharacter>();
			var param = GlobalService.Camera.Param;
			Position = param.ScrollCamera.OriginPosition;
			Size = param.ScrollCamera.Size;
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
			var position = Position;
			position.x = _PlayerChara.transform.position.x;
			Position = position;
		}

		private PlayerCharacter _PlayerChara = null;
	}
}