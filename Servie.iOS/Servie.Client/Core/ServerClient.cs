using System;
using System.Net;
using RestSharp;

using Actions = System.String;

namespace Servie.Client.Core
{
	public class ServerClient : RestClient
	{
		public class Actions
		{
			public const string Register = "Authorization/Register";
			public const string Login = "Authorization/Login";
			public const string Logout = "Authorization/Logout";
			public const string Sync = "Sync";		
		}

		public ServerClient(ServerCredentials server)
		{
			BaseUrl = server.AccessUrl;
			Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("");
		}

		public RestRequest CreateJsonRequest(Actions action, Method method, object data, int timeout = 30000)
		{
			var request = new RestRequest(action, method);
			request.RequestFormat = DataFormat.Json;
			request.AddBody(data ?? "");
			request.Timeout = timeout;

			return request;
		}

		public RestRequest CreateRequest(Actions action, Method method, object data, int timeout = 30000)
		{
			var request = new RestRequest(action, method);
			request.RequestFormat = DataFormat.Xml;
			request.AddBody(data ?? "");
			request.Timeout = timeout;
			
			return request;
		}

		public void Execute<T>(RestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> worker, RequestType type = RequestType.Sync) where T : new()
		{
			var method = request.Method;

			if (type == RequestType.Async)
			{
				if (method == Method.GET)
					ExecuteAsyncGet<T>(request, worker, method.ToString());
				else
					ExecuteAsyncPost<T>(request, worker, method.ToString());
			}
			else
			{
				IRestResponse<T> response = null;

				if (method == Method.GET)
					response = ExecuteAsGet<T>(request, method.ToString());					
				else
					response = ExecuteAsPost<T>(request, method.ToString());
				
				worker(response, null);
			}
		}
	}
}

