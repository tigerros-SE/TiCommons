using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
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

namespace IngameScript.TiCommons.Util {
	/// <summary>
	/// An abstract class that allows a property to only be set a specific number of times.
	/// 
	/// <para>
	/// <example>
	/// To make your class, use this code:
	/// <code>
	/// public class OneSetProperty&lt;T&gt; : <see cref="XSetPropertyBase{T}"/> {
	///		public override int? SetsAllowed { get; set; } = 1;
	///		
	///		public OneSetProperty() { }
	///		public OneSetProperty(T value) { Value = value; }
	///		public OneSetProperty(OneSetProperty&lt;T&gt; oneSetProperty) { Value = oneSetProperty.Value; }
	/// }
	/// </code>
	/// When a property is of this class, it can only be set once.
	/// </example>
	/// </para>
	/// </summary>
	/// <typeparam name="T">The type of the property.</typeparam>
	public abstract class XSetPropertyBase<T> {
		/// <summary>
		/// The amount of times that <see cref="Value"/> can be set.
		/// </summary>
		public abstract int? SetsAllowed { get; set; }
		/// <summary>
		/// The message of the exception that will be thrown once the <see cref="Value"/> has been set more times than <see cref="SetsAllowed"/>.
		/// </summary>
		public virtual string ExceptionMessage { get; set; }
		private int SetCount { get; set; } = 0;
		private T _value;
		public T Value {
			get { return _value; }
			set {
				if (SetsAllowed == null) {
					throw new Exception($"The property {SetsAllowed} 'SetsAllowed' must be set.");
				}

				if (SetCount >= SetsAllowed.Value) {
					throw new Exception(ExceptionMessage ?? $"You can only set this property {SetsAllowed.Value} times.");
				} else {
					_value = value;
					SetCount++;
				}
			}
		}
	}
}
