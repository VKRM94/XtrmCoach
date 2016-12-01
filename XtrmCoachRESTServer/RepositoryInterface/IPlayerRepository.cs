using System.Collections;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer.RepositoryInterface
{
	public interface IPlayerRepository
	{
		ArrayList GetPlayers(long userId);
		Player GetPlayer(long playerId);
		long InsertPlayer(Player player);
		bool DeletePlayer(long playerId);
		bool UpdatePlayer(Player player);
	}
}