using System.Collections.Generic;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer.RepositoryInterface
{
	public interface IPlayerAnalysisRepository
	{
		HighChart GetPlayerAnalysis(PlayerAnalysis playerAnalysis);
		//PlayerAnalysis GetPlayerAnalysis(long AnalysisId);
		//long InsertPlayerAnalysis(PlayerAnalysis playerAnalysis);
		//bool DeletePlayerAnalysis(long playerAnalysisId);
		//bool UpdatePlayerAnalysis(PlayerAnalysis playerAnalysis);
	}
}