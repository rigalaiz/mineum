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
			var hashesClient = new Dictionary<string,string> ();
			var hashesServer = new Dictionary<string,string> ();
			MinecraftClient Xen = new MinecraftClient ();
			MinecraftSettings s = new MinecraftSettings ();
			hashesClient = Xen.getHashes ();
			hashesServer = Xen.getHashesSrv ();
			var dlist = new List<string> ();
			var delList = new List<string> ();
			foreach (var ust in hashesClient) {
				Console.WriteLine (ust.Key + "si");
			}
			foreach (var ust in hashesServer) 
			{
				if (hashesClient.ContainsKey (ust.Key)) 
				{
					Console.WriteLine ("совпадение!" + ust.Key);
					if (hashesClient [ust.Key] != ust.Value) 
					{
						dlist.Add (ust.Key);
						delList.Add (ust.Key);
					}
				} 
				else 
				{
					dlist.Add (ust.Key);
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine ("несовпадение!" + ust.Key);
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
			foreach (string file in delList)
			{
				Console.WriteLine("Deleting file...{0}",file);
				Xen.delFile(Xen.getPath()+"\\"+file);
			}
			foreach(string file in dlist)
			{
				Console.WriteLine("Downloading file...{0}",file);
				Xen.getFile(s.files_url+file,Xen.getPath()+"\\"+file);
			}
			Console.ReadLine ();	
		}

	}
}
