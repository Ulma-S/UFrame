using System;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
	public class GoalBeacon : AppZoneLocator
	{
		public override ZoneDef.ID ZoneID => ZoneDef.ID.GOAL;
	}
}