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
	/// A property that can only be set only on initialization. After that, any attempts of changing the property will result in an exception.
	/// </para>
	/// </summary>
	/// <typeparam name="T">The type of the property.</typeparam>
	public class InitProperty<T> {
		private bool IsSet = false;
		private T _value;
		public T Value {
			get {
				return _value;
			} set {
				if (IsSet) {
					throw new Exception("You can only set this property on initialization.");
				} else {
					_value = value;
					IsSet = true;
				}
			}
		}

		public InitProperty() {
			IsSet = true;
		}

		public InitProperty(T value) {
			Value = value;
			IsSet = true;
		}

		public InitProperty(InitProperty<T> ip) {
			Value = ip.Value;
			IsSet = true;
		}
	}
}
