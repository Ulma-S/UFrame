using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public interface IPausable
	{
		void OnPause();

		void OnResume();
	}
}