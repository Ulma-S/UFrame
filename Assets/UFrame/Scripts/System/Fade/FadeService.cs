using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace uframe
{
	/// <summary>
	/// ■概要
	/// 画面のフェードイン、フェードアウトを制御するクラス
	/// 
	/// 
	/// ■使い方
	/// ▶フェードイン
	/// ・処理を待機する場合
	/// SceneService.csを参考にasync, awaitを付けてください
	/// GlobalService.Fade.FadeIn().SuppressCancellationThrow();
	/// 
	/// ・処理を待機せず続行する場合
	/// GlobalService.Fade.FadeIn().Forget();
	/// 
	/// 
	/// ▶フェードアウト
	/// ・処理を待機する場合
	/// SceneService.csを参考にasync, awaitを付けてください
	/// GlobalService.Fade.FadeOut().SuppressCancellationThrow();
	/// 
	/// ・処理を待機せず続行する場合
	/// GlobalService.Fade.FadeOut().Forget();
	/// </summary>
	public class FadeService : GlobalServiceElement<FadeService>
	{
		public async UniTask FadeIn(FadeDef.TYPE fadeType, float duration = 1f, float startAlpha = 1f, Action onFadeEnd = null)
		{
			if (_FadeControllerHolder.TryGetValue((int)fadeType, out var controller))
			{
				_CurrentFadeType = fadeType;
				await controller.FadeIn(duration, startAlpha, onFadeEnd);
			}
		}

		public async UniTask FadeOut(FadeDef.TYPE fadeType, float duration = 1f, float startAlpha = 0f, Action onFadeEnd = null)
		{
			if (_FadeControllerHolder.TryGetValue((int)fadeType, out var controller))
			{
				_CurrentFadeType = fadeType;
				await controller.FadeOut(duration, startAlpha, onFadeEnd);
			}
		}

		public void RegisterController(FadeDef.TYPE fadeType, FadeControllerBase controller)
		{
			_FadeControllerHolder.Add((int)fadeType, controller);
		}

		private void Update()
		{
			if (_FadeControllerHolder.TryGetValue((int)_CurrentFadeType, out var controller))
			{
				controller.UpdateController();
			}
		}

		private Dictionary<int, FadeControllerBase> _FadeControllerHolder = new Dictionary<int, FadeControllerBase>();

		private FadeDef.TYPE _CurrentFadeType = FadeDef.TYPE.ALPHA;
	}
}