using System.Collections;
using System.Web.Http;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer.Controller
{
	public class PerformanceParameterTypeController : ApiController
	{
		private IPerformanceParameterTypeRepository _iPerformanceParameterTypeRepository;

		public PerformanceParameterTypeController(IPerformanceParameterTypeRepository performanceParameterTypeRepository)
		{
			_iPerformanceParameterTypeRepository = performanceParameterTypeRepository;
		}

		// GET: api/PerformanceParameter/5
		public ArrayList Get(int id)
		{
			return _iPerformanceParameterTypeRepository.GetPerformanceParameterTypes((long)id);
		}
	}
}