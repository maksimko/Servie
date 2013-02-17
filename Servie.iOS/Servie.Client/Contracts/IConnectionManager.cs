using System;
using RestSharp;
using System.Net;

namespace Servie.Client
{
	public interface IConnectionManager
	{
		event Action<Exception, HttpStatusCode> ErrorOccurred;		
	}
}

