using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public abstract class GlobalServiceElement<T> : MonoBehaviour where T : MonoBehaviour
	{
		protected virtual void OnAwake()
		{

		}

		private void Awake()
		{
			OnAwake();
		}

		public static T Instance
		{
			get
			{
				if (_Instance != null)
				{
					return _Instance;
				}
				_Instance = FindObjectOfType<T>();
				if (_Instance == null)
				{
					LogService.Error($"{typeof(T)}がアタッチされていません");
				}
				return _Instance;
			}
		}

		private static T _Instance = null;
	}
}