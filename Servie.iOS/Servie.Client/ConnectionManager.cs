using System;
using Servie.Client.Contracts;
using RestSharp;
using Servie.Client.Core;
using Servie.Domain;

namespace Servie.Client
{
	public class ConnectionManager : IAuthenticating
	{
		private static ConnectionManager _manager;

		public static IAuthenticating Instance { get { return _manager ?? (_manager = new ConnectionManager()); } }

		public ServerCredentials ServerCredentials
		{ 
			get
			{
#if DEBUG
				return ServerCredentials.Test;
#else
				return ServerCredentials.Production;			
#endif
			}
		}

		private ServerClient _client;

		private ConnectionManager()
		{
			_client = new ServerClient(ServerCredentials);
		}

		public void Register(bool async = false)
		{
			var person = new Person {
				Login = "Person login",
				Password = "Person password"
			};

			var request = _client.CreateJsonRequest(ServerClient.Actions.Register, Method.POST, person);

			_client.Execute<Person>(request, OnRegister, async ? RequestType.Async : RequestType.Sync);
		}

		public void Login(bool async = false)
		{
			var person = new Person {
				Login = "Person login",
				Password = "Person password"
			};

			var request = _client.CreateJsonRequest(ServerClient.Actions.Login, Method.POST, person);

			_client.Execute<Person>(request, OnLogin, async ? RequestType.Async : RequestType.Sync);
		}

		public void Logout(bool async = false)
		{
			var person = new Person {
				Login = "Person login",
				Password = "Person password"
			};

			var request = _client.CreateJsonRequest(ServerClient.Actions.Logout, Method.POST, person);

			_client.Execute<Person>(request, OnLogout, async ? RequestType.Async : RequestType.Sync);			
		}

		private void OnRegister(IRestResponse<Person> person, RestRequestAsyncHandle handle)
		{
		}

		private void OnLogin(IRestResponse<Person> person, RestRequestAsyncHandle handle)
		{			
		}

		private void OnLogout(IRestResponse<Person> person, RestRequestAsyncHandle handle)
		{
		}
	}
}

