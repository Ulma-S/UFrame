using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class PlayerZoneController : ZoneControllerBase
	{
		protected override void OnEnter(ZONE_HIT_INFO hitInfo)
		{
			if (hitInfo.Locator == null)
			{
				return;
			}
			var locator = hitInfo.Locator as AppZoneLocator;
			var zoneID = locator.ZoneID;
			switch (zoneID)
			{
				case ZoneDef.ID.GOAL:
					GlobalService.GameFlow.NotifyGoal(locator as GoalBeacon);
					break;
			}
		}
	}
}