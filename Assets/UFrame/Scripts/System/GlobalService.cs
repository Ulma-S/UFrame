namespace uframe
{
	/// <summary>
	/// ■概要
	/// シングルトンクラスのアクセスを集約したクラス
	/// 適宜シングルトンクラスを登録してください
	/// 
	/// 
	/// ■使い方
	/// ▶シングルトンクラスの登録
	/// MonoBehaviour継承クラスの登録の際には、GlobalServiceElement<T>クラスを継承することを推奨します
	/// 
	/// ▶SceneServiceにアクセスする時
	/// GlobalService.Scene.~
	/// </summary>
	public static partial class GlobalService
	{
		/// <summary>
		/// シーン遷移
		/// </summary>
		public static SceneService Scene => SceneService.Instance;

		/// <summary>
		/// 画面フェード
		/// </summary>
		public static FadeService Fade => FadeService.Instance;

		/// <summary>
		/// 一時停止
		/// </summary>
		public static PauseService Pause => PauseService.Instance;
	}
}