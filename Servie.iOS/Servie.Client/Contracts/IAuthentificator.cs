using System;
using RestSharp;
using Servie.Domain;

namespace Servie.Client.Contracts
{
	public interface IAuthentificator
	{
		event Action<Person> LoginSucceed;
		event Action<Person> LogoutSucceed;
		event Action<Person> RegisterSucceed;

		void Register(bool async = false); //TODO: Return connection token
		void Login(bool async = false); //TODO: Return connection token
		void Logout(bool async = false);
	}
}

