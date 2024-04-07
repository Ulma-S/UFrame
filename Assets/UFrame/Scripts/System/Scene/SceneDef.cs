namespace uframe
{
	public static partial class SceneDef
	{
		public enum PACK_ID
		{
			INVALID,
			SPLASH_SCREEN,
			LOADING,
			SAMPLE_TITLE,
			SAMPLE_GAME,
			SAMPLE_RESULT,
		}

		/// <summary>
		/// 開始Scene名
		/// </summary>
		public static readonly string StartupSceneName = "Title";

		/// <summary>
		/// 常駐Scene名
		/// </summary>
		public static readonly string ResidentSceneName = "ResidentSystem";

		/// <summary>
		/// ロード画面継続時間
		/// </summary>
		public static readonly float LoadingDurationSec = 1f;
	}
}