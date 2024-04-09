using app;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public static partial class GlobalService
	{
		public static AppInputService Input => (AppInputService)AppInputService.Instance;

		public static AppCameraManager Camera => (AppCameraManager)AppCameraManager.Instance;

		public static GameFlowManager GameFlow => GameFlowManager.Instance;

		public static PlayerManager Pl => PlayerManager.Instance;
	}
}