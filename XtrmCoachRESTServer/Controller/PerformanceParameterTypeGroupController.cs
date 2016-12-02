using System.Collections;
using System.Web.Http;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer.Controller
{
	public class PerformanceParameterTypeGroupController : ApiController
	{
		private IPerformanceParameterTypeGroupRepository _iPerformanceParameterTypeGroupRepository;

		public PerformanceParameterTypeGroupController(IPerformanceParameterTypeGroupRepository performanceParameterTypeGroupRepository)
		{
			_iPerformanceParameterTypeGroupRepository = performanceParameterTypeGroupRepository;
		}

		// GET: api/PerformanceParameter/5
		public ArrayList Get(int id)
		{
			return _iPerformanceParameterTypeGroupRepository.GetPerformanceParameterTypeGroups((long)id);
		}
	}
}