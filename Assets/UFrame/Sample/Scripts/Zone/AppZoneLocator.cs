using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class AppZoneLocator : ZoneLocatorBase
	{
		public virtual ZoneDef.ID ZoneID => (ZoneDef.ID)ZoneIDRaw;
	}
}