using System.Collections;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer.RepositoryInterface
{
	public interface IPerformanceParameterNameRepository
	{
		ArrayList GetPerformanceParameterNames();
		PerformanceParameterName GetPerformanceParameterName(long performanceParamterNameId);
	}
}