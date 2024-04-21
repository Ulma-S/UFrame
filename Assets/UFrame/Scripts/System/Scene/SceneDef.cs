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
			SAMPLE_THIRD_PERSON_GAME,
		}

		/// <summary>
		/// �J�nScene��
		/// </summary>
		public static readonly string StartupSceneName = "Title";

		/// <summary>
		/// �풓Scene��
		/// </summary>
		public static readonly string ResidentSceneName = "ResidentSystem";

		/// <summary>
		/// ���[�h��ʌp������
		/// </summary>
		public static readonly float LoadingDurationSec = 1f;
	}
}