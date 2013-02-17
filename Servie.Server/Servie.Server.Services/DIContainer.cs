using System;
using SimpleInjector;

namespace Servie.Server.Services
{
	public static class DIContainer 
	{
		private static Container _instance;
		public static Container Current { get { return _instance ?? (_instance = new Container()); } }
	}
}

