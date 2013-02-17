using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servie.Server.Services;
using Servie.Server.Domain;
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
			TemporaryLog("Login");

			var person = Deserialize<Person> (jsonData);

			var personResponse = new Person()
			{
				Login = "test",
				Password = "test"
			};

			return JsonResponse (personResponse);
        }

		[HttpPost]
		public ActionResult Register(string jsonData)
		{
			return TemporaryLog("Register");
		}
    }
}
