using System;
using System.Collections.Generic;
using UnityEngine;

namespace uframe
{
	public static partial class LayerDef
	{
		public static readonly string Terrain = "Terrain";

		public static readonly int TerrainLayer = LayerMask.NameToLayer(Terrain);

		public static readonly int TerrainMask = LayerMask.GetMask(Terrain);
	}
}