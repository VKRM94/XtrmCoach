using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer.RepositoryInterface
{
	public interface IUserRepository
	{
		long SaveUser(User userToSave);
		User AuthenticateUser(string username, string password);
	}
}