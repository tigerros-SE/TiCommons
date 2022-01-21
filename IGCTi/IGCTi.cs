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
using IngameScript.TiCommons.Util.Atlang;

namespace IngameScript.TiCommons.IGCTi {
	public static class IGCTi {
		#region Properties
		/// <summary>
		/// The valid tags.
		/// The first tag in this collection will be treated as the tag that other blocks listen to.
		/// The following tags will be embedded into the <see cref="Message"/> itself.
		/// </summary>
		//public EnumClass[] Tags = new EnumClass[0];
		/// <summary>
		/// The <see cref="IMyIntergridCommunicationSystem"/> that will be used to send messages.
		/// The usage might not be limited to just sending messages though.
		/// </summary>
		public static IMyIntergridCommunicationSystem IGC { get; }
		#endregion
		#region Constructors
		//public IGCTi(IMyIntergridCommunicationSystem igc, EnumClass[] tags) {
		//	IGC = igc;
		//	Tags = tags;
		//}
		#endregion
		#region Methods
		/// <summary>
		/// Finds all occurences of <c>"[<paramref name="itemName"/>:(.*)]"</c>.
		/// Captures one group - <c>.*</c> (anything).
		/// </summary>
		private static System.Text.RegularExpressions.Regex MessageRX(string itemName)
			=> new System.Text.RegularExpressions.Regex($@"\[{itemName}\s*:\s*(.*)\]");

		/// <summary>
		/// Sends a broadcast message using a <see cref="Message"/> constructed with the given parameters.
		/// </summary>
		/// <typeparam name="TData">
		/// The datatype.
		/// The <see cref="Type"/> object of this type will be saved in the <see cref="Message"/> object..</typeparam>
		/// <param name="tags">
		/// The message tags.
		/// The first element in this list will be the main message tag that other blocks listen to.
		/// Therefore, the length of this list must be at least 1.</param>
		/// <param name="data">
		/// The data to send.
		/// This will be converted to a string, but the type will be saved in the <see cref="Message"/> object</param>
		/// <param name="description">
		/// The description.
		/// This is an optional parameter and it is likely you only need the <paramref name="data"/>.
		/// </param>
		public static void SendBroadcastMessage<TData>(EnumClass[] tags, TData data, string description = null) {
			if (tags.Length == 0) {
				throw new ArgumentException("The length of the parameter 'tags' must be higher than 0.");
			}

			var tagString = string.Join("", tags.Select((tag, i) => $"[Tag{i}:{tag.EnumValue.Value}]"));
			SendBroadcastMessage(new Message(tags, data + "", description));
		}

		/// <summary>
		/// Sends a broadcast message using a message object.
		/// </summary>
		/// <param name="message">The <see cref="Message"/> object to send.</param>
		public static void SendBroadcastMessage(Message message) {
			var listen = IGC.RegisterBroadcastListener(message.Tags[0].EnumValue);
			var msg = listen.AcceptMessage();
			
			IGC.SendBroadcastMessage(new AtBuilder(message.Tags[0].EnumValue).AddAttribute("#Ti#", true).ToString(), message.ToString());
		}

		public static IMyBroadcastListener RegisterBroadcastListenerTi(this IMyIntergridCommunicationSystem igc, EnumClass tag) {
			return igc.RegisterBroadcastListener(new AtBuilder(new AtObject("Tag", tag.EnumValue)).AddAttribute("#Ti#", true).ToString());
		}

		public static Message ParseTi<TTag>(this IMyBroadcastListener listener) where TTag : EnumClass, new() {
			TTag[] tags;
			var desc = "";
			var tagsMatch = MessageRX("Tags").Match(message.Data.ToString());
			var descMatch = MessageRX("Description").Match(message.Data.ToString());

			if (tagsMatch.Success) {
				var tagGroups = tagsMatch.Groups;

				tags = new TTag[tagGroups.Count];

				for (int tagI = 0; tagI < tagGroups.Count; tagI++) {
					tags[tagI] = new TTag() { EnumValue = tags[tagI].EnumValue };
				}
			} else {
				throw new ArgumentException("No tags found in the parameter 'message'.");
			}

			if (descMatch.Success) desc = descMatch.Groups[0].Value;

			return new Message(tags, message.Data, desc);
		}
		#endregion
	}
}
