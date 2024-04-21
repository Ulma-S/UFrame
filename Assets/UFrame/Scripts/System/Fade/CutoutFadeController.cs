using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class CutoutFadeController : FadeControllerBase
	{
		public override async UniTask FadeIn(float duration = 1f, float startValue = 1f, Action onFadeEnd = null)
		{
			if (State != FadeDef.STATE.NONE)
			{
				return;
			}
			State = FadeDef.STATE.FADE_IN;
			_FadeTimer.Limit = duration;
			_FadeTimer.Reset();
			CutoutValue = 1f - startValue;
			_StartCutoutValue = 1f - startValue;
			_OnFadeEnd = onFadeEnd;
			await UniTask.WaitUntil(() => State == FadeDef.STATE.NONE, cancellationToken: this.GetCancellationTokenOnDestroy());
		}

		public override async UniTask FadeOut(float duration = 1f, float startValue = 0f, Action onFadeEnd = null)
		{
			if (State != FadeDef.STATE.NONE)
			{
				return;
			}
			State = FadeDef.STATE.FADE_OUT;
			_FadeTimer.Limit = duration;
			_FadeTimer.Reset();
			CutoutValue = 1f - startValue;
			_StartCutoutValue = 1f- startValue;
			_OnFadeEnd = onFadeEnd;
			await UniTask.WaitUntil(() => State == FadeDef.STATE.NONE, cancellationToken: this.GetCancellationTokenOnDestroy());
		}

		public override void UpdateController()
		{
			LogService.Info(this, $"Cutout:{CutoutValue}");
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
					CutoutValue = 1f;
					State = FadeDef.STATE.NONE;
					_OnFadeEnd?.Invoke();
				}
				else
				{
					var delta = (1f - _StartCutoutValue) * deltaSec / _FadeTimer.Limit;
					CutoutValue += delta;
					if ((1f - CutoutValue) < Mathf.Epsilon)
					{
						CutoutValue = 0f;
						State = FadeDef.STATE.NONE;
						_OnFadeEnd?.Invoke();
					}
				}
			}
			else
			{
				if (remainTime < Mathf.Epsilon)
				{
					CutoutValue = 0f;
					State = FadeDef.STATE.NONE;
					_OnFadeEnd?.Invoke();
				}
				else
				{
					var delta = _StartCutoutValue * deltaSec / _FadeTimer.Limit;
					CutoutValue -= delta;
					if (CutoutValue < Mathf.Epsilon)
					{
						CutoutValue = 1f;
						State = FadeDef.STATE.NONE;
						_OnFadeEnd?.Invoke();
					}
				}
			}
		}

		public override FadeDef.STATE State
		{
			get;
			protected set;
		} = FadeDef.STATE.NONE;

		protected override FadeDef.TYPE FadeType => FadeDef.TYPE.CUTOUT;

		private float CutoutValue
		{
			get
			{
				return FadePanel.material.GetFloat("_Scale");
			}
			set
			{
				FadePanel.material.SetFloat("_Scale", value);
			}
		}

		private cTimer _FadeTimer = new cTimer();
		private float _StartCutoutValue = 0f;
		private Action _OnFadeEnd = null;
	}
}