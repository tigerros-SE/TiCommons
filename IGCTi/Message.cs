using Microsoft.SqlServer.Server;
using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Scripting;
using VRageMath;
using IngameScript.TiCommons.Extensions;
using IngameScript.TiCommons.Util;

namespace IngameScript.TiCommons.IGCTi {
	/// <summary>
	/// A class for a message.
	/// This will be sent by
	/// <see cref="IGCTi.SendBroadcastMessage(Message)"/>
	/// and then parsed by <see cref="IGCTi.Parse{TTag}(MyIGCMessage)"/>.
	/// </summary>
	public class Message {
		public EnumClass[] Tags { get; }
		public string Description { get; }
		public object Data { get; }

		public Message(EnumClass[] tags, object data, string description) {
			Tags = tags;
			Data = data;
			Description = description;
		}

		public override string ToString() {
			var tagString = string.Join("", Tags.Select((tag, i) => $"[Tag{i}:{tag.EnumValue.Value}]"));
			
			return tagString +
			$"[Data:{Data}]" +
			$"[Description:{Description}]";
		}
	}
}
