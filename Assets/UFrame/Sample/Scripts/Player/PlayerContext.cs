using System;
using System.Collections.Generic;
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
		}

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