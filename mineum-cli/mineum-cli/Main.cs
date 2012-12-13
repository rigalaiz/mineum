using System;
using System.Reflection;
using System.Text;
namespace mineumcli
{
	class MainClass
	{
		public const string mc_ufile_url="http://127.0.0.1/mc_version";
	 static void Main (string[] args)
		{
			MinecraftClient Xen = new MinecraftClient ();
			MinecraftSettings s = new MinecraftSettings ();
			string[,] test = Xen.getHashes ();
			Console.WriteLine(test.GetLength(0));
			//Console.WriteLine(Xen.getHashes()[1,0]);
			Console.ReadLine ();	
		}

	}
}
