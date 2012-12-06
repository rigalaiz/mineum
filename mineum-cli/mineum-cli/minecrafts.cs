using System;
using System.Net;
using System.Xml;
using System.Text;
using System.IO;
namespace mineumcli
{
	public class MinecraftSettings
	{
		public string mineum;
		public string version_url;
		public string md5hashes_url;
		public string login;
		public string password;
		public string metar_url;
		public string metar_fav_icao;
		public MinecraftSettings ()
		{
		}
		public MinecraftSettings (string config_path)
		{
		}
	}
	public class MinecraftClient
		{	
			public string path;
			public string version;
			public string[] md5_hashes;
			/*public string XMLRead (string filename)
		{

			XmlTextReader reader = new XmlTextReader ("settings.xml");
			while (reader.Read()) {
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
				WebClient con = new WebClient ();
				byte[] udata;
				try {
					udata = con.DownloadData (version);
					return Encoding.ASCII.GetString (udata);
				} catch (System.Net.WebException e) {
					return e.Message;
				}	
			}	
			public string getPath ()
		{ 
			//ready
			switch (getOS ()) {
				//1 - unix; 2 - win
			case 1:
				return System.Environment.GetEnvironmentVariable("HOME")+"/.minecraft";
				break;

			case 2:
				return System.Environment.GetEnvironmentVariable("APPDATA")+"/.minecraft";
				break;
				default:
					return "";
					break;
			}
			}
			public string[] getHashes() 
			{ 
				string[] d = new string[1];
				return d;
			}
			public int getOS() 
			{ 
			//ready. may be mac os ?
			switch (System.Environment.OSVersion.Platform) {
			case PlatformID.Unix:
				return 1;
				break;
			case PlatformID.Win32NT:
				return 2;
				break;
			default:
				return 0;
				break;
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
				return true;
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
				md5_hashes = getHashes(); //assuming hashes would be put into the array and then compared

			}
	}
	public class MinecraftClientUser : MinecraftClient
	{
			public int OS;
			public MinecraftClientUser() //Constructor
			{
				OS = getOS(); //System.Environment.OSVersion.Platform
				md5_hashes = getHashes(); //same as for server
				path = getPath(); //based on OS
				version = getVersion();
			}
	}
}
