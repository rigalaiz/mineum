using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
namespace mineumcli
{
	class MainClass
	{

	 static void Main (string[] args)
		{
			var dic = new Dictionary<string,string>();
			MinecraftClient Xen = new MinecraftClient ();
			MinecraftSettings s = new MinecraftSettings ();
			//string[,] test = Xen.getHashes ();
			//Console.WriteLine(test.GetLength(0));
			dic=Xen.getHashes();
			foreach (var ust in dic)
			{
				Console.WriteLine (ust.Key);
			}
			//Console.WriteLine(Xen.getHashes()[1,0]);
			Console.ReadLine ();	
		}

	}
}
