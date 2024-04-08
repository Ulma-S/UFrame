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
			public static readonly ACTION_ID AirJump = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 3);
			public static readonly ACTION_ID DamageSmall = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 4);
			public static readonly ACTION_ID DamageSmash = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 5);
			public static readonly ACTION_ID KneelDown = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 6);
			public static readonly ACTION_ID Victory = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 7);
		}

		public static class ID
		{
			public const int Idle = 0;
			public const int Move = 1;
			public const int Jump = 2;
			public const int AirJump = 3;
			public const int DamageSmall = 4;
			public const int DamageSmash = 5;
			public const int KneelDown = 6;
			public const int Victory = 7;
		}
	}
}