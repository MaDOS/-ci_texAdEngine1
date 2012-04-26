using System;
using System.IO;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class area
	{
		Dictionary<int, item> items;
		IniFile dataIni;
		string filename;
		string name;
		
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
			
			items = new Dictionary<int, item>();
			
			string tempItemString = dataIni.GetSetting("content", "items");
			
			foreach(string itemFilename in tempItemString.Split(','))
			{
				items.Add(items.Count, new item(itemFilename));
			}
			
		}
		
		public void save()
		{
			string itemsString = "";
			
			foreach(item i in items.Values)
			{
				itemsString += i.filename + ",";
			}
			
			itemsString = itemsString.Substring(0, itemsString.Length - 1); //trim last comma away
			
			dataIni.AddSetting("content", "items", itemsString);
			
			dataIni.SaveSettings(game.root + "\\saves\\maps\\" + name + ".ini");
		}
	}
}
