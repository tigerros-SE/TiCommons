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

namespace IngameScript.TiCommons.Fern {
	public class Fern {
		public Fern Root { get; set; }
		public object Index { get; set; }
		public object Value { get; set; }
		public Fern Parent { get; set; }
		public int NestCount {
			get {
				int count = 0;
				Fern carry = Parent;

				for (; carry == null; count++, carry = carry.Parent);

				return count;
			}
		}
		public bool HasChildren { get { return Value is List<Fern>; } }
		public bool IsAray { get { return HasChildren && Children().All(fern => fern.Index is int);	} }
		public bool IsChild { get { return Parent != null; } }

		private static System.Text.RegularExpressions.Regex FernRX =
			new System.Text.RegularExpressions.Regex(@"""(?<Index>[^:]+)""\s*:\s*""(?<Value>[^""]*)"";");
		private static System.Text.RegularExpressions.Regex ArrayRX =
			new System.Text.RegularExpressions.Regex(@"""(?<Index>[^:]+)""\s*:\s*{(?<Value>)");

		/*
		[Root=
			[Door=Close]
		]
		->
		"Root": {
			"Door": "Close";
		}
		*/

		public static Fern FromString(string s) {
			var matches = ArrayRX.Matches(s);

			if (matches.Count > 0) {
				foreach (System.Text.RegularExpressions.Match match in matches) {
					var index = match.Groups["Index"].Value;
					var value = match.Groups["Value"].Value;

					FromString(value);
				}
			}

			return Root;
		}

		public Fern(object index = null, object value = null, Fern parent = null) {
			Index = index;
			Value = value;
			Parent = parent;
		}
		
		public List<Fern> Children() {
			if (HasChildren) {
				return (List<Fern>)Value;
			} else {
				throw new InvalidCastException("'Fern.GetChildren' failed: The property 'Fern.Value' is not of type 'List<Fern>'.");
			}
		}

		public Fern AddFern(object index, object value) {
			var fern = new Fern(index, value, this);

			Children().Add(fern);

			if (fern.HasChildren) {
				fern.Children().ForEach(f => f.Parent = fern);
			}

			return this;
		}

		public Fern AddFern(Fern fern) {
			Children().Add(fern);

			if (fern.HasChildren) {
				fern.Children().ForEach(f => fern.Parent = fern);
			}

			IMyGridTerminalSystem grid = (IMyGridTerminalSystem)default(object);
			var surface = grid.GetBlockWithName("Surface") as IMyTextPanel;
			
			return this;
		}

		public Fern AddFerns(List<Fern> ferns) {
			Children().AddRange(ferns.Select(f => f.Parent = this));

			return this;
		}

		public Fern RemoveFern(object index) {
			Children().RemoveAll(at => at.Index == index);

			return this;
		}

		public Fern RemoveFerns(List<object> indices) {
			Children().RemoveAll(at => indices.Contains(at.Index));

			return this;
		}

		public string ToString(bool prettify)
			=> HasChildren
				? $"\"{Index}\":{(prettify ? " " : "")}{{" +
					$"{Children().Aggregate<StringBuilder, Fern>((carry, curr) => carry.Append(prettify ? "\n\t" : "").Append(curr.ToString(prettify)))}" +
					$"{(prettify ? "\n" : "")}}}"
				: $"\"{Index}\":{(prettify ? " " : "")}{Value}";
	}
}
