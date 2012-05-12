using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ci_texAdEngine1;

namespace ci_texAdEngine1
{
	public class game
	{
		public static string root = Path.GetDirectoryName(Assembly.GetAssembly(typeof(game)).CodeBase).Remove(0, 6); //remove(0,6) trims the "file:\" away
		public static int itemsCount;
		public static string map;
		public static Dictionary<int, item> items; //id, instance
		public static Dictionary<int, area> areas; //filename, instance
		private static IniFile itemIds;
		private static IniFile areaIds;
		
		public game(string map_param)
		{
			map = map_param;
			
			items = new Dictionary<int, item>();
			areas = new Dictionary<int, area>();
			
			itemIds = new IniFile(root + "\\items\\ids.ini");
			itemsCount = itemIds.keyPairs.Count;
			
			Console.WriteLine("Loading items...");
			
			foreach(string id in itemIds.EnumSection("ids"))
			{
				string itemFile = itemIds.GetSetting("ids", id);
				
				items.Add(Convert.ToInt32(id), new item(itemFile));
				
				Console.WriteLine("Loaded item with id: " + id + " => " + itemFile.Replace(".ini", ""));
			}
			
			
			areaIds = new IniFile(root + "\\maps\\" + map + "\\ids.ini");
			
			Console.WriteLine("Loading areas of map \"" + map + "\" ...");
			
			foreach(string id in areaIds.EnumSection("ids"))
			{
				string areaFile = areaIds.GetSetting("ids", id);
				
				areas.Add(Convert.ToInt32(id), new area(map, areaFile));
				
				Console.WriteLine("Loaded area with id: " + id + " => " + areaFile.Replace(".ini", ""));
			}
		}
		
		
        /// <summary>
        /// Method to call to pass a connected client to the engine. Returns: false if password was wrong.
        /// </summary>
        /// <param name="connectedPlayer">Instance of the player that connected.</param>
		public void onConnect(player connectedPlayer)
		{	
			Console.WriteLine("Spawning player " + connectedPlayer.name + " in area " + connectedPlayer.position.name + " (areaId: " + connectedPlayer.areaId + ")");
			connectedPlayer.position.activePlayers.Add(connectedPlayer.name, connectedPlayer);
		}
	}
}
