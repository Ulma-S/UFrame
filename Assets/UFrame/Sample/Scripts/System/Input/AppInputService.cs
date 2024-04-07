using System;
using System.Collections.Generic;
using uframe;
using UnityEngine;

namespace app
{
	public class AppInputService : InputService
	{
		public void RegisterGameCommand(GAME_COMMAND_TYPE commandType, Func<bool>[] conditions)
		{
			_GameCommandHolder.RegisterCommand(commandType, conditions);
		}

		public void RegisterUICommand(UI_COMMAND_TYPE commandType, Func<bool>[] conditions)
		{
			_UICommandHolder.RegisterCommand(commandType, conditions);
		}

		public void RegisterDebugCommand(DEBUG_COMMAND_TYPE commandType, Func<bool>[] conditions)
		{
			_DebugCommandHolder.RegisterCommand(commandType, conditions);
		}

		/// <summary>
		/// �w�肵��Game Command�����͂��ꂽ��
		/// </summary>
		/// <param name="commandType"></param>
		/// <returns></returns>
		public bool IsCommandSuccess(GAME_COMMAND_TYPE commandType)
		{
			return _GameCommandHolder.IsCommandSuccess((int)commandType);
		}

		/// <summary>
		/// �w�肵��UI Command�����͂��ꂽ��
		/// </summary>
		/// <param name="commandType"></param>
		/// <returns></returns>
		public bool IsCommandSuccess(UI_COMMAND_TYPE commandType)
		{
			return _UICommandHolder.IsCommandSuccess((int)commandType);
		}

		/// <summary>
		/// �w�肵��Debug Command�����͂��ꂽ��
		/// </summary>
		/// <param name="commandType"></param>
		/// <returns></returns>
		public bool IsCommandSuccess(DEBUG_COMMAND_TYPE commandType)
		{
			return _DebugCommandHolder.IsCommandSuccess((int)commandType);
		}

		private cInputCommandHolder<GAME_COMMAND_TYPE> _GameCommandHolder = cInputCommandHolder<GAME_COMMAND_TYPE>.Create();
		private cInputCommandHolder<UI_COMMAND_TYPE> _UICommandHolder = cInputCommandHolder<UI_COMMAND_TYPE>.Create();
#if UNITY_EDITOR
		private cInputCommandHolder<DEBUG_COMMAND_TYPE> _DebugCommandHolder = cInputCommandHolder<DEBUG_COMMAND_TYPE>.Create();
#endif
	}
}