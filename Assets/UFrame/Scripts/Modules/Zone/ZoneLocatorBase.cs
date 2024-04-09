using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class ZoneLocatorBase : BehaviourBase
	{
		protected virtual int ZoneIDRaw
		{
			get;
		} = -1;
	}
}