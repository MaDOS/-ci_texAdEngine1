using System;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class area
	{
		Dictionary<int, item> items;
				
		int id;
		
		public area(string name)
		{
			IniFile dataIni = new IniFile(game.root + "\\maps\\" + name + ".ini");
			items = new Dictionary<int, item>();
//			Load items from a save-ini
			
			
		}
	}
}
