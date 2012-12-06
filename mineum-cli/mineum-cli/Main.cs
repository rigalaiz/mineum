using System;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
namespace mineumcli
{
	class MainClass
	{
		public const string mc_ufile_url="http://127.0.0.1/mc_version";
	 static void Main (string[] args)
		{
			MinecraftSettings s;
			FileStream fs = File.OpenRead("../../settings.xml");
			XmlSerializer x = new XmlSerializer(typeof(MinecraftSettings));
			s =(MinecraftSettings) x.Deserialize(fs);
			Console.WriteLine("login="+s.login);
			Console.ReadLine ();
			//MinecraftClient Xen = new.MinecraftClient;


/*

	 static void md5_gen ()
		{
			string[] files = Directory.GetFiles ("/home/sergey/.minecraft", "*.*", SearchOption.AllDirectories);
			foreach (string file in files) {
				FileStream f = new FileStream (file, FileMode.Open, FileAccess.Read);
				int length = (int)f.Length;
				byte[] buf = new byte[length];
				f.Read (buf, 0, length);
				MD5 hasher = MD5.Create ();
				byte[] data = hasher.ComputeHash (buf);
				StringBuilder sBuilder = new StringBuilder ();
				for (int i = 0; i<data.Length; i++) {
					sBuilder.Append (data [i].ToString ("x2"));
				}
				
				Console.WriteLine (sBuilder.ToString()+" "+file);
			}*/
			
		}
	}
}
