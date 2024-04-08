using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public interface IDamageable
	{
		void ApplyDamage(cDamageInfoBase damageInfo);
	}
}