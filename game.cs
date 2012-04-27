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
		public static Dictionary<string, area> activeAreas;
		public static Dictionary<string, player> activePlayers;
		
		public game(string spawnAreaFileName)
		{
			activePlayers = new Dictionary<string, player>();
		}
		
		public string onConnect(string playername, string password)
		{
			string saveFile = root + "\\saves\\players\\" + playername + ".ini";
			
			ci_crypter.encodeFile(saveFile, 139);
			IniFile playerDataIni = new IniFile(saveFile);
			ci_crypter.decodeFile(saveFile, 139);
				
			//check password
			if(password != playerDataIni.GetSetting("identification", "password"))
			{
				return "incorrect password!";
			}
			
			activeAreas[playerDataIni.GetSetting("attributes", "position")].spawnPlayer(playername);
			
			return "connected successfully!";
		}
	}
}
