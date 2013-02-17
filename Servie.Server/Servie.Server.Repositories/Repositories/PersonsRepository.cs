using System;
using Servie.Domain;

namespace Servie.Server.Repositories
{
	public class PersonsRepository : BaseRepository<Person>, IPersonsRepository
	{
		public PersonsRepository () : base()
		{
			throw new NotImplementedException();
		}
	}
}

