using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
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
	public static class TerminalBlockX {
		public static void SetValues<TValue, TBlock>(this TBlock block, Dictionary<string, TValue> dict) where TBlock : IMyTerminalBlock {
			foreach (KeyValuePair<string, TValue> pair in dict) {
				block.SetValue(pair.Key, pair.Value);
			}
		}

		public static void SetValueRecordPrevious<TValue, TBlock>(this TBlock block, string propertyId, TValue value, Dictionary<string, TValue> dict) where TBlock : IMyTerminalBlock {
			dict[propertyId] = block.GetValue<TValue>(propertyId);
			block.SetValue(propertyId, value);
		}
	}
}
