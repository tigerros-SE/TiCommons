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
	public static class CommandLineX {
		/// <summary>
		/// Returns a list of all detected arguments
		/// </summary>
		public static IEnumerable<string> Arguments(this MyCommandLine cl) =>
			from v in Enumerable.Range(0, cl.ArgumentCount) select cl.Argument(v);

		/// <summary>
		/// Contains the valid argument modifiers. These modifiers will be handled in the HandleArguments method.
		/// To add a modifier to an argument, add the pipe '|' character and then the modifier.
		/// 
		/// <para>
		/// Valid modifiers:
		/// <para>
		/// <c>->*</c> : The next argument is of any form. Eg. 'open "DoorName"', where 'open' has this modifier.
		/// </para>
		/// </para>
		/// </summary>
		public static Dictionary<string, bool> ArgumentModifiers = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase) {
			["->*"] = false
		};

		/// <summary>
		/// Goes through the <c>CommandLine</c> switches and <c>validSwitches</c> and checks if the <c>CommandLine</c> switches are valid.
		/// 
		/// <para>
		/// If they are, their value will become 'true' in <c>validSwitches</c>.
		/// </para>
		/// 
		/// <para>
		/// If they are not, a warning message will print to the <c>IMyTextSurface</c> in the programmable block.
		/// </para>
		/// </summary>
		public static void HandleSwitches(this MyCommandLine cl, IMyProgrammableBlock pb, Dictionary<string, bool> validSwitches) {
			foreach (var @switch in cl.Switches) {
				if (validSwitches.ContainsKey(@switch)) {
					validSwitches[@switch] = true;
				} else {
					pb.WriteLineAndData($"WARNING: UNKNOWN SWITCH: \"{@switch}\". AVAILABLE SWITCHES: [{string.Join(", ", validSwitches.Keys)}]");
				}
			}
		}

		/// <summary>
		/// Goes through the <c>CommandLine</c> arguments and <c>validArguments</c> and checks if the <c>CommandLine</c> arguments are valid.
		/// 
		/// <para>
		/// If they are, the action associated with that argument in <c>validArguments</c> will be called.
		/// </para>
		/// 
		/// <para>
		/// If they are not, a warning message will print to the <c>IMyTextSurface</c> in the programmable block.
		/// </para>
		/// </summary>
		public static void HandleArguments(this MyCommandLine cl, IMyProgrammableBlock pb, Dictionary<string, Action> validArguments) {
			ArgumentModifiers.Values.Select(_ => false);
			var validArgumentsNoModifiers = validArguments.Keys.Select(k => k.Split('|')[0]);

			for (int ai = 0; ai < cl.ArgumentCount; ai++) {
				if (ArgumentModifiers["->*"]) {
					ArgumentModifiers["->*"] = false;
					continue;
				}

				var argument = cl.Argument(ai);

				if (validArgumentsNoModifiers.Contains(argument)) {
					var match = validArguments.First(pair => pair.Key.StartsWith(argument));
					match.Value();
					var modifiers = match.Key.Split('|').Skip(1);

					foreach (var modifier in modifiers) {
						if (ArgumentModifiers.ContainsKey(modifier)) {
							ArgumentModifiers[modifier] = true;
						}
					}
				} else {
					pb.WriteLineAndData($"WARNING: UNKNOWN ARGUMENT: \"{argument}\". AVAILABLE ARGUMENTS: [{string.Join(", ", validArgumentsNoModifiers)}]");
				}			
			}
		}
	}
}
