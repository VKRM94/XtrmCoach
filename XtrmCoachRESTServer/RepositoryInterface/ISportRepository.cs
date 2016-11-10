using System.Collections;
using XtrmCoachRESTServer.Models;
using MySql.Data;

namespace XtrmCoachRESTServer.RepositoryInterface
{
	public interface ISportRepository
	{
		ArrayList GetSports();
		Sport GetSport(long sportId);
		long InsertSport(Sport sport);
		bool DeleteSport(long sportId);
		bool UpdateSport(Sport sport);
	}
}