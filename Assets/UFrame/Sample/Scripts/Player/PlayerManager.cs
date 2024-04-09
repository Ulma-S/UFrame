using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class PlayerManager : GlobalServiceElement<PlayerManager>
	{
		protected override void OnStart()
		{
			PlayerCharacter = FindObjectOfType<PlayerCharacter>();
		}

		public PlayerCharacter PlayerCharacter
		{
			get;
			private set;
		} = null;
	}
}