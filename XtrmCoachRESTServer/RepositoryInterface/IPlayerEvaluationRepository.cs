using System.Collections;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer.RepositoryInterface
{
	public interface IPlayerEvaluationRepository
	{
		ArrayList GetPlayerEvaluations(long sportsId, long playerId);
		PlayerEvaluation GetPlayerEvaluation(long evaluationId);
		long InsertPlayerEvaluation(PlayerEvaluation playerEvaluation);
		bool DeletePlayerEvaluation(long playerEvaluationId);
		bool UpdatePlayerEvaluation(PlayerEvaluation playerEvaluation);
	}
}