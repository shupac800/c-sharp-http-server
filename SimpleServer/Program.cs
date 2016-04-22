using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SimpleServer;

namespace NSS 
{
	public class SimpleServer
	{
		public static int Main(String[] args) 
		{
			HttpServer httpServer;
			if (args.GetLength(0) > 0)
			{
				httpServer = new MyHttpServer(Convert.ToInt16(args[0]));
			} else {
				httpServer = new MyHttpServer(8080);
			}
			Thread thread = new Thread(new ThreadStart(httpServer.listen));
			thread.Start();
			return 0;
		}
	}
}


