using System;

namespace mineumcli
{
	class MainClass
	{
		public const string mc_ufile_url="http://127.0.0.1/mc_version";
	 static void Main (string[] args)
		{

			MinecraftClient Xen = new MinecraftClient();
			MinecraftSettings s = new MinecraftSettings();
			Console.WriteLine(Xen.getActualVersion());
			Console.ReadLine ();

			
		}
	}
}
