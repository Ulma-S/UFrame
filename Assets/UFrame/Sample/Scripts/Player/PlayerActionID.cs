using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public static partial class PlayerAction
	{
		public static partial class Scroll
		{
			public static class SetID
			{
				public static readonly ACTION_ID Idle = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 0);
				public static readonly ACTION_ID Move = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 1);
				public static readonly ACTION_ID Jump = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 2);
				public static readonly ACTION_ID AirJump = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 3);
				public static readonly ACTION_ID FastFall = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 4);
				public static readonly ACTION_ID DamageSmall = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 5);
				public static readonly ACTION_ID DamageSmash = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 6);
				public static readonly ACTION_ID KneelDown = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 7);
				public static readonly ACTION_ID Victory = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 8);
			}

			public static class ID
			{
				public const int Idle = 0;
				public const int Move = 1;
				public const int Jump = 2;
				public const int AirJump = 3;
				public const int FastFall = 4;
				public const int DamageSmall = 5;
				public const int DamageSmash = 6;
				public const int KneelDown = 7;
				public const int Victory = 8;
			}
		}

		public static partial class Tps
		{
			public static class SetID
			{
				public static readonly ACTION_ID Idle = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 100);
				public static readonly ACTION_ID Move = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 101);
				public static readonly ACTION_ID Jump = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 102);
				public static readonly ACTION_ID AirJump = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 103);
				public static readonly ACTION_ID FastFall = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 104);
				public static readonly ACTION_ID DamageSmall = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 105);
				public static readonly ACTION_ID DamageSmash = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 106);
				public static readonly ACTION_ID KneelDown = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 107);
				public static readonly ACTION_ID Victory = new ACTION_ID((int)ActionDef.CATEGORY.PLAYER, 108);
			}

			public static class ID
			{
				public const int Idle = 100;
				public const int Move = 101;
				public const int Jump = 102;
				public const int AirJump = 103;
				public const int FastFall = 104;
				public const int DamageSmall = 105;
				public const int DamageSmash = 106;
				public const int KneelDown = 107;
				public const int Victory = 108;
			}
		}
	}
}