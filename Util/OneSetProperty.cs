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
	/// Make sure to make only one instance of this object per variable. Each new instance "resets" the counter.
	/// 
	/// <para>
	/// A property that can only be set once. After that, any attempts of changing the property will result in an exception.
	/// </para>
	/// 
	/// <para>
	/// <example>
	/// To set this up, use this code:
	/// <code>
	/// private <see cref="OneSetProperty{T}"/> _prop { get; set; } = new <see cref="OneSetProperty{T}"/>();
	/// public string Prop {
	/// 	get { return _prop.Value; }
	/// 	set {
	/// 		_prop.Value = value;
	///		}
	/// }
	/// </code>
	/// Or, if you don't mind the <c>'Value'</c> property, you can make <c>'Prop'</c> an instance of <see cref="OneSetProperty{T}"/> with auto-implemented properties.
	/// </example>
	/// </para>
	/// </summary>
	/// <typeparam name="T">The type of the property.</typeparam>
	public class OneSetProperty<T> : XSetPropertyBase<T> {
		public override int? SetsAllowed { get; set; } = 1;
		public override string ExceptionMessage { get; set; } = "This property can only be set once.";

		public OneSetProperty() { }
		public OneSetProperty(T value) { Value = value; }
		public OneSetProperty(OneSetProperty<T> oneSetProperty) { Value = oneSetProperty.Value; }
	}
}
