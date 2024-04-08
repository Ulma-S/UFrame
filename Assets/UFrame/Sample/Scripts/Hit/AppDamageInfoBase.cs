using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class cAppDamageInfoBase : cDamageInfoBase
	{
		public HitDef.TYPE Type
		{
			get;
			set;
		}
	}
}