namespace uframe
{
	public enum EXECUTION_ORDER : int
	{
		APP_INITIALIZER,
		SCENE_INITIALIZER,
		SYSTEM,
		DEFAULT = 0,
		PLAYER_DAMAGE_CONTROLLER,
		PLAYER_CONTROLLER,
		PLAYER_CHARACTER,
	}
}