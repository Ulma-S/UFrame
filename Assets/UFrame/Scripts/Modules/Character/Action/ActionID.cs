using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace uframe
{
	[StructLayout(LayoutKind.Auto)]
	public struct ACTION_ID : IEquatable<ACTION_ID>
	{
		public static readonly ACTION_ID Invalid = new ACTION_ID(-1, -1);

		public ACTION_ID(int category, int index)
		{
			Category = category;
			Index = index;
			Name = string.Empty;
		}

		public bool Equals(ACTION_ID actionID)
		{
			return Category == actionID.Category && Index == actionID.Index;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ACTION_ID))
			{
				return false;
			}
			var arg = (ACTION_ID)obj;
			return Category == arg.Category && Index == arg.Index;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return $"[ActionID](Category:{Category}, Index:{Index})";
		}

		public static bool operator ==(ACTION_ID lfs, ACTION_ID rfs)
		{
			return lfs.Equals(rfs);
		}

		public static bool operator !=(ACTION_ID lfs, ACTION_ID rfs)
		{
			return !lfs.Equals(rfs);
		}

		public int Category
		{
			get;
			private set;
		}

		public int Index
		{
			get;
			private set;
		}

#if UNITY_EDITOR
		public string Name
		{
			get;
			private set;
		}
#endif
	}
}