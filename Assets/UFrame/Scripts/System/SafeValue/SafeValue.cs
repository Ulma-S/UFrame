using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public class cSafeValue<T> : IEquatable<cSafeValue<T>>
	{
		public void SetValue(T value)
		{
			Value = value;
			_KeepValue = true;
		}

		public void UpdateValue()
		{
			if (_KeepValue)
			{
				_KeepValue = false;
			}
			else
			{
				Value = default(T);
			}
		}

		public override bool Equals(object obj)
		{
			var other = obj as cSafeValue<T>;
			if (other == null)
			{
				return false;
			}
			return EqualityComparer<T>.Default.Equals(Value, other.Value);
		}

		public override string ToString()
		{
			return $"[SafeValue]({typeof(T)}, {Value})";
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public bool Equals(cSafeValue<T> other)
		{
			return EqualityComparer<T>.Default.Equals(Value, other.Value);
		}

		public static bool operator ==(cSafeValue<T> lfs, T rfs)
		{
			return EqualityComparer<T>.Default.Equals(lfs.Value, rfs);
		}

		public static bool operator !=(cSafeValue<T> lfs, T rfs)
		{
			return !EqualityComparer<T>.Default.Equals(lfs.Value, rfs);
		}

		public T Value
		{
			get;
			private set;
		} = default(T);

		private bool _KeepValue = false;
	}
}