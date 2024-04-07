using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class InputService : GlobalServiceElement<InputService>
	{
		public bool IsKeyPressed(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		public bool IsKeyReleased(KeyCode key)
		{
			return Input.GetKeyUp(key);
		}
	}
}