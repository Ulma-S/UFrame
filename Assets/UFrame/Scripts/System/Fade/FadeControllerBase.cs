using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace uframe
{
	public abstract class FadeControllerBase : MonoBehaviour
	{
		public abstract UniTask FadeIn(float duration = 1f, float startAlpha = 1f, Action onFadeEnd = null);

		public abstract UniTask FadeOut(float duration = 1f, float startAlpha = 0f, Action onFadeEnd = null);

		public abstract void UpdateController();

		private void Awake()
		{
			FadePanel = GetComponent<Image>();
		}

		private void Start()
		{
			GlobalService.Fade.RegisterController(FadeType, this);
		}

		public abstract FadeDef.STATE State
		{
			get;
			protected set;
		}

		protected abstract FadeDef.TYPE FadeType
		{
			get;
		}

		protected Image FadePanel
		{
			get;
			private set;
		} = null;
	}
}