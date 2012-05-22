using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ci_texAdEngine1
{
	public class player
	{
		public Dictionary<int, int> inventory; //id, count
		public string name;
		public string password;
		public bool wasNew;
		public bool loginOk = false;
		private IniFile dataIni;
		private string filename;
		public area position;
		public int areaId;
		private Dictionary<int, area> areasAvailable;
		private string saveFile;
		public int health = 100;
		
		public player(string playername, string password_param, Dictionary<int, area> areasAvailable_param)
		{
			saveFile = game.root + "\\saves\\players\\" + playername + ".ini";
			
			inventory = new Dictionary<int, int>();
			areasAvailable = areasAvailable_param;
			
			if(File.Exists(saveFile))
			{
				//player exists
				wasNew = false;
				
				Console.WriteLine("Player-save exists loading player " + playername + "...");
				
				dataIni = new IniFile(saveFile);

				
				//attributes
				health = Convert.ToInt32(dataIni.GetSetting("attributes", "health"));
				
				//inventory parsing
				string serializedItems = dataIni.GetSetting("inventory", "items");
				if(serializedItems.Trim() != "")
				{
					string[] serializedPairs = serializedItems.Split(',');
					string[] splittedPair = new string[1];
					foreach(string pair in serializedPairs)
					{
						splittedPair = pair.Split(':');
						inventory.Add(Convert.ToInt32(splittedPair[0]), Convert.ToInt32(splittedPair[1]));
					}
				}
				
				name = dataIni.GetSetting("identification", "name");
				password = dataIni.GetSetting("identification", "password");
				
				if(GetSHA512(password_param) == password)
				{
					loginOk = true;
				}
				
				areaId = Convert.ToInt32(dataIni.GetSetting("attributes", "areaId"));
				
				Console.WriteLine("Done loading player " + name + "...");
			}
			else
			{
				//player is new on the server
				Console.WriteLine("No player-save detecded. Creating player " + playername + "...");
				new StreamWriter(new FileStream(saveFile, FileMode.Create)).Close();
				dataIni = new IniFile(saveFile);
				wasNew = true;
				filename = playername + ".ini";
				name = playername.ToUpper();
				password = GetSHA512(password_param);
				areaId = 0;
				loginOk = true;
				dataIni.AddSetting("identification", "filename", filename);
				dataIni.AddSetting("identification", "name", name);
				dataIni.AddSetting("identification", "password", password);
				dataIni.AddSetting("attributes", "areaId", areaId.ToString());
				dataIni.AddSetting("attributes", "health", health.ToString());
				this.save();
				Console.WriteLine("Done creating player " + name + "...");
			}
			
			position = areasAvailable[areaId];
			
		}
		
		public void takeItem(int id)
		{
			if(position.drops[id] > 0)
			{
				position.drops[id]--;
				inventory[id]++;
			}
		}
		
		public void dropItem(int id)
		{
			if(inventory[id] > 0)
			{
				position.drops[id]++;
				inventory[id]--;
			}
		}
		
		public void go(string direction)
		{
			if(this.position.directions[direction.ToUpper()] != -1)
			{
				area newPos = areasAvailable[position.directions[direction.ToUpper()]];
				
				position.activePlayers.Remove(name);
				newPos.activePlayers.Add(name, this);
				position = newPos;
			}
		}
		
		public void save()
		{
			string serializedItems = "";
			
			foreach(int id in inventory.Keys)
			{
				serializedItems += id.ToString() + ":" + inventory[id].ToString() + ",";
			}
				
			if(serializedItems.Length > 0)
			{
				serializedItems = serializedItems.Substring(0, serializedItems.Length - 1); //trim last comma away
			}
			
			dataIni.AddSetting("inventory", "items", serializedItems);
			dataIni.AddSetting("attributes", "areaId", areaId.ToString());
			dataIni.SaveSettings();
		}
		
		private string GetSHA512(string text)
		{
			UnicodeEncoding UE = new UnicodeEncoding();
			byte[] hashValue;
			byte[] message = UE.GetBytes(text);
			 
			SHA512Managed hashString = new SHA512Managed();
			string hex = "";
			 
			hashValue = hashString.ComputeHash(message);
			foreach (byte x in hashValue)
			{
				hex += String.Format("{0:x2}", x);
			}
			return hex.ToUpper();
		}
	}
}
