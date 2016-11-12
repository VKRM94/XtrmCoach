using System.Collections;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer.Controller
{
	public class PerformanceParameterNameController : ApiController
	{
		private IPerformanceParameterNameRepository _iPerformanceParameterNameRepository;

		public PerformanceParameterNameController(IPerformanceParameterNameRepository performanceParameterNameRepository)
		{
			_iPerformanceParameterNameRepository = performanceParameterNameRepository;
		}

		// GET: api/PerformanceParameter
		public ArrayList Get()
		{
			return _iPerformanceParameterNameRepository.GetPerformanceParameterNames();
		}

		// GET: api/PerformanceParameter/5
		public PerformanceParameterName Get(int id)
		{
			return _iPerformanceParameterNameRepository.GetPerformanceParameterName((long)id);
		}
	}
}