using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public static partial class PlayerAction
	{
		public static class SetID
		{
			public static readonly ACTION_ID Idle = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 0);
			public static readonly ACTION_ID Move = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 1);
			public static readonly ACTION_ID Jump = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 2);
		}

		public static class ID
		{
			public const int Idle = 0;
			public const int Move = 1;
			public const int Jump = 2;
		}
	}
}