using System.IO;
using ci_;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class player
	{
		public Dictionary<int, item> inventory;
		public string name;
		private IniFile dataIni;
		private string filename;
		private string saveFile;
		private area position;
		
		public player(string playername, area pos)
		{
			saveFile = game.root + "\\saves\\players\\" + playername + ".ini";
			
			inventory = new Dictionary<int, item>();
			
			if(File.Exists(saveFile))
			{
				//player exists
				ci_crypter.encodeFile(saveFile, 139);
				dataIni = new IniFile(saveFile);
				ci_crypter.decodeFile(saveFile, 139);

				
				//inventory
				string tempItemString = dataIni.GetSetting("inventory", "items");
				
				foreach(string itemFilename in tempItemString.Split(','))
				{
					inventory.Add(inventory.Count, new item(itemFilename));
				}
			}
			else
			{
				//player is new on the server
				filename = playername + ".ini";
				name = playername;
				
			}
			
			position = pos;
			
			
		}
		
		private void takeItem(string name)
		{
			
		}
		
		private void dropItem(int id)
		{
			int itemCount = 0;
			
			foreach(item in position.items[id])
			{
				itemCount++;
			}
			
			position.items[id][itemCount - 1] = new item(id);
		}
		
		public void save()
		{
			dataIni.SaveSettings();
		}
	}
}
