using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public static partial class FadeDef
	{
		public enum STATE
		{
			NONE,
			FADE_IN,
			FADE_OUT,
		}

		public enum TYPE
		{
			ALPHA,
			CUTOUT,
		}
	}
}