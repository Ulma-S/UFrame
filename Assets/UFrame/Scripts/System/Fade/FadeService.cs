using Cysharp.Threading.Tasks;
using System;
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
		public enum STATE
		{
			NONE,
			FADE_IN,
			FADE_OUT,
		}

		/// <summary>
		/// フェードインリクエスト
		/// </summary>
		/// <param name="duration">フェードの持続時間</param>
		/// <param name="startAlpha">開始時のアルファ値</param>
		/// <param name="onFadeEnd">フェード終了時のイベント</param>
		public async UniTask FadeIn(float duration = 1f, float startAlpha = 1f, Action onFadeEnd = null)
		{
			if (_State != STATE.NONE)
			{
				return;
			}
			_State = STATE.FADE_IN;
			_FadeTimer.Limit = duration;
			_FadeTimer.Reset();
			ForceSetAlpha(startAlpha);
			_StartAlpha = startAlpha;
			_OnFadeEnd = onFadeEnd;
			await UniTask.WaitUntil(() => _State == STATE.NONE, cancellationToken: this.GetCancellationTokenOnDestroy());
		}

		/// <summary>
		/// フェードアウトリクエスト
		/// </summary>
		/// <param name="duration">フェードの持続時間</param>
		/// <param name="startAlpha">開始時のアルファ値</param>
		/// <param name="onFadeEnd">フェード終了時のイベント</param>
		public async UniTask FadeOut(float duration = 1f, float startAlpha = 0f, Action onFadeEnd = null)
		{
			if (_State != STATE.NONE)
			{
				return;
			}
			_State = STATE.FADE_OUT;
			_FadeTimer.Limit = duration;
			_FadeTimer.Reset();
			ForceSetAlpha(startAlpha);
			_StartAlpha = startAlpha;
			_OnFadeEnd = onFadeEnd;
			await UniTask.WaitUntil(() => _State == STATE.NONE, cancellationToken: this.GetCancellationTokenOnDestroy());
		}

		/// <summary>
		/// アルファを設定
		/// </summary>
		/// <param name="alpha"></param>
		public void ForceSetAlpha(float alpha)
		{
			var color = _FadePanel.color;
			color.a = alpha;
			_FadePanel.color = color;
		}

		private void Start()
		{
			_FadePanel = GetComponent<Image>();
		}

		private void Update()
		{
			if (_State == STATE.NONE)
			{
				return;
			}
			var deltaSec = Time.deltaTime;
			_FadeTimer.Update(deltaSec);
			var remainTime = _FadeTimer.Limit - _FadeTimer.Timer;
			if (_State == STATE.FADE_IN)
			{
				if (remainTime < Mathf.Epsilon)
				{
					ForceSetAlpha(0f);
					_State = STATE.NONE;
					_OnFadeEnd?.Invoke();
				}
				else
				{
					var deltaAlpha = _StartAlpha * deltaSec / _FadeTimer.Limit;
					var color = _FadePanel.color;
					color.a -= deltaAlpha;
					if (color.a < Mathf.Epsilon)
					{
						color.a = 0f;
						_State = STATE.NONE;
						_OnFadeEnd?.Invoke();
					}
					ForceSetAlpha(color.a);
				}
			}
			else
			{
				if (remainTime < Mathf.Epsilon)
				{
					ForceSetAlpha(1f);
					_State = STATE.NONE;
					_OnFadeEnd?.Invoke();
				}
				else
				{
					var deltaAlpha = (1f - _StartAlpha) * deltaSec / _FadeTimer.Limit;
					var color = _FadePanel.color;
					color.a += deltaAlpha;
					if ((1f - color.a) < Mathf.Epsilon)
					{
						color.a = 1f;
						_State = STATE.NONE;
						_OnFadeEnd?.Invoke();
					}
					ForceSetAlpha(color.a);
				}
			}
		}

		private Image _FadePanel = null;

		private STATE _State = STATE.NONE;

		private cTimer _FadeTimer = new cTimer();

		private float _StartAlpha = 0f;

		private Action _OnFadeEnd = null;
	}
}