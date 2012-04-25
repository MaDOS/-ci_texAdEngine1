using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ci_texAdEngine1;

namespace ci_texAdEngine1
{
	public class game
	{
		public static string root = Path.GetDirectoryName(Assembly.GetAssembly(typeof(game)).CodeBase);
		public static area spawn;
//		private area[] activeAreas;
		
		public game(area startMap)
		{
			spawn = startMap;
//			activeAreas += spawn;
		}
	}
}
