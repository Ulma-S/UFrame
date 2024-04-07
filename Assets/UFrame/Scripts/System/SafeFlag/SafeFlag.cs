using System;
using System.Collections;

namespace uframe
{
	/// <summary>
	/// ���T�v
	/// ���đ����Ȃ��Ə���ɃI�t�ɂȂ�t���O
	/// �I�t�ɂ��Y��邱�Ƃ��Ȃ��̂ŁA���S�Ɏg���܂�
	/// 
	/// ���g����
	/// �T���v���R�[�h���Q�l�ɂ��Ă�������
	/// �t���O�������ŃI�t�ɂ��邽�߂ɁAUpdateFlags���\�b�h�𖈃t���[���ĂԕK�v������܂�
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