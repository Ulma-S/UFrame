using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace uframe
{
	public class DebugDashboard : MonoBehaviour
	{
		private void Start()
		{
			for (int i = 0; i < ContentCount; i++)
			{
				_Contents[i] = _ContentSize;
				_Contents[i].y *= i + 1;
			}
		}

		private void Update()
		{
			if (GlobalService.Input.IsCommandSuccess(DEBUG_COMMAND_TYPE.SWITCH_DASHBOARD))
			{
				_IsOpened = !_IsOpened;
			}
		}

		private void OnGUI()
		{
			if (_IsOpened)
			{
				var windowWidth = Screen.currentResolution.width - 20;
				var windowHeight = Screen.currentResolution.height - 20;
				//GUI.Box(new Rect(10, 10, windowWidth, windowHeight), "DebugDashboard");
				var buttonStyle = GUI.skin.button;
				var boxStyle = GUI.skin.box;
				buttonStyle.fontSize = 40;
				boxStyle.alignment = TextAnchor.MiddleCenter;
				boxStyle.fontSize = 40;
				_ScrollPosition = GUI.BeginScrollView(_ScrollRect, _ScrollPosition, _ViewRect);
				GUI.Box(_Contents[0], "Debug");
				GUI.Button(_Contents[1], "Button");
				GUI.Button(_Contents[2], "Button");
				GUI.Button(_Contents[3], "Button");
				GUI.Button(_Contents[4], "Button");
				GUI.Button(_Contents[5], "Button");
				GUI.EndScrollView();
			}
		}

		const int Margin_x = 50;
		const int Margin_y = 100;
		const int Width = 500;
		const int Height = 100;
		const int ContentCount = 6;

		private bool _IsOpened = false;
		
		private Rect _ScrollRect = new Rect(Margin_x, Height * 2, Width + Margin_x, Height * 5);
		private Vector2 _ScrollPosition = default;
		private Rect _ViewRect = new Rect(Margin_x, Height, Width, Height * ContentCount);

		private Rect _ContentSize = new Rect(Margin_x, Margin_y, Width, Height);

		private Rect[] _Contents = new Rect[ContentCount];
	}
}