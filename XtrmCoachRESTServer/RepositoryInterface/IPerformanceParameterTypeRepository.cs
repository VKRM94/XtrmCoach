using System.Collections;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer.RepositoryInterface
{
	public interface IPerformanceParameterTypeRepository
	{
		ArrayList GetPerformanceParameterTypes(long performanceParamterNameId);
	}
}