using System;

namespace Servie.Extensions
{
	public static class ActionExtensions
	{
		public static void Raise<T>(this Action<T> @action, T obj)
		{
			if (@action != null)
				@action (obj);
		}

		public static void Raise<T1, T2>(this Action<T1, T2> @action, T1 obj1, T2 obj2)
		{
			if (@action != null)
				@action (obj1, obj2);
		}
	}
}

