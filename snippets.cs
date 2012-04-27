

//srialization of dict<int, item[]>

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
			
			/*
			 * Parsing vv
			 */
			
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