using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cMicroModule
	{
		public void UpdateBegin()
		{
			OnUpdateBegin();
		}

		public bool Update()
		{
			return OnUpdate();
		}

		public void UpdateEnd()
		{
			OnUpdateEnd();
		}

		public void Activate()
		{
			IsEnabled = true;
		}

		public void Deactivate()
		{
			IsEnabled = false;
		}

		protected virtual void OnUpdateBegin() { }

		protected virtual bool OnUpdate() {  return true; }

		protected virtual void OnUpdateEnd() { }

		public bool IsEnabled
		{
			get;
			private set;
		} = false;
	}
}