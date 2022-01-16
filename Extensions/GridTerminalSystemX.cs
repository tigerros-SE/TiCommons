using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript.TiCommons.Extensions {
	public static class GridTerminalSystemX {
		/// <summary>
		/// Creates a new list and fills it with blocks reachable by this grid terminal system.
		/// This means all blocks on the same grid, or connected via rotors, pistons or connectors.
		/// </summary>
		public static List<T> GetBlocksOfType<T>(this IMyGridTerminalSystem gts) where T : class {
			var list = new List<T>();
			gts.GetBlocksOfType(list);
			return list;
		}
	}
}
