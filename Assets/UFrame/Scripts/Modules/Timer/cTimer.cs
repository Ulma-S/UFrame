using System;

namespace uframe
{
	/// <summary>
	/// ���T�v
	/// �ėp�̃^�C�}�[
	/// 
	/// 
	/// ���g����
	/// �e�v���p�e�B�̎g�����͂��ꂼ���summary���Q�l�ɂ��Ă�������
	/// 
	/// <��>
	/// Max10�b�̃^�C�}�[�𐶐�
	/// var timer = new cTimer(10f);
	/// 
	/// �^�C�}�[�̍X�V
	/// timer.Update(Time.DeltaTime);
	/// </summary>
	[Serializable]
	public class cTimer
	{
		public cTimer() { }

		public cTimer(float limit, bool enabled = true)
		{
			_Limit = limit;
			_Enabled = enabled;
		}

		public void Update(float deltaTime)
		{
			if (!_Enabled)
			{
				return;
			}
			_Timer += deltaTime;
			if (_Timer >= _Limit)
			{
				if (_Repeat)
				{
					_Timer = 0f;
				}
				else
				{
					_Timer = _Limit;
				}
			}
		}

		public void Reset()
		{
			Enabled = true;
			_Timer = 0f;
		}

		public override string ToString()
		{
			return $"[Timer] {_Timer} / {_Limit}, Enabled: {_Enabled}, Repeat: {_Repeat}, TimeOut: {IsTimeOut}";
		}

		public float Timer => _Timer;

		public float Limit
		{
			get => _Limit;
			set => _Limit = value;
		}

		public bool Enabled
		{
			get => _Enabled;
			set => _Enabled = value;
		}

		public bool Repeat
		{
			get => _Repeat;
			set => _Repeat = value;
		}

		private float _Timer = 0f;

		private float _Limit = 0f;

		private bool _Enabled = true;

		private bool _Repeat = false;

		public bool IsTimeOut => Timer >= Limit;
	}
}