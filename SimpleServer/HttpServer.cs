using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace SimpleServer
{
	public abstract class HttpServer 
	{
		protected int port;
		TcpListener listener;
		bool is_active = true;

		public HttpServer(int port) 
		{
			this.port = port;
		}

		public void listen() 
		{
			listener = new TcpListener(port);
			listener.Start();
			while (is_active)
			{
				TcpClient s = listener.AcceptTcpClient();
				HttpProcessor processor = new HttpProcessor(s, this);
				Thread thread = new Thread(new ThreadStart(processor.process));
				thread.Start();
				Thread.Sleep(1);
			}
		}

		public abstract void handleGETRequest(HttpProcessor p);
		public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
	}

	public class MyHttpServer : HttpServer
	{
		public MyHttpServer(int port) : base(port) 
		{
		}

		public override void handleGETRequest (HttpProcessor p)
		{
			string heading;
			string message;

			p.writeSuccess();

			Console.WriteLine("request: {0}", p.http_url);


			p.outputStream.WriteLine("<html><body>");
			p.outputStream.WriteLine("Current Time: " + DateTime.Now.ToString());

			switch (p.http_url) {
			case "/animals":
				AnimalHandler animals = new AnimalHandler ();
				animals.getAllAnimals ();
				break;
			case "/habitats":
				break;
			case "/employees":
				break;
			}


			if (p.http_url.Equals ("/animals")) 
			{
				heading = "You requested a list of animals";
				message = "There are currently 57 animals in my Zoo";

				p.outputStream.WriteLine("<h1>{0}</h1>", heading);
				p.outputStream.WriteLine("<div>{0}</div>", message);

				p.outputStream.WriteLine("<form method=\"POST\" action=\"/animal\">");
				p.outputStream.WriteLine("<input type=\"text\" name=\"animal-name\" placeholder=\"Enter a new animal name\">");
				p.outputStream.WriteLine("<input type=\"submit\" value=\"Create Animal\">");
				p.outputStream.WriteLine("</form>");
			}

			p.outputStream.WriteLine("</body></html>");
		}

		public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData) 
		{
			Console.WriteLine("POST request: {0}", p.http_url);
			string data = inputData.ReadToEnd();

			p.writeSuccess();
			p.outputStream.WriteLine("<html><body><h1>test server</h1>");
			p.outputStream.WriteLine("<a href=/test>return</a><p>");
			p.outputStream.WriteLine("postbody: <pre>{0}</pre>", data);
		}
	}
}

