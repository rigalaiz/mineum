using System;

namespace mineumcli
{
	public class MinecraftClient
		{	
			public string path;
			public string version;
			public string[] md5_hashes;
			public string XMLRead(string filename)
			{

			}
			public string getActualVersion() 
			{ 
			
			}	
			public string getPath() 
			{ 
			
			}
			public string[] getHashes() 
			{ 
			
			}
			public int getOS() 
			{ 
			
			}
			public string getVersion() 
			{ 
			
			}
		}
	public class MinecraftClientServer : MinecraftClient
	{
			public string host;
			public MinecraftClientServer() //Constructor
			{
				host = XMLRead("settings.xml"); //looking for server ip in settings using some built-in XML functions
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