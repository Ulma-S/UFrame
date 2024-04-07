using System;
using System.Collections;

namespace uframe
{
	/// <summary>
	/// ■概要
	/// 立て続けないと勝手にオフになるフラグ
	/// オフにし忘れることがないので、安全に使えます
	/// 
	/// ■使い方
	/// サンプルコードを参考にしてください
	/// フラグを自動でオフにするために、UpdateFlagsメソッドを毎フレーム呼ぶ必要があります
	/// </summary>
	/// <typeparam name="TFlagType"></typeparam>
	public class cSafeFlag<TFlagType> where TFlagType : Enum
	{
		public cSafeFlag()
		{
			var length = Enum.GetValues(typeof(TFlagType)).Length;
			_Flags = new BitArray(length);
			_CheckFlags = new BitArray(length);
		}

		public void On(TFlagType flagType)
		{
			var flagTypeInt = flagType.GetHashCode();
			On(flagTypeInt);
		}

		public void On(int flagTypeInt)
		{
			_Flags[flagTypeInt] = true;
			_CheckFlags[flagTypeInt] = true;
		}

		public bool IsOn(TFlagType flagType)
		{
			var flagTypeInt = flagType.GetHashCode();
			return IsOn(flagTypeInt);
		}

		public bool IsOn(int flagTypeInt)
		{
			return _Flags[flagTypeInt];
		}

		public void UpdateFlags()
		{
			for (int i = 0, length = _Flags.Length; i < length; i++)
			{
				if (_CheckFlags[i])
				{
					_CheckFlags[i] = false;
				}
				else
				{
					_Flags[i] = false;
				}
			}
		}

		private BitArray _Flags = null;
		private BitArray _CheckFlags = null;
	}
}