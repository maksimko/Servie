using System;
using System.ComponentModel;
using Servie.Server.Repositories;

namespace Servie.Server.Services
{
	public class BaseService<T> : IService<T>
	{
		protected IPersonsRepository PersonsRepository;
	
		public BaseService ()
		{
			//throw new NotImplementedException();
		}

		public BaseService(IPersonsRepository personsRepository)
		{
			PersonsRepository = personsRepository;
		}
	}
}

