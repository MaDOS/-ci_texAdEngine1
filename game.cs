using System;
using System.Collections.Generic;
using System.IO;
using ci_;
using System.Reflection;
using ci_texAdEngine1;

namespace ci_texAdEngine1
{
	public class game
	{
		public static string root = Path.GetDirectoryName(Assembly.GetAssembly(typeof(game)).CodeBase);
		public static int itemsCount = new IniFile(root + "\\items\\ids.ini").keyPairs.Count;
		public static string map;
		public static Dictionary<int, item> items; //id, instance
		public static Dictionary<int, area> areas; //filename, instance
		
		public game(string map_param)
		{
			items = new Dictionary<int, item>();
			areas = new Dictionary<int, area>();
			
			StreamReader rdr = new StreamReader(root + "\\items\\ids.ini");
			string line = rdr.ReadLine();
			while(line != "")
			{
				line = rdr.ReadLine();
				if(line.ToCharArray()[0] == '#' 
				   || line.ToCharArray()[0] == '\'' 
				   || line.ToCharArray()[0] == '[')
				{
					//comment or section-identifier
				}
				else
				{
					string[] splitLine = line.Split('=');
					items.Add(Convert.ToInt32(splitLine[0]), new item(splitLine[1]));
				}
			}
			rdr.Close();
			
			rdr = new StreamReader(root + "\\maps\\" + map + "\\");
			line = rdr.ReadLine();
			while(line != "")
			{
				line = rdr.ReadLine();
				if(line.ToCharArray()[0] == '#' 
				   || line.ToCharArray()[0] == '\'' 
				   || line.ToCharArray()[0] == '[')
				{
					//comment or section-identifier
				}
				else
				{
					string[] splitLine = line.Split('=');
					areas.Add(Convert.ToInt32(splitLine[0]), new area(splitLine[1]));
				}
			}
			rdr.Close();
		}
		
		public string onConnect(string playername, string password) //first connection handling and password protecting are a bit messed up and very unsafe. I will change this later
		{
			string saveFile = root + "\\saves\\players\\" + playername + ".ini";
			
			if(File.Exists(saveFile))
			{
			
				ci_crypter.encodeFile(saveFile, 139);
				IniFile playerDataIni = new IniFile(saveFile);
				ci_crypter.decodeFile(saveFile, 139);
					
				//check password
				if(password != playerDataIni.GetSetting("identification", "password"))
				{
					return "incorrect password!";
				}
				
				int spawnAreaId = Convert.ToInt32(playerDataIni.GetSetting("attributes", "area"));
				
				areas[spawnAreaId].activePlayers.Add(playername, new player(playername, areas[spawnAreaId]));
			}
			else
			{
				IniFile playerDataIni = new IniFile(saveFile);
				playerDataIni.AddSetting("identification", "password", password);
				playerDataIni.SaveSettings();
				ci_crypter.decodeFile(saveFile, 139);
				areas[0].activePlayers.Add(playername, new player(playername, areas[0]));
			}
			
			return "connected successfully!";
		}
	}
}
