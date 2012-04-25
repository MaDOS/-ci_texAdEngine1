using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ci_texAdEngine1;

namespace ci_texAdEngine1
{
	public class game
	{
//		public Dictionary<int,item> items;
		public static string root = Path.GetDirectoryName(Assembly.GetAssembly(typeof(game)).CodeBase);
		private area[] activeAreas;
		
		public game(area startMap)
		{
//			items = new Dictionary<int,item>();
		}
	}
}
