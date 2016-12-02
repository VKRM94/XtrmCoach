using System.Collections;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer.RepositoryInterface
{
	public interface IPerformanceParameterTypeGroupRepository
	{
		ArrayList GetPerformanceParameterTypeGroups(long performanceParamterNameId);
	}
}