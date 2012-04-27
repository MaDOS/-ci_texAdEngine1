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
		public string map;
		public Dictionary<int, item> items; //id, instance
		public Dictionary<int, area> areas; //filename, instance
		public Dictionary<string, player> activePlayers;
		
		public game(string map_param)
		{
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
			
			rdr = new StreamReader(root + "\\maps\\" + map + "\\")
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
					areas.Add(Convert.ToInt32(splitLine[0], new area(splitLine[1]));
				}
			}
			rdr.Close();
			
			activePlayers = new Dictionary<string, player>();
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
				
				activePlayers.Add(playername, new player(playername, areas[Convert.ToInt32(playerDataIni.GetSetting("attributes", "area"))]));
			}
			else
			{
				IniFile playerDataIni = new IniFile(saveFile);
				playerDataIni.AddSetting("identification", "password", password);
				playerDataIni.SaveSettings();
				ci_crypter.decodeFile(saveFile, 139);
				activePlayers.Add(playername, new player(playername, areas[0]));
			}
			
			return "connected successfully!";
		}
	}
}
