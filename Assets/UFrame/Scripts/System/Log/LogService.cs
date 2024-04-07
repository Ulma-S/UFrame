using UnityEngine;

namespace uframe
{
	public static class LogService
	{
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Info(string message)
		{
			Debug.Log(message);
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Info(GameObject gameObject, string message)
		{
			var name = string.Empty;
			if (gameObject != null)
			{
				name = gameObject.name;
			}
			Info($"[{name}]{message}");
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Info(Component component, string message)
		{
			var name = string.Empty;
			if (component != null)
			{
				name = component.GetType().Name;
			}
			Info($"[{name}]{message}");
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Info(object obj, string message)
		{
			var name = string.Empty;
			if (obj != null)
			{
				name = obj.ToString();
			}
			Info($"[{name}]{message}");
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Error(string message)
		{
			Debug.LogError(message);
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Error(GameObject gameObject, string message)
		{
			var name = string.Empty;
			if (gameObject != null)
			{
				name = gameObject.name;
			}
			Error($"[{name}]{message}");
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Error(Component component, string message)
		{
			var name = string.Empty;
			if (component != null)
			{
				name = component.GetType().Name;
			}
			Error($"[{name}]{message}");
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Error(object obj, string message)
		{
			var name = string.Empty;
			if (obj != null)
			{
				name = obj.ToString();
			}
			Error($"[{name}]{message}");
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Assert(string message)
		{
			Debug.Assert(true, message);
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Assert(GameObject gameObject, string message)
		{
			var name = string.Empty;
			if (gameObject != null)
			{
				name = gameObject.name;
			}
			Assert($"[{name}]{message}");
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Assert(Component component, string message)
		{
			var name = string.Empty;
			if (component != null)
			{
				name = component.GetType().Name;
			}
			Assert($"[{name}]{message}");
		}

		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Assert(object obj, string message)
		{
			var name = string.Empty;
			if (obj != null)
			{
				name = obj.ToString();
			}
			Assert($"[{name}]{message}");
		}
	}
}