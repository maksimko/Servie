using System;

namespace Servie.Client.Core
{
	public class ServerCredentials
	{
		private static ServerCredentials _credentials;

		public string Address { get; private set; }
		public int Port { get; private set; }
		public int ApiVersion { get; private set; }
		public string AccessUrl { get; private set; }		

		private ServerCredentials(int port, string address, int apiVersion)
		{
			Port = port;
			Address = address;
			ApiVersion = apiVersion;

			AccessUrl = String.Format("http://{0}:{1}/v{2}/", address, port, apiVersion);
		}

		public static ServerCredentials Test { get { return _credentials ?? (_credentials = new ServerCredentials(8080, "127.0.0.1", 1)); } }

		public static ServerCredentials Production { get { return _credentials ?? (_credentials = new ServerCredentials (80, "production.server.com", 1)); } }

	}
}

