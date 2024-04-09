using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public struct ZONE_HIT_INFO
	{
		public ZONE_HIT_INFO(Collider collider, ZoneLocatorBase locator)
		{
			Collider = collider;
			Locator = locator;
		}

		public Collider Collider
		{
			get;
		}

		public ZoneLocatorBase Locator
		{
			get;
		}
	}
}