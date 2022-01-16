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
	public static class ProgrammableBlockX {
		/// <summary>
		/// Writes a new line in the programmable block surface and the custom data starting with "<c>-> </c>".
		/// </summary>
		/// <param name="pb">The <see cref="IMyProgrammableBlock"/> to write to.</param>
		/// <param name="text">The text to write.</param>
		/// <param name="append">Specifies whether this text should overwrite the surface, or keep every previous message there.</param>
		/// <param name="maxChars">The maximum amount of characters in between new lines.</param>
		/// <param name="maxLines">The maximum amount of lines. Once it is reached, the screen will be wiped.</param>
		public static void WriteLineAndData(this IMyProgrammableBlock pb, string text, bool append = true, int maxChars = 21, int maxLines = 11) {
			if (append == false) {
				pb.CustomData = $"-> {text}";
			} else {
				pb.CustomData += $"\n-> {text}";
			}

			var surface = pb.GetSurface(0);

			surface.WriteLine(text, append, maxChars, maxLines);

			if (text.Contains("WARNING")) {
				surface.BackgroundColor = Color.Orange;
				surface.FontColor = Color.Black;
			}
		}
	}
}
