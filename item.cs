using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ci_texAdEngine1
{
	public class item
	{
		public string filename;
		public string name;
		public int id;
		public bool takeable;
		public bool useable;
		public bool weapon;
		public bool key;
		public bool eatable;
		public int damage;
		public int heal;
		public double weight;
		private IniFile dataIni;
		
		
		public item(string filename)
		{
			dataIni = new IniFile(game.root + "\\items\\" + filename);
			
			id = Convert.ToInt32(dataIni.GetSetting("identification", "id"));
			filename = dataIni.GetSetting("identification", "filename");
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
		
		public item(int id)
		{
			string filename = new IniFile(game.root + "\\items\\ids.ini").GetSetting("ids", id.ToString());
			
			dataIni = new IniFile(game.root + "\\items\\" + filename);
			
			id = Convert.ToInt32(dataIni.GetSetting("identification", "id"));
			filename = dataIni.GetSetting("identification", "filename");
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
