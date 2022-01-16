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
	public static class EnumX {
		/// <summary>
		/// Converts an enum value to a string.
		/// </summary>
		/// <param name="enum">The enum to convert.</param>
		/// <returns>The converted string.</returns>
		public static string ConvertToString(this Enum @enum)
			=> Enum.GetName(@enum.GetType(), @enum);
	}
}
