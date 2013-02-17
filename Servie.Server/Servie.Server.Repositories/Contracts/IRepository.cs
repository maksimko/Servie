using System;

namespace Servie.Server.Repositories
{
	public interface IRepository<T>
	{
		void Insert(T @this);
		void Remove(T @this);
		void Get(DateTime date);
	}
}

