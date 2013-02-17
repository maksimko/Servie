using System;
using Servie.Client.Contracts;
using RestSharp;
using Servie.Client.Core;
using Servie.Domain;
using Servie.Extensions;
using System.Net;

namespace Servie.Client
{
	public class ConnectionManager : IConnectionManager, IAuthentificator {
		public event Action<Exception, HttpStatusCode> ErrorOccurred;

		static ConnectionManager _manager;
		ServerClient _client;

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

		public static IConnectionManager Instance { get { return _manager ?? (_manager = new ConnectionManager()); } }

		private ConnectionManager()
		{
			_client = new ServerClient(ServerCredentials);
		}

		#region IAuthentificator section

		public event Action<Person> LoginSucceed;
		public event Action<Person> LogoutSucceed;
		public event Action<Person> RegisterSucceed;

		public static IAuthentificator Authentificator { get { return _manager ?? (_manager = new ConnectionManager()); } }

		public void Register(bool async = false)
		{
			var person = new Person {
				Login = "Request login",
				Password = "Request password"
			};

			var request = _client.CreateJsonRequest(ServerClient.Actions.Register, Method.POST, person);

			_client.Execute<Person>(request, HandleRegister, async ? RequestType.Async : RequestType.Sync);
		}

		public void Login(bool async = false)
		{
			var person = new Person {
				Login = "Request login",
				Password = "Request password"
			};

			var request = _client.CreateJsonRequest(ServerClient.Actions.Login, Method.POST, person);

			_client.Execute<Person>(request, HandleLogin, async ? RequestType.Async : RequestType.Sync);
		}

		public void Logout(bool async = false)
		{
			var person = new Person {
				Login = "Request login",
				Password = "Request password"
			};

			var request = _client.CreateJsonRequest(ServerClient.Actions.Logout, Method.POST, person);

			_client.Execute<Person>(request, HandleLogout, async ? RequestType.Async : RequestType.Sync);			
		}

		private void HandleRegister(IRestResponse<Person> response, RestRequestAsyncHandle handle)
		{
			if (!response.IsSuccess ()) {
				ErrorOccurred.Raise(response.GetException(), response.StatusCode);
				return;
			}

			RegisterSucceed.Raise(response.Data);

		}

	    private void HandleLogin(IRestResponse<Person> response, RestRequestAsyncHandle handle)
		{		
			if (!response.IsSuccess ()) {
				ErrorOccurred.Raise(response.GetException(), response.StatusCode);
				return;
			}

			LoginSucceed.Raise(response.Data);			
		}

		private void HandleLogout(IRestResponse<Person> response, RestRequestAsyncHandle handle)
		{
			if (!response.IsSuccess ()) {
				ErrorOccurred.Raise(response.GetException(), response.StatusCode);
				return;
			}

			LogoutSucceed.Raise(response.Data);			
		}

		#endregion
	}
}

