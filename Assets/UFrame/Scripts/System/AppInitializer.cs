using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uframe;

namespace app
{
	[DefaultExecutionOrder((int)EXECUTION_ORDER.APP_INITIALIZER)]
	public class AppInitializer : GlobalServiceElement<AppInitializer>
	{
		protected override void OnAwake()
		{
			GlobalService.Scene.RegisterScenePack(SceneDef.PACK_ID.LOADING, new string[] { }, new cLoadingSceneState());
			GlobalService.Scene.RegisterScenePack(SceneDef.PACK_ID.SAMPLE_TITLE, new string[] { "Title" }, new cTitleSceneState());
			GlobalService.Scene.RegisterScenePack(SceneDef.PACK_ID.SAMPLE_GAME, new string[] { "Game", "GameSetting" }, new cGameSceneState());
			GlobalService.Scene.RegisterScenePack(SceneDef.PACK_ID.SAMPLE_RESULT, new string[] { "Result" }, new cResultSceneState());

			RegisterInputCommand();
#if UNITY_EDITOR
			RegisterDebugCommand();
#endif
		}

		private void RegisterInputCommand()
		{
			{// 右移動コマンドの登録
				var commandConditions = new Func<bool>[2];
				commandConditions[0] = () =>
				{
					return Input.GetKey(KeyCode.D);
				};
				commandConditions[1] = () =>
				{
					return Input.GetKey(KeyCode.RightArrow);
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.MOVE_RIGHT, commandConditions);
			}

			{// 左移動コマンドの登録
				var commandConditions = new Func<bool>[2];
				commandConditions[0] = () =>
				{
					return Input.GetKey(KeyCode.A);
				};
				commandConditions[1] = () =>
				{
					return Input.GetKey(KeyCode.LeftArrow);
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.MOVE_LEFT, commandConditions);
			}

			{// ジャンプコマンドの登録
				var commandConditions = new Func<bool>[1];
				commandConditions[0] = () =>
				{
					return Input.GetKeyDown(KeyCode.Space);
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.JUMP, commandConditions);
			}

			{// 決定コマンドの登録
				var commandConditions = new Func<bool>[1];
				commandConditions[0] = () =>
				{
					return Input.GetKeyDown(KeyCode.Return);
				};
				GlobalService.Input.RegisterUICommand(UI_COMMAND_TYPE.DECIDE, commandConditions);
			}

			{// キャンセルコマンドの登録
				var commandConditions = new Func<bool>[1];
				commandConditions[0] = () =>
				{
					return Input.GetKeyDown(KeyCode.Escape);
				};
				GlobalService.Input.RegisterUICommand(UI_COMMAND_TYPE.CANCEL, commandConditions);
			}
		}

		private void RegisterDebugCommand()
		{
			{// DebugDashboardを開く/閉じる	
				var commandConditions = new Func<bool>[1];
				commandConditions[0] = () =>
				{
					var isControl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
					return isControl && Input.GetKeyDown(KeyCode.Q);
				};
				GlobalService.Input.RegisterDebugCommand(DEBUG_COMMAND_TYPE.SWITCH_DASHBOARD, commandConditions);
			}
		}
	}
}