using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class item
	{
		int id;
		string name;
		bool takeable;
		bool useable;
		bool weapon;
		bool key;
		bool eatable;
		int damage;
		int heal;
		double weight;
		
		
		public item(string filename)
		{
			IniFile dataIni = new IniFile(game.root + "\\items\\" + filename);
			
			id = Convert.ToInt32(dataIni.GetSetting("identification", "id"));
			name = dataIni.GetSetting("identification", "name");
			
			takeable = Convert.ToBoolean(dataIni.GetSetting("flags", "takeable"));
			useable = Convert.ToBoolean(dataIni.GetSetting("flags", "useable"));
			weapon = Convert.ToBoolean(dataIni.GetSetting("flags", "weapon"));
			key = Convert.ToBoolean(dataIni.GetSetting("flags", "key"));
			eatable = Convert.ToBoolean(dataIni.GetSetting("flags", "eatable"));
			
			damage = Convert.ToInt32(dataIni.GetSetting("properties", "damage"));
			heal = Convert.ToInt32(dataIni.GetSetting("properties", "heal"));
			weight = Convert.ToDouble(dataIni.GetSetting("properties", "weight"));
		}
	}
}
