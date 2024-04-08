using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cVirtualCamera
	{
		public static cVirtualCamera Create<T>() where T : cVirtualCamera, new()
		{
			var camera = new T();
			camera.Setup();
			return camera;
		}

		public void Setup()
		{
			OnSetup();
		}

		protected virtual void OnSetup()
		{

		}

		public void Update()
		{
			OnUpdate();
		}

		protected virtual void OnUpdate()
		{

		}

		protected cVirtualCamera()
		{

		}

		public Vector3 Position
		{
			get;
			set;
		} = Vector3.zero;

		public Quaternion Rotation
		{
			get;
			set;
		} = Quaternion.identity;
	}
}