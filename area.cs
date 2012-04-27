using System;
using System.IO;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class area
	{
		public Dictionary<int, int> drops; //id, count
		private IniFile dataIni;
		public int id;
		private string filename;
		private string name;
		
		public area(string filename)
		{
			string savedMap = game.root + "\\saves\\maps\\" + filename;
			
			if(File.Exists(savedMap))
			{
				dataIni = new IniFile(savedMap);
			}
			else
			{
				try
				{
					dataIni = new IniFile(game.root + "\\maps\\" + filename);
				}
				catch(Exception ex)
				{
					throw ex; 
				}
			}
			
			filename = dataIni.GetSetting("identification", "filename");
			name = dataIni.GetSetting("identification", "name");
			
			drops = new Dictionary<int, int>();
			
			string serializedItems = dataIni.GetSetting("content", "drops");
			string[] serializedPairs = serializedItems.Split(',');
			string[] splittedPair = "";
			foreach(string pair in serializedPairs)
			{
				splittedPair = pair.Split(':');
				drops.Add(Convert.ToInt32(splittedPair[0]), Convert.ToInt32(splittedPair[1]));
			}
		}
		
		public void spawnPlayer(string name)
		{
//			game.activePlayers.Add(name, new player(name, this));
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
		}
	}
}
