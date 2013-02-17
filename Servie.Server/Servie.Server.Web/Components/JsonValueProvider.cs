using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Web.Script.Serialization;
using Servie.Domain;
using Servie.Server.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Servie.Server.Web
{
	public class JsonValueProvider : ValueProviderFactory
	{
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			var deserializedJson = GetDeserializedJson(controllerContext);

			if (deserializedJson == null) return null;
			
			var backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

			AddToBackingStore(backingStore, "jsonData", deserializedJson);
			
			return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
		}
		
		private static void AddToBackingStore(Dictionary<string, object> backingStore, string prefix, object value)
		{
			var dictionary = value as IDictionary<string, object>;
			if (dictionary != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
					AddToBackingStore(backingStore, MakePropertyKey(prefix, keyValuePair.Key), keyValuePair.Value);
			}
			else
			{
				var list = value as IList;
				if (list != null)
				{
					for (var index = 0; index < list.Count; ++index)
						AddToBackingStore(backingStore, MakeArrayKey(prefix, index), list[index]);
				}
				else 
					backingStore[prefix] = value;
			}
		}
		
		private static object GetDeserializedJson(ControllerContext controllerContext)
		{
			var contentType = controllerContext.HttpContext.Request.ContentType;

			var contentTypeIsJson = contentType.StartsWith("text/json", StringComparison.OrdinalIgnoreCase) || contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase);

			if (!contentTypeIsJson) 
				return null;
			
			var input = new StreamReader(controllerContext.HttpContext.Request.InputStream).ReadToEnd();

			return input;

//			if (String.IsNullOrEmpty(input)) return null;

//			var type = input.GetSerilizedType();

//			var types = Type.GetType (type); 

//			var syncObject = JsonConvert.DeserializeObject<SyncObject> (input);
//			var genericType = Convert.ChangeType (syncObject, Type.GetType(syncObject.TypeName));

//			var realObject = JsonConvert.DeserializeObject<> (input);

//			return realObject;
		}
		
		private static string MakeArrayKey(string prefix, int index)
		{
			return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
		}
		
		private static string MakePropertyKey(string prefix, string propertyName)
		{
			if (!string.IsNullOrEmpty(prefix))
				return prefix + "." + propertyName;
			return propertyName;
		}
	}
}

