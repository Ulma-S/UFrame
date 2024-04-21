using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	[DefaultExecutionOrder((int)EXECUTION_ORDER.PLAYER_DAMAGE_CONTROLLER)]
	public class PlayerDamageController : AppDamageControllerBase
	{
		protected override void OnApplyDamage(cAppDamageInfoBase damageInfo)
		{
			if (_Chara.PlayerContext.CheckSafeFlag(PlayerDef.SAFE_FLAG.INVINCIBLE))
			{// 無敵
				return;
			}
			_HitType.SetValue(damageInfo.Type);
			switch (damageInfo.Type)
			{
				case HitDef.TYPE.SMALL:
					break;
				case HitDef.TYPE.SMASH:
					break;
			}
		}

		private void Start()
		{
			_Chara = GetComponent<PlayerCharacter>();
		}

		private void Update()
		{
#if UNITY_EDITOR
			if (GlobalService.Input.IsCommandSuccess(DEBUG_COMMAND_TYPE.SWITCH_DASHBOARD))
			{
				ApplyDamage(new cAppDamageInfoBase() { Type = HitDef.TYPE.SMASH });
			}
#endif

			switch (_HitType.Value)
			{
				case HitDef.TYPE.SMALL:
					_Chara.RequestSetAction(PlayerAction.Scroll.SetID.DamageSmall);
					break;
				case HitDef.TYPE.SMASH:
					_Chara.RequestSetAction(PlayerAction.Scroll.SetID.DamageSmash);
					break;
			}
			var playerAction = _Chara.ActionController.CurrentAction as PlayerAction.cPlayerActionBase;
			if (playerAction.CheckActionAttribute(PlayerDef.ACTION_ATTR.DAMAGE))
			{// ダメージアクション中
				if (_Chara.ActionController.IsActionEnd)
				{

				}
				else
				{
					if (_Chara.AnimationSequence.IsCancellable)
					{
						if (PlayerActionCondition.Judge(PlayerAction.Scroll.SetID.Move, _Chara))
						{
							_Chara.RequestSetAction(PlayerAction.Scroll.SetID.Move);
							return;
						}
					}
					_Chara.PlayerContext.OnSafeFlag(PlayerDef.SAFE_FLAG.DAMAGE_FLOW);
				}
			}
			_HitType.UpdateValue();
		}

		private PlayerCharacter _Chara = null;
		private cSafeValue<HitDef.TYPE> _HitType = new cSafeValue<HitDef.TYPE>();
	}
}