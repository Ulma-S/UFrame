using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public static class GameObjectExtension
	{
		public static T FindObjectOfInterface<T>() where T : class
		{
			var components = GameObject.FindObjectsOfType<Component>();
			foreach (var component in components)
			{
				if (component is T)
				{
					return component as T;
				}
			}
			return null;
		}

		public static T[] FindObjectsOfInterface<T>() where T : class
		{
			var components = GameObject.FindObjectsOfType<Component>();
			var ret = new T[components.Length];
			int index = 0;
			foreach (var component in components)
			{
				if (component is T)
				{
					ret[index] = component as T;
					index++;
				}
			}
			Array.Resize(ref ret, index);
			return ret;
		}
	}
}