using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace app
{
	public class DisplayNameAttribute : PropertyAttribute
	{
		public GUIContent Label
		{
			get;
		} = null;

		public DisplayNameAttribute(string label)
		{
			Label = new GUIContent(label);
		}
	}

#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(DisplayNameAttribute))]
	public class DisplayNameAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var displayName = attribute as DisplayNameAttribute;
			label = displayName.Label;
			EditorGUI.PropertyField(position, property, label, true);
		}
	}
#endif
}