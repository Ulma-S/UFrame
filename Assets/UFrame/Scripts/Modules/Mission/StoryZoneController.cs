using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class StoryZoneController : MonoBehaviour
	{
		public cSafeEventHandler Register(Action<Collider> action)
		{
			return _OnStay.Register(action);
		}

		private void OnTriggerStay(Collider other)
		{
			_OnStay.Invoke(other);
		}

		private cSafeEvent<Collider> _OnStay = new cSafeEvent<Collider>();
	}
}