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
			Transform = Camera.main.transform;
			OnSetup();
		}

		protected virtual void OnSetup()
		{

		}

		public void Enter()
		{
			OnEnter();
		}

		protected virtual void OnEnter()
		{

		}

		public void Update()
		{
			OnUpdate();
		}

		protected virtual void OnUpdate()
		{

		}

		public void Exit()
		{
			OnExit();
		}

		protected virtual void OnExit()
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

		public Transform Transform
		{
			get;
			private set;
		} = null;
	}
}