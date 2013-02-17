using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleInjector;
using Newtonsoft.Json;
using Servie.Server.Services;
using System.Text;

namespace Servie.Server.Web.Controllers
{
    public class BaseController : Controller
    {
		protected Container IocContainer { get { return MvcApplication.IocContainer; } }		

		protected IPersonService PersonService;

		public BaseController ()
		{
		}

		public BaseController (IPersonService personService) : base()
		{
			PersonService = personService;
		}

        public ActionResult Index()
        {        
			return LogContent(GetType().Name);
        }

		public ActionResult LogContent(string text)
		{
			Console.WriteLine(text);

			return Content(text);
		}

		protected T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T> (json);
		}

		protected ContentResult JsonResponse(object data)
		{
			var jsonData = JsonConvert.SerializeObject (data);

			return new ContentResult {
				Content = jsonData,
				ContentEncoding = Encoding.UTF8,
				ContentType = "application/json"
			};
		}
    }
}
