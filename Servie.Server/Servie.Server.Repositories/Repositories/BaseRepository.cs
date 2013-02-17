using System;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Servie.Server.Repositories
{
	public class BaseRepository<T> : IRepository<T>
	{
		string _className;
		MongoCollection<T> _collection;

		public BaseRepository()
		{
			_className = GetType().Name;
			_collection = MongoManager.GetCollection<T>();
		}

		public virtual void Insert(T @this)
		{
			if (@this == null) throw new ArgumentNullException("Insert " + _className);

			_collection.Insert(@this);
		}

		public virtual void Remove (T @this)
		{
			if (@this == null) throw new ArgumentNullException("Remove " + _className);

			throw new NotImplementedException();
		}

		public virtual void Get(DateTime date)
		{
			throw new NotImplementedException();
		}
	}
}

