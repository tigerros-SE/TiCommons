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

namespace IngameScript.TiCommons.Util.Atlang {
	public class FernBuilder {
		public Fern Root { get; set; } = new Fern("Root", new List<Fern>());
		private static System.Text.RegularExpressions.Regex AtRx =
			new System.Text.RegularExpressions.Regex($@"\[([a-zA-Z0-9_\s]+)\s*\=\s*([a-zA-Z0-9_\s]+)\]");

		public FernBuilder(string s = "") {
			
		}

		public FernBuilder(Fern attribute) {
			Root.AddFern(attribute);
		}

		public FernBuilder(List<Fern> attributes) {

		}

		public static string AddFern(string s, string name, object value) {
			return $"[{name}:{value}]";
		}

		public FernBuilder AddFern(string name, object value) {
			Root.Children().Add(new Fern(name, value));
			return this;
		}

		public FernBuilder AddFerns(List<Fern> attributes) {
			Root.Children().AddRange(attributes);
			return this;
		}

		public FernBuilder RemoveFern(string name) {
			Root.Children().RemoveAll(at => at.Name == name);
			return this;
		}

		public FernBuilder RemoveFerns(List<string> names) {
			Root.Children().RemoveAll(at => names.Contains(at.Index));
			return this;
		}

		public static List<Fern> FromString(string st) {
			var attributes = new List<Fern>();
			var matches = AtRx.Matches(st);

			if (matches.Count > 0) {
				foreach (System.Text.RegularExpressions.Match match in matches) {
					var groups = match.Groups;

					attributes.Add(new Fern(groups[1].Value, groups[2].Value));
				}
			}

			return attributes;
		}

		public override string ToString() {
			var st = new StringBuilder();

			foreach (var fern in Root.Children()) {
				st.Append(AddFern(st.ToString(), fern.Index, fern.Value));
			}

			return st.ToString();
		}
	}
}
