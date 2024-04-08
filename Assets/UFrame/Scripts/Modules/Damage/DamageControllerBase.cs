using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace uframe
{
	public class DamageControllerBase : MonoBehaviour, IDamageable
	{
		public void ApplyDamage(cDamageInfoBase damageInfo)
		{
			OnApplyDamage(damageInfo);
		}

		protected virtual void OnApplyDamage(cDamageInfoBase damageInfo)
		{
		}
	}
}