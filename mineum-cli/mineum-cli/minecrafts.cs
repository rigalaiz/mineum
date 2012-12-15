using System;
using System.Net;
using System.Xml;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;


namespace mineumcli
{
	public class MinecraftSettings
	{

		public string version_url;
		public string md5hashes_url;
		public string login;
		public string password;
		public string metar_url;
		public string metar_fav_icao;
		public MinecraftSettings ()
		{
			XmlTextReader reader = new XmlTextReader ("../../settings.xml");
			this.version_url=getCbyE(reader,"version_url");
			this.md5hashes_url=getCbyE(reader,"md5hashes_url");
		}
		public MinecraftSettings (string config_path)
		{
		}
		public string getCbyE(XmlTextReader reader, string element)
		{
			while (reader.Read ()) 
			{
				if (reader.MoveToContent () == XmlNodeType.Element && reader.Name == element) 
				{
					return reader.ReadString ();
				}

			}
			return reader.ReadString();
		}
	}
	public class MinecraftClient
		{	
			public string path;
			public string version;
			public string[,] md5_hashes;
			/*public string XMLRead (string filename)
			{
				XmlTextReader reader = new XmlTextReader ("settings.xml");
				while (reader.Read()) 
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
						break;
						case XmlNodeType.Text: 
						Console.WriteLine (reader.Value);
						break;
					}
				}
			}*/
			public string getActualVersion() 
			{ 
				//depends on XMLRead and may be function which will get vars from cfg
				MinecraftSettings s = new MinecraftSettings();
				WebClient con = new WebClient ();
				byte[] udata;
				try 
				{
					udata = con.DownloadData (s.version_url);
					return Encoding.ASCII.GetString (udata);
				} 
				catch (System.Net.WebException e) 
				{
					return e.Message;
				}	
			}

			public string getPath ()
			{ 
				//ready
				switch (getOS ()) 
				{
					//1 - unix; 2 - win
					case 1:
					return System.Environment.GetEnvironmentVariable("HOME")+"/.minecraft";
					case 2:
					//return System.Environment.GetEnvironmentVariable("APPDATA")+"\\.minecraft";
					return System.Environment.GetEnvironmentVariable("APPDATA")+"\\mc_test";
					default:
					return "";
				}
			}

			public bool getFile (string getFrom, string getTo)
		{
			WebClient con = new WebClient ();
			try {
				con.DownloadFile (getFrom,getTo);
				return true;
			} 
			catch (WebException e) 
			{
				// Do add...
				return false;
			}
				
			}
			public Dictionary<string,string> getHashes ()
			{
				string[] files;
				var hashes = new Dictionary<string,string>();
				try 
				{
					// нэработает!!!Directory.Exists(getPath());
					files = Directory.GetFiles (getPath (), "*.*", SearchOption.AllDirectories);
				}
				catch (System.IO.DirectoryNotFoundException e) 
				{
					Console.WriteLine(e.Message);
					Directory.CreateDirectory(getPath());
				}
				finally
				{
					Console.WriteLine("Dont worry");
					files = Directory.GetFiles (getPath (), "*.*", SearchOption.AllDirectories);
					//moved up
					//hashes = new string[files.Length,2];
					//foreach (string file in files) {
					for (int d = 0 ; d<files.Length; d++)
					{
						FileStream f = new FileStream (files[d], FileMode.Open, FileAccess.Read);
						byte[] buf = new byte[(int)f.Length];
						f.Read (buf, 0, (int)f.Length);
						MD5 hasher = MD5.Create ();
						byte[] data = hasher.ComputeHash (buf);
						StringBuilder sBuilder = new StringBuilder ();
						for (int i = 0; i<data.Length; i++)
						{
							sBuilder.Append (data [i].ToString ("x2"));
						}
						hashes.Add(files[d],sBuilder.ToString());
						//hashes[d,0]=files[d];
						//hashes[d,1]=sBuilder.ToString();
						Console.WriteLine (sBuilder.ToString () + " " + files[d]);
					}
				}
				return hashes;
			}
			public Dictionary<string,string> getHashesSrv ()
			{
				var hashes = new Dictionary<string,string>();
				MinecraftSettings s = new MinecraftSettings();
				WebClient con = new WebClient ();
				byte[] udata;
				try 
				{
					string t = con.DownloadString(s.md5hashes_url);
					string[] u = t.Split(new char[] {' ','\n'},StringSplitOptions.RemoveEmptyEntries);
				//Code for Dictionaries.
				for (int i=1; i<u.Length;i=i+2)
					{
						hashes.Add(u[i],u[i-1]);
					}
				} 
				catch (System.Net.WebException e) 
				{
					Console.WriteLine(e.Message);
					//return e.Message;
				}
				return hashes;
			}

			public int getOS() 
			{ 
				//ready. may be mac os ?
				switch (System.Environment.OSVersion.Platform) {
			case PlatformID.Unix:
				return 1;
			case PlatformID.Win32NT:
				return 2;
			default:
				return 0;
			}
			}

			public string getVersion() 
			{ 
				// get version of what? i think - current
				FileStream fstream = new FileStream(getPath ()+"\\mc_version",FileMode.Open,FileAccess.Read);
				//(int)f.Length;
				byte[] buf = new byte[(int)fstream.Length];
				fstream.Read (buf, 0, (int)fstream.Length);
				return Encoding.ASCII.GetString(buf);
			}

			public bool compareVersions ()
			{
				if (this.getVersion () == this.getActualVersion ()) 
				{
					//false if no updates
					return false;
				}
				else 
				{
					//true if updates
					return true;
				}
			}
			public static bool IsOdd(int value)
			{
				return value % 2 != 0;
			}

			public static bool IsEven(int value)
			{
			return value % 2 == 0;
			}

		}
	public class MinecraftClientServer : MinecraftClient
	{
			public string host;
			public MinecraftClientServer() //Constructor
			{
				//host = XMLRead("settings.xml"); //looking for server ip in settings using some built-in XML functions
				version = getActualVersion(); //from server version file/variable
				path = getPath(); //hardcoded path for some time
				// temporary commented
				//md5_hashes = getHashes(); //assuming hashes would be put into the array and then compared

			}
	}
	public class MinecraftClientUser : MinecraftClient
	{
			public int OS;
			public MinecraftClientUser() //Constructor
			{
				OS = getOS(); //System.Environment.OSVersion.Platform
				// temporary commented
				//md5_hashes = getHashes(); //same as for server
				path = getPath(); //based on OS
				version = getVersion();
			}
	}
}
