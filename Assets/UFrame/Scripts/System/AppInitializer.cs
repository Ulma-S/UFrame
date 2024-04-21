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
			GlobalService.Scene.RegisterScenePack(SceneDef.PACK_ID.SAMPLE_GAME, new string[] { "ScrollGame", "GameSetting" }, new cScrollGameSceneState());
			GlobalService.Scene.RegisterScenePack(SceneDef.PACK_ID.SAMPLE_THIRD_PERSON_GAME, new string[] { "ThirdPersonGame", "GameSetting" }, new cTpsGameState());
			GlobalService.Scene.RegisterScenePack(SceneDef.PACK_ID.SAMPLE_RESULT, new string[] { "Result" }, new cResultSceneState());

			RegisterInputCommand();

			var defaultCamera = cVirtualCamera.Create<cDefaultVirtualCamera>();
			GlobalService.Camera.RegisterCamera(CameraDef.ID.DEFAULT, defaultCamera);
			GlobalService.Camera.ChangeActiveCamera(CameraDef.ID.DEFAULT, immediately: true);
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
					return Input.GetAxisRaw("Horizontal") > 0.5f;
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
					return Input.GetAxisRaw("Horizontal") < -0.5f;
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.MOVE_LEFT, commandConditions);
			}

			{// ジャンプコマンドの登録
				var commandConditions = new Func<bool>[2];
				commandConditions[0] = () =>
				{
					return Input.GetKeyDown(KeyCode.Space);
				};
				commandConditions[1] = () =>
				{
					return Input.GetKeyDown(KeyCode.JoystickButton1);
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.JUMP, commandConditions);
			}

			{// 高速落下コマンドの登録
				var commandConditions = new Func<bool>[2];
				commandConditions[0] = () =>
				{
					return Input.GetKey(KeyCode.S);
				};
				commandConditions[1] = () =>
				{
					return Input.GetAxisRaw("Vertical") < -0.5f;
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.FALL, commandConditions);
			}

			{// カメラ移動（上）コマンドの登録
				var commandConditions = new Func<bool>[1];
				commandConditions[0] = () =>
				{
					return Input.GetKey(KeyCode.UpArrow);
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.MOVE_CAMERA_UP, commandConditions);
			}

			{// カメラ移動（下）コマンドの登録
				var commandConditions = new Func<bool>[1];
				commandConditions[0] = () =>
				{
					return Input.GetKey(KeyCode.DownArrow);
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.MOVE_CAMERA_DOWN, commandConditions);
			}

			{// カメラ移動（右）コマンドの登録
				var commandConditions = new Func<bool>[1];
				commandConditions[0] = () =>
				{
					return Input.GetKey(KeyCode.RightArrow);
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.MOVE_CAMERA_RIGHT, commandConditions);
			}

			{// カメラ移動（左）コマンドの登録
				var commandConditions = new Func<bool>[1];
				commandConditions[0] = () =>
				{
					return Input.GetKey(KeyCode.LeftArrow);
				};
				GlobalService.Input.RegisterGameCommand(GAME_COMMAND_TYPE.MOVE_CAMERA_LEFT, commandConditions);
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