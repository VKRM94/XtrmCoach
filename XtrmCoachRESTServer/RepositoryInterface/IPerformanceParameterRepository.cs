using System.Collections;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer.RepositoryInterface
{
	public interface IPerformanceParameterRepository
	{
		//ArrayList GetPerformanceParameters();
		ArrayList GetPerformanceParameters(long sportsId);
		PerformanceParameter GetPerformanceParameter(long performanceParamterId);
		long InsertPerformanceParameter(PerformanceParameter performanceParamter);
		bool DeletePerformanceParameter(long performanceParamterId);
		bool UpdatePerformanceParameter(PerformanceParameter performanceParamter);
	}
}