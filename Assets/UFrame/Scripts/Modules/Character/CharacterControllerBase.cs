using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class CharacterControllerBase : MonoBehaviour
	{
		protected virtual void OnStart() { }

		protected virtual void OnUpdate() { }

		private void Start()
		{
			Character = GetComponent<CharacterBase>();
			OnStart();
		}

		private void Update()
		{
			OnUpdate();
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