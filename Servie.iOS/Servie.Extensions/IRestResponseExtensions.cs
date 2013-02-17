using System;
using RestSharp;
using System.Net;

namespace Servie.Extensions
{
	public static class IRestResponseExtensions
	{
		public static bool IsSuccess<T>(this IRestResponse<T> @response)
		{
			return 
				@response.ResponseStatus == ResponseStatus.Completed 
				&& @response.StatusCode == HttpStatusCode.OK;
		}

		public static Exception GetException<T>(this IRestResponse<T> @response)
		{
			return @response.ErrorException ?? new Exception (@response.ErrorMessage);
		}
	}
}

