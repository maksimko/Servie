using System;
using MongoDB.Driver;
using System.Configuration;

namespace Servie.Server.Repositories
{
	public static class MongoManager
	{
		public static string Database = "servie";
		private static MongoClient _client;
		private static MongoServer _server;
		private static MongoDatabase _database;

		static MongoManager()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["servie"].ConnectionString;

			_client = new MongoClient(connectionString);
			_server = _client.GetServer();
			_database = _server.GetDatabase(Database);
		}
		
		public static MongoCollection<TC> GetCollection<TC>()
		{
			return _database.GetCollection<TC>(typeof(TC).Name.ToLower());
		}
		
		public static MongoCollection<TC> GetCollection<TC>(string name)
		{
			return _database.GetCollection<TC>(name);
		}
	}
}

