using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servie.Server.Services;
using Servie.Domain;
using Newtonsoft.Json;

namespace Servie.Server.Web.Controllers
{
	public class AuthorizationController : BaseController
    {
		public AuthorizationController (IPersonService personService) : base(personService)
		{
		}

		[HttpPost]
		public ActionResult Login(string jsonData)
        {	
			LogContent("Login");
			LogContent (jsonData);

			var person = Deserialize<Person> (jsonData);

			var personResponse = new Person()
			{
				Login = "Response login",
				Password = "Response password"
			};

			return JsonResponse (personResponse);
        }

		[HttpPost]
		public ActionResult Logout(string jsonData)
		{
			return LogContent("Logout");
		}

		[HttpPost]
		public ActionResult Register(string jsonData)
		{
			return LogContent("Register");
		}
    }
}
