using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace uframe
{
	/// <summary>
	/// ■概要
	/// ※以下ゲームの意味としてのシーンは"シーン"、Unityの機能としてのシーンは"Scene"と呼称します
	/// 
	/// Sceneをゲームのシーン遷移ごとにまとめてロード、アンロードする
	/// Unityでは複数Sceneを同時に読み込めるので、常駐SceneやUI Sceneなどで分割することを想定しています
	/// 本システムでは、複数Scene（名）をまとめたものをScenePackと呼称します
	/// 
	/// 
	/// ■使い方
	/// ▶シーン毎にScenePackを登録する
	/// ※常駐SceneはScenePackに登録不要です
	/// 【例】ゲーム画面として"Game"と"GameSetting" Sceneを登録する時
	/// 
	/// GlobalService.Scene.RegisterScenePack(nameof(ScenePackDef.SAMPLE_GAME), new string[] { "Game", "GameSetting" });
	/// 
	/// 第一引数が画面のキーです
	/// サンプルでは変数名をキーにしていますが、識別できるなら他の方法でも問題ありません
	/// 
	/// サンプルではAppInitializer.csで行っています
	/// 
	/// 
	/// ▶シーンに対応したScenePackを読み込む
	/// 【例】上の例で登録したゲーム画面を読み込む
	/// 
	/// GlobalService.Scene.LoadScenePack(nameof(ScenePackDef.SAMPLE_GAME));
	/// 
	/// ロード終了を待機して続きの処理を行いたい場合はawaitで待機する必要があります
	/// 以下の例のようにasync, awaitキーワードを付けてください
	/// 
	/// private async void Hoge()
	/// {
	///		await GlobalService.Scene.LoadScenePack(nameof(ScenePackDef.SAMPLE_GAME));
	///		// 以下はロード終了後に実行されます
	///		// ~
	/// }
	/// 
	/// 
	///【例】上の例で登録したゲーム画面をフェードイン、アウトの演出付きで読み込む
	/// 
	/// GlobalService.Scene.LoadScenePackWithFade(nameof(ScenePackDef.SAMPLE_GAME));
	/// 
	/// フェードインを待機して続きの処理を行う場合はawaitで待機する必要があります
	/// 上の例を参考にasync, awaitキーワードを付けてください
	/// </summary>
	[DefaultExecutionOrder((int)EXECUTION_ORDER.SYSTEM)]
	public class SceneService : GlobalServiceElement<SceneService>
	{
		/// <summary>
		/// 指定したScenePackをロードする
		/// 待機可能
		/// </summary>
		/// <param name="scenePackID"></param>
		/// <returns></returns>
		public async UniTask LoadScenePackAsync(SceneDef.PACK_ID scenePackID)
		{
			IsSceneTransition = true;
			await LoadScenePackAsyncCore(scenePackID);
			IsSceneTransition = false;
		}

		/// <summary>
		/// 1つ前のScenePackをロードする
		/// 待機可能
		/// </summary>
		/// <returns></returns>
		public async UniTask LoadPrevScenePackAsync()
		{
			if (_ScenePackHistory.Count < 2)
			{
				return;
			}
			IsSceneTransition = true;
			_ScenePackHistory.Pop();
			var prevScenePackID = _ScenePackHistory.Pop();
			if (_ScenePackHolder.TryGetValue(prevScenePackID, out var scenePack))
			{
				if (_CurrentSceneState != null)
				{
					_CurrentSceneState.Exit();
				}
				_CurrentSceneState = _SceneStateHolder[(int)SceneDef.PACK_ID.LOADING];
				await UnloadScenePack(scenePack);
				await LoadScenePack(scenePack);
				CurrentScenePackID= (SceneDef.PACK_ID)prevScenePackID;
				_CurrentSceneState = _SceneStateHolder[prevScenePackID];
				_CurrentSceneState.Enter();
				_ScenePackHistory.Push(prevScenePackID);
			}
			IsSceneTransition = false;
		}

		/// <summary>
		/// フェードアウト、フェードインに挟んで、指定したScenePackをロードする
		/// 待機不可
		/// </summary>
		/// <param name="scenePackID"></param>
		/// <param name="fadeDuration"></param>
		public async void LoadScenePackWithFade(SceneDef.PACK_ID scenePackID, float fadeDuration = 1f, Action onLoadEnd = null)
		{
			await LoadScenePackWithFadeAsync(scenePackID, fadeDuration);
			onLoadEnd?.Invoke();
		}

		/// <summary>
		/// フェードアウト、フェードインに挟んで、指定したScenePackをロードする
		/// 待機可能
		/// </summary>
		/// <param name="fadeDuration"></param>
		/// <returns></returns>
		public async UniTask LoadScenePackWithFadeAsync(SceneDef.PACK_ID scenePackID, float fadeDuration = 1f)
		{
			IsSceneTransition = true;
			await GlobalService.Fade.FadeOut(fadeDuration).SuppressCancellationThrow();
			var loadSceneTask = LoadScenePackAsyncCore(scenePackID);
			var waitDurationTask = UniTask.Delay(TimeSpan.FromSeconds(SceneDef.LoadingDurationSec), cancellationToken: this.GetCancellationTokenOnDestroy());
			var tasks = new UniTask[]
			{
				loadSceneTask, waitDurationTask,
			};
			await UniTask.WhenAll(tasks).SuppressCancellationThrow();
			await GlobalService.Fade.FadeIn(fadeDuration).SuppressCancellationThrow();
			IsSceneTransition = false;
		}

		/// <summary>
		/// シーンごとにScenePackを登録する
		/// </summary>
		/// <param name="scenePackID"></param>
		/// <param name="scenes"></param>
		public void RegisterScenePack(SceneDef.PACK_ID scenePackID, string[] scenes, cSceneStateBase sceneState)
		{
			if (_ScenePackHolder.ContainsKey((int)scenePackID))
			{
				LogService.Error(this, $"{scenePackID}は登録済みです");
				return;
			}
			_ScenePackHolder.Add((int)scenePackID, scenes);
			_SceneStateHolder.Add((int)scenePackID, sceneState);
		}

		/// <summary>
		/// 常駐シーンを登録する
		/// </summary>
		/// <param name="sceneNames"></param>
		public void RegisterResidentScenes(string[] sceneNames)
		{
			_ResidentSceneNames = sceneNames;
		}

		public void Setup(SceneDef.PACK_ID scenePackID)
		{
			if (CurrentScenePackID != SceneDef.PACK_ID.INVALID)
			{// 最初のシーンは設定済み
				return;
			}
			_CurrentSceneState = _SceneStateHolder[(int)scenePackID];
			_CurrentSceneState.Enter();
		}

		#region 非公開メソッド
		private async UniTask LoadScenePack(string[] scenePack)
		{
			var loadedSceneCount = SceneManager.sceneCount;
			var loadedSceneNames = new string[loadedSceneCount];
			for (int i = 0; i < loadedSceneCount; i++)
			{
				loadedSceneNames[i] = SceneManager.GetSceneAt(i).name;
			}
			var scenePackCount = scenePack.Length;
			var loadSceneNames = new string[scenePackCount];
			int loadSceneCount = 0;
			for (int i = 0; i < scenePackCount; i++)
			{// ロード対象のシーンを収集
				var sceneName = scenePack[i];
				if (loadedSceneNames.Contains(sceneName))
				{// ロード済み
					continue;
				}
				loadSceneNames[loadSceneCount] = sceneName;
				loadSceneCount++;
			}
			var loadTasks = new UniTask[loadSceneCount];
			for (int i = 0; i < loadSceneCount; i++)
			{// ロード処理
				var sceneName = loadSceneNames[i];
				loadTasks[i] = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
			}
			await loadTasks;
			for (int i = 0, sceneCount = SceneManager.sceneCount; i < sceneCount; i++)
			{
				var scene = SceneManager.GetSceneAt(i);
				if (scene.name == scenePack[0])
				{// ScenePackの0番目をActiveSceneとして扱う
					SceneManager.SetActiveScene(scene);
				}
			}
		}

		private async UniTask UnloadScenePack(string[] scenePack)
		{
			var loadedSceneCount = SceneManager.sceneCount;
			var unloadSceneNames = new string[loadedSceneCount];
			int unloadSceneCount = 0;
			for (int i = 0; i < loadedSceneCount; i++)
			{// アンロード対象のシーンを収集
				var scene = SceneManager.GetSceneAt(i);
				if (scene.name == SceneDef.ResidentSceneName)
				{
					continue;
				}
				if (scenePack.Contains(scene.name))
				{
					continue;
				}
				unloadSceneNames[unloadSceneCount] = scene.name;
				unloadSceneCount++;
			}
			var unloadTasks = new UniTask[unloadSceneCount];
			for (int i = 0; i < unloadSceneCount; i++)
			{// アンロード処理
				unloadTasks[i] = SceneManager.UnloadSceneAsync(unloadSceneNames[i]).ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
			}
			// メモリ負荷対策のためにload前にunloadを待機
			await unloadTasks;
		}

		private async UniTask LoadScenePackAsyncCore(SceneDef.PACK_ID scenePackID)
		{
			if (_ScenePackHolder.TryGetValue((int)scenePackID, out var scenePack))
			{
				if (_CurrentSceneState != null)
				{
					_CurrentSceneState.Exit();
				}
				_CurrentSceneState = _SceneStateHolder[(int)SceneDef.PACK_ID.LOADING];
				await UnloadScenePack(scenePack);
				await LoadScenePack(scenePack);
				CurrentScenePackID = scenePackID;
				_CurrentSceneState = _SceneStateHolder[(int)scenePackID];
				_CurrentSceneState.Enter();
				_ScenePackHistory.Push((int)scenePackID);
			}
		}

		private void Start()
		{
			FindAnyObjectByType<SceneInitializer>().Initialize();
		}

		private void Update()
		{
			_CurrentSceneState.Update();
			if (!IsSceneTransition)
			{
				if (_CurrentSceneState.CheckSceneTransition(out var nextScenePackID))
				{
					LoadScenePackWithFade(nextScenePackID);
				}
			}
		}
		#endregion

		public SceneDef.PACK_ID CurrentScenePackID
		{
			get;
			private set;
		} = SceneDef.PACK_ID.INVALID;

		/// <summary>
		/// シーン遷移処理中か
		/// </summary>
		public bool IsSceneTransition
		{
			get;
			private set;
		} = false;

		private Dictionary<int, string[]> _ScenePackHolder = new Dictionary<int, string[]>();
		private Dictionary<int, cSceneStateBase> _SceneStateHolder = new Dictionary<int, cSceneStateBase>();
		private string[] _ResidentSceneNames = null;
		private Stack<int> _ScenePackHistory = new Stack<int>();
		private cSceneStateBase _CurrentSceneState = null;
	}
}