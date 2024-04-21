using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class AlphaFadeController : FadeControllerBase
	{
		public override async UniTask FadeIn(float duration = 1, float startAlpha = 1, Action onFadeEnd = null)
		{
			if (State != FadeDef.STATE.NONE)
			{
				return;
			}
			State = FadeDef.STATE.FADE_IN;
			_FadeTimer.Limit = duration;
			_FadeTimer.Reset();
			ForceSetAlpha(startAlpha);
			_StartAlpha = startAlpha;
			_OnFadeEnd = onFadeEnd;
			await UniTask.WaitUntil(() => State == FadeDef.STATE.NONE, cancellationToken: this.GetCancellationTokenOnDestroy());
		}

		public override async UniTask FadeOut(float duration = 1, float startAlpha = 0, Action onFadeEnd = null)
		{
			if (State != FadeDef.STATE.NONE)
			{
				return;
			}
			State = FadeDef.STATE.FADE_OUT;
			_FadeTimer.Limit = duration;
			_FadeTimer.Reset();
			ForceSetAlpha(startAlpha);
			_StartAlpha = startAlpha;
			_OnFadeEnd = onFadeEnd;
			await UniTask.WaitUntil(() => State == FadeDef.STATE.NONE, cancellationToken: this.GetCancellationTokenOnDestroy());
		}

		public override void UpdateController()
		{
			if (State == FadeDef.STATE.NONE)
			{
				return;
			}
			var deltaSec = Time.deltaTime;
			_FadeTimer.Update(deltaSec);
			var remainTime = _FadeTimer.Limit - _FadeTimer.Timer;
			if (State == FadeDef.STATE.FADE_IN)
			{
				if (remainTime < Mathf.Epsilon)
				{
					ForceSetAlpha(0f);
					State = FadeDef.STATE.NONE;
					_OnFadeEnd?.Invoke();
				}
				else
				{
					var deltaAlpha = _StartAlpha * deltaSec / _FadeTimer.Limit;
					var color = FadePanel.color;
					color.a -= deltaAlpha;
					if (color.a < Mathf.Epsilon)
					{
						color.a = 0f;
						State = FadeDef.STATE.NONE;
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
					State = FadeDef.STATE.NONE;
					_OnFadeEnd?.Invoke();
				}
				else
				{
					var deltaAlpha = (1f - _StartAlpha) * deltaSec / _FadeTimer.Limit;
					var color = FadePanel.color;
					color.a += deltaAlpha;
					if ((1f - color.a) < Mathf.Epsilon)
					{
						color.a = 1f;
						State = FadeDef.STATE.NONE;
						_OnFadeEnd?.Invoke();
					}
					ForceSetAlpha(color.a);
				}
			}
		}

		private void ForceSetAlpha(float alpha)
		{
			var color = FadePanel.color;
			color.a = alpha;
			FadePanel.color = color;
		}

		public override FadeDef.STATE State
		{
			get;
			protected set;
		} = FadeDef.STATE.NONE;

		protected override FadeDef.TYPE FadeType => FadeDef.TYPE.ALPHA;

		private cTimer _FadeTimer = new cTimer();
		private float _StartAlpha = 0f;
		private Action _OnFadeEnd = null;
	}
}