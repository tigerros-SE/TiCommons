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

namespace IngameScript {
	public static class LinqX {
		public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T item) {
			foreach (var curr in enumerable) {
				yield return curr;
			}

			yield return item;
		}

		public static IList<T> Append<T>(this IList<T> list, T item) {
			var @new = list;
			@new.Add(item);
			return @new;
		}

		public static IList<T> AppendRange<T>(this IList<T> list, IList<T> second) {
			var @new = list;

			foreach (var secondItem in second) {
				@new.Add(secondItem);
			}

			return @new;
		}

		public static TResult Aggregate<TResult, TSource>(this IEnumerable<TSource> enumerable, Func<TResult, TSource, TResult> func) {
			TResult carry = default(TResult);

			foreach (var current in enumerable) {
				carry = func(carry, current);
			}

			return carry;
		}
	}
}
