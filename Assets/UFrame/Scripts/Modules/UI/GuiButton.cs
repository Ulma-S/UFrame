using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace uframe
{
	public class GuiButton : GuiElement
	{
		protected virtual void OnClicked() { }

		private void Awake()
		{
			_Button = GetComponent<Button>();
		}

		private void Start()
		{
			_Button.onClick.AddListener(OnClicked);
		}

		private Button _Button = null;
	}
}