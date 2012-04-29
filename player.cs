using System;
using System.IO;
using ci_;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class player
	{
		public Dictionary<int, int> inventory; //id, count
		public string name;
		private IniFile dataIni;
		private string filename;
		private area position;
		private string saveFile;
		
		public player(string playername, area pos)
		{
			saveFile = game.root + "\\saves\\players\\" + playername + ".ini";
			
			inventory = new Dictionary<int, int>();
			
			if(File.Exists(saveFile))
			{
				//player exists
				ci_crypter.encodeFile(saveFile, 139);
				dataIni = new IniFile(saveFile);
				ci_crypter.decodeFile(saveFile, 139);

				
				//inventory parsing
				string serializedItems = dataIni.GetSetting("content", "drops");
				string[] serializedPairs = serializedItems.Split(',');
				string[] splittedPair = new string[1];
				foreach(string pair in serializedPairs)
				{
					splittedPair = pair.Split(':');
					inventory.Add(Convert.ToInt32(splittedPair[0]), Convert.ToInt32(splittedPair[1]));
				}
			}
			else
			{
				//player is new on the server
				filename = playername + ".ini";
				name = playername;
			}
			position = pos;
			
			this.save();
		}
		
		public void takeItem(int id)
		{
			inventory[id]++;
		}
		
		public void dropItem(int id)
		{
			inventory[id]--;
		}
		
		public void go(string direction)
		{
			game.areas[this.position.directions[direction]].activePlayers.Add(this.name, this);
			position.activePlayers.Remove(this.name);
		}
		
		public void save()
		{
			string serializedItems = "";
			
			foreach(int id in inventory.Keys)
			{
				serializedItems += id.ToString() + ":" + inventory[id].ToString() + ",";
			}
			
			serializedItems = serializedItems.Substring(0, serializedItems.Length - 1); //trim last comma away
			
			dataIni.AddSetting("inventory", "items", serializedItems);
			dataIni.AddSetting("attributes", "area", position.id.ToString());
			
			ci_crypter.encodeFile(saveFile, 139);
			dataIni.SaveSettings();
			ci_crypter.decodeFile(saveFile, 139);
		}
	}
}
