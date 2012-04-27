using System;
using System.IO;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class area
	{
		public Dictionary<int, item[]> items;
		private IniFile dataIni;
		private string filename;
		private string name;
		
		public area(string name)
		{
			if(File.Exists(game.root + "\\saves\\maps\\" + name + ".ini"))
			{
				dataIni = new IniFile(game.root + "\\saves\\maps\\" + name + ".ini");
			}
			else
			{
				try
				{
					dataIni = new IniFile(game.root + "\\maps\\" + name + ".ini");
				}
				catch(Exception ex)
				{
					throw ex; 
				}
			}
			
			filename = dataIni.GetSetting("identification", "filename");
			name = dataIni.GetSetting("identification", "name");
			
			items = new Dictionary<int, item[]>();
			
			string searializedItems = dataIni.GetSetting("content", "items");
			int itemCount;
			string itemType;
			string[] tempItemStringSplit;
			item tempItem;
			item[] tempItemArray;
			
			foreach(string tempItemString in searializedItems.Split(','))
			{
				tempItemStringSplit = tempItemString.Split('#');
				itemType = tempItemStringSplit[0];
				itemCount = Convert.ToInt32(tempItemStringSplit[1]);
				tempItem = new item(itemType);
				
				tempItemArray = new item[itemCount];
				
				for(int i = 1; i <= itemCount; i++)
				{
					tempItemArray[i] = tempItem;
				}
				
				items.Add(tempItem.id, tempItemArray);
			}
		}
		
		public void spawnPlayer(string name)
		{
			game.activePlayers.Add(name, new player(name, this));
		}
		
		public void save()
		{
			string itemsString = "";
			
			foreach(int itemId in items.Keys)
			{
				int itemCount = 0;
				foreach(item i in items[itemId])
				{
					itemCount++;
				}
				itemsString += items[itemId][0].filename + "#" + itemCount + ",";
			}
			
			itemsString = itemsString.Substring(0, itemsString.Length - 1); //trim last comma away
			
			dataIni.AddSetting("content", "items", itemsString);
			
			dataIni.SaveSettings(game.root + "\\saves\\maps\\" + name + ".ini");
		}
	}
}
