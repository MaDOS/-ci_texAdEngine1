using System;
using System.IO;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class area
	{
		public Dictionary<int, int> drops; //id, count
		public Dictionary<string, player> activePlayers;
		public Dictionary<string, int> directions; //direction, id of the area the direction is pointing at
		private IniFile dataIni;
		public int id;
		private string filename;
		public string name;
		public string map;
		public string description;
		
		public area(string map_param, string filename_param)
		{
			map = map_param;
			string savedMap = game.root + "\\saves\\maps\\" + map + "\\" + filename_param;
			
			if(File.Exists(savedMap))
			{
				dataIni = new IniFile(savedMap);
			}
			else
			{
				dataIni = new IniFile(game.root + "\\maps\\" + map + "\\" + filename_param);
			}
			
			id = Convert.ToInt32(dataIni.GetSetting("identification", "id"));
			filename = dataIni.GetSetting("identification", "filename");
			name = dataIni.GetSetting("identification", "name");
			description = dataIni.GetSetting("content", "desc");
			
			
			drops = new Dictionary<int, int>();
			
			string serializedItems = dataIni.GetSetting("content", "drops");
			
			if(serializedItems.Trim() != "")
			{
				string[] serializedPairs = serializedItems.Split(',');
				string[] splittedPair = new string[1];
				
				foreach(string pair in serializedPairs)
				{
					splittedPair = pair.Split(':');
					drops.Add(Convert.ToInt32(splittedPair[0]), Convert.ToInt32(splittedPair[1]));
				}
			}
			
			
			directions = new Dictionary<string, int>();
			
			string serializedDirections = dataIni.GetSetting("properties", "directions");
			string[] serializedDirection = serializedDirections.Split(',');
			string[] splittedDirection = new string[1];
			
			foreach(string pair in serializedDirection)
			{
				splittedDirection = pair.Split(':');
				directions.Add(splittedDirection[0], Convert.ToInt32(splittedDirection[1]));
			}
			
			activePlayers = new Dictionary<string, player>();
		}
		
		public void save()
		{
			string serializedItems = "";
			
			foreach(int id in drops.Keys)
			{
				serializedItems += id.ToString() + ":" + drops[id].ToString() + ",";
			}
			
			serializedItems = serializedItems.Substring(0, serializedItems.Length - 1); //trim last comma away
			
			dataIni.AddSetting("content", "drops", serializedItems);
			
			dataIni.SaveSettings(game.root + "\\saves\\maps\\" + map + "\\" + filename);
		}
	}
}
