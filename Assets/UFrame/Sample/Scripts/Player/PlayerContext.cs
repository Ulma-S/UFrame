using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class cPlayerContext
	{
		public class cMoveInfo
		{
			public void Clear()
			{
				MoveState = PlayerDef.MOVE_STATE.IDLE;
				PrevMoveState = PlayerDef.MOVE_STATE.IDLE;
				AirJumpCount = 0;
			}

			public PlayerDef.MOVE_STATE MoveState
			{
				get;
				set;
			} = PlayerDef.MOVE_STATE.IDLE;

			public PlayerDef.MOVE_STATE PrevMoveState
			{
				get;
				set;
			} = PlayerDef.MOVE_STATE.IDLE;

			public int AirJumpCount
			{
				get;
				set;
			} = 0;
		}

		public void OnSafeFlag(PlayerDef.SAFE_FLAG safeFlag)
		{
			_SafeFlag.On(safeFlag);
		}

		public bool CheckSafeFlag(PlayerDef.SAFE_FLAG safeFlag)
		{
			return _SafeFlag.IsOn(safeFlag);
		}

		public void UpdateSafeFlag()
		{
			_SafeFlag.UpdateFlags();
		}

		private cSafeFlag<PlayerDef.SAFE_FLAG> _SafeFlag = new cSafeFlag<PlayerDef.SAFE_FLAG>();

		public void Clear()
		{
			MoveInfo.Clear();
		}

		public cMoveInfo MoveInfo
		{
			get;
			private set;
		} = new cMoveInfo();
	}
}