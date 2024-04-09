using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	[DefaultExecutionOrder((int)EXECUTION_ORDER.SYSTEM)]
	public class GameFlowManager : GlobalServiceElement<GameFlowManager>
	{
		private class cGameFlowProcessor
		{
			public static cGameFlowProcessor Create()
			{
				return new cGameFlowProcessor();
			}

			public void Setup(cGameFlowBase[] gameFlows)
			{
				_Timeline = gameFlows;
				_FlowIndex = 0;
				IsRunning = true;
				gameFlows[0].Enter();
			}

			public bool Update()
			{
				if (!IsRunning)
				{
					return false;
				}
				if (_FlowIndex >= _Timeline.Length)
				{
					IsRunning = false;
					return true;
				}
				var result = _Timeline[_FlowIndex].Update();
				if (result)
				{
					_FlowIndex++;
					if (_FlowIndex < _Timeline.Length)
					{
						_Timeline[_FlowIndex].Enter();
					}
				}
				return false;
			}

			private cGameFlowProcessor() { }

			public bool IsRunning
			{
				get;
				private set;
			} = false;

			private cGameFlowBase[] _Timeline = null;
			private int _FlowIndex = 0;
		}

		public void NotifyGoal(GoalBeacon goalBeacon)
		{
			if (_GoalRequested)
			{
				return;
			}
			_GoalRequested = true;
			CurrentGoal = goalBeacon;
		}

		protected override void OnStart()
		{
			_GameClearFlowHolder.Add((int)GameFlowDef.GAME_CLEAR_FLOW.WARP_PLAYER, cGameFlowBase.Create<GameClearFlow.cWarpPlayer>());
			_GameClearFlowHolder.Add((int)GameFlowDef.GAME_CLEAR_FLOW.ZOOM_CAMERA, cGameFlowBase.Create<GameClearFlow.cZoomCamera>());
			_GameClearFlowHolder.Add((int)GameFlowDef.GAME_CLEAR_FLOW.VICTORY_PERFORMANCE, cGameFlowBase.Create<GameClearFlow.cVictoryPerformance>());
			_GameClearFlowHolder.Add((int)GameFlowDef.GAME_CLEAR_FLOW.END, cGameFlowBase.Create<GameClearFlow.cEnd>());

			_GameClearFlowTimeline = new cGameFlowBase[4];
			_GameClearFlowTimeline[0] = _GameClearFlowHolder[(int)GameFlowDef.GAME_CLEAR_FLOW.WARP_PLAYER];
			_GameClearFlowTimeline[1] = _GameClearFlowHolder[(int)GameFlowDef.GAME_CLEAR_FLOW.ZOOM_CAMERA];
			_GameClearFlowTimeline[2] = _GameClearFlowHolder[(int)GameFlowDef.GAME_CLEAR_FLOW.VICTORY_PERFORMANCE];
			_GameClearFlowTimeline[3] = _GameClearFlowHolder[(int)GameFlowDef.GAME_CLEAR_FLOW.END];
		}

		protected override void OnUpdate()
		{
			if (_GoalRequested && !_ClearProcessRequested)
			{
				_Processor.Setup(_GameClearFlowTimeline);
				_ClearProcessRequested = true;
			}
			UpdateGameClearFlow();
		}

		private void UpdateGameClearFlow()
		{
			_Processor.Update();
		}

		public GoalBeacon CurrentGoal
		{
			get;
			private set;
		} = null;

		private cGameFlowProcessor _Processor = cGameFlowProcessor.Create();
		private Dictionary<int, cGameFlowBase> _GameClearFlowHolder = new Dictionary<int, cGameFlowBase>();
		private cGameFlowBase[] _GameClearFlowTimeline = null;
		private bool _GoalRequested = false;
		private bool _ClearProcessRequested = false;
	}
}