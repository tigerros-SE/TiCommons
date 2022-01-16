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
using System.Text.RegularExpressions;
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
	public static class TextSurfaceX {
		/// <summary>
		/// Inserts a new line into <paramref name="text"/> every <paramref name="maxChars"/> divided by <paramref name="fontSize"/>.
		/// </summary>
		/// <param name="text">The string to wrap.</param>
		/// <param name="fontSize">The size of the text. <paramref name="maxChars"/> is divided by this.</param>
		/// <param name="maxChars">The maximum amount of characters in between new lines.</param>
		/// <returns>The wrapped string.</returns>
		public static string Wrapped(string text, float fontSize = 1, int maxChars = 24) {
			maxChars = (int) (maxChars / fontSize);

			if (text.Length <= maxChars) return text;

			var lines = text.Split('\n');

			for (int i = 0; i < lines.Length; i++) {
				if (lines[i].Length > maxChars) {
					lines[i] = System.Text.RegularExpressions.Regex.Replace(lines[i], $".{{{maxChars}}}", "$0\n");
				}
			}

			return string.Join("\n", lines);
		}

		/// <summary>
		/// Calls the <see cref="Wrapped(string, int, int)"/> method with the <c>text</c> and <c>fontSize</c> being exracted from the <paramref name="surface"/>.
		/// </summary>
		/// <param name="surface">The surface from which to extract the <c>text</c> and the <c>fontSize</c>.</param>
		/// <param name="maxChars">The maximum amount of characters in between new lines.</param>
		/// <returns>The wrapped string.</returns>
		public static string Wrapped(this IMyTextSurface surface, int maxChars = 24)
			=> Wrapped(surface.GetText(), surface.FontSize, maxChars);

		/// <summary>
		/// Calls the <see cref="Wrapped(string, int, int)"/> method with the <c>text</c> and <c>fontSize</c> being exracted from the <paramref name="surface"/>.
		/// It displays the result in the surface.
		/// </summary>
		/// <param name="surface">The surface from which to extract the <c>fontSize</c> and display the result in.</param>
		/// <param name="text">The string to wrap.</param>
		/// <param name="append">Specifies whether this text should overwrite the surface, or keep every previous message there.</param>
		/// <param name="maxChars">The maximum amount of characters in between new lines.</param>
		/// <param name="maxLines">The maximum amount of lines. Once it is reached, the screen will be wiped.</param>
		public static void WriteLine(this IMyTextSurface surface, string text, bool append = true, int maxChars = 21, int maxLines = 11) {
			var lines = surface.GetText().Split('\n');

			if (lines.Length > maxLines) {
				surface.WriteText($"{lines.Last()}\n-> {Wrapped(text, surface.FontSize, maxChars)}");
			} else {
				surface.WriteText($"\n-> {Wrapped(text, surface.FontSize, maxChars)}", append);
			}
		}
	}
}
