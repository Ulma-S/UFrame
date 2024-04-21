using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class ZoneControllerBase : MonoBehaviour
	{
		protected virtual void OnStart()
		{

		}

		protected virtual void OnUpdate()
		{

		}

		protected virtual void OnEnter(ZONE_HIT_INFO hitInfo)
		{

		}

		protected virtual void OnStay(ZONE_HIT_INFO hitInfo)
		{

		}

		protected virtual void OnExit(ZONE_HIT_INFO hitInfo)
		{

		}

		private void Start()
		{
			OnStart();
		}

		private void Update()
		{
			OnUpdate();
		}

		private void OnTriggerEnter(Collider other)
		{
			var zoneLocator = other.GetComponent<ZoneLocatorBase>();
			if (zoneLocator != null)
			{
				OnEnter(new ZONE_HIT_INFO(other, zoneLocator));
			}
		}

		private void OnTriggerStay(Collider other)
		{
			var zoneLocator = other.GetComponent<ZoneLocatorBase>();
			if (zoneLocator != null)
			{
				OnStay(new ZONE_HIT_INFO(other, zoneLocator));
			}
		}

		private void OnTriggerExit(Collider other)
		{
			var zoneLocator = other.GetComponent<ZoneLocatorBase>();
			if (zoneLocator != null)
			{
				OnExit(new ZONE_HIT_INFO(other, zoneLocator));
			}
		}
	}
}