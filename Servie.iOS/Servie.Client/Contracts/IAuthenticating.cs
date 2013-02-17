using System;

namespace Servie.Client.Contracts
{
	public interface IAuthenticating
	{
		void Register(bool async = false); //TODO: Return connection token
		void Login(bool async = false); //TODO: Return connection token
		void Logout(bool async = false);
	}
}

