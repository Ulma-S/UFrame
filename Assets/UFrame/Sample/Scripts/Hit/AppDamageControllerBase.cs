using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class AppDamageControllerBase : DamageControllerBase
	{
		protected sealed override void OnApplyDamage(cDamageInfoBase damageInfo)
		{
			var appDamageInfo = damageInfo as cAppDamageInfoBase;
			OnApplyDamage(appDamageInfo);
		}

		protected virtual void OnApplyDamage(cAppDamageInfoBase damageInfo)
		{

		}
	}
}