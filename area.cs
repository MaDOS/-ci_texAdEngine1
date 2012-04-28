using System;
using System.IO;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class area
	{
		public Dictionary<int, int> drops; //id, count
		public Dictionary<string, player> activePlayers;
		private IniFile dataIni;
		public int id;
		private string filename;
		private string name;
		private string map;
		
		public area(string filename)
		{
			string savedMap = game.root + "\\saves\\maps\\" + map + "\\" + filename;
			
			if(File.Exists(savedMap))
			{
				dataIni = new IniFile(savedMap);
			}
			else
			{
				dataIni = new IniFile(game.root + "\\maps\\" + map + "\\" + filename);
			}
			
			id = Convert.ToInt32(dataIni.GetSetting("identification", "id"));
			filename = dataIni.GetSetting("identification", "filename");
			name = dataIni.GetSetting("identification", "name");
			map = dataIni.GetSetting("identification", "map");
			
			drops = new Dictionary<int, int>();
			
			string serializedItems = dataIni.GetSetting("content", "drops");
			string[] serializedPairs = serializedItems.Split(',');
			string[] splittedPair = new string[1];
			
			foreach(string pair in serializedPairs)
			{
				splittedPair = pair.Split(':');
				drops.Add(Convert.ToInt32(splittedPair[0]), Convert.ToInt32(splittedPair[1]));
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
