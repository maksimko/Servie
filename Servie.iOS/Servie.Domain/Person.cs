using System;
using System.Runtime.Serialization;

namespace Servie.Server.Domain
{
	[Serializable]
	public class Person
	{
		public string Login { get; set; }	
		public string Password { get; set; }
	}
}

