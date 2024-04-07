using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cCharacterController
	{
		public static cCharacterController Create<T>(CharacterBase character) where T : cCharacterController, new()
		{
			var controller = new T();
			controller.Setup(character);
			return controller;
		}

		public void Update()
		{
			OnUpdate();
		}

		protected virtual void OnUpdate() { }

		protected void Setup(CharacterBase character)
		{
			Character = character;
		}

		public ACTION_ID CurrentActionID
		{
			get;
			protected set;
		} = ACTION_ID.Invalid;

		protected CharacterBase Character
		{
			get;
			private set;
		} = null;
	}
}