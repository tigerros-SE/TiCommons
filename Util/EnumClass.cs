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

namespace IngameScript.TiCommons.Util {
	/// <summary>
	/// An abstract class that simulates an enum.
	/// <example>
	/// To make your own enum with extra items, use this code:
	/// 
	/// <code>public class YourClass : EnumClass {
	///		public static readonly YourClass ITEM_NAME = new YourClass("ITEM_NAME");
	/// 
	///		public YourClass(string value) : base(value) {}
	/// }</code>
	/// 
	/// To add more items, simply add more properties like <c>ITEM_NAME</c> (with different names).
	/// You can inherit this class further and each time add new items.
	/// </example>
	/// </summary>
	public abstract class EnumClass {
		private OneSetProperty<string> _enumValue { get; set; } = new OneSetProperty<string>();
		/// <summary>
		/// This can be only be changed once. I was highly against it, but one of my libraries could not work without it.
		/// Please try as hard as possible to change this in the constructor.
		/// 
		/// <para>
		/// The string value of the enum.
		/// </para>
		/// </summary>
		public string EnumValue {
			get { return _enumValue.Value; }
			set { _enumValue.Value = value; }
		}

		public override string ToString() => EnumValue;
		public EnumClass() { }
		public EnumClass(string value) { EnumValue = value; }
	}
}
