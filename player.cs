using System;
using System.IO;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class player
	{
		public Dictionary<int, item> inventory;
		
		public player(string playername)
		{
			if(File.Exists(game.root + "\\saves\\players\\" + playername + ".ini"));
				IniFile dataIni = new IniFile(game.root + "\\saves\\players\\" + playername + ".ini");
			
			inventory = new Dictionary<int, item>();
//			Load inventory from a save-ini
			
		}
		
		public void save()
		{
			
		}
	}
}
