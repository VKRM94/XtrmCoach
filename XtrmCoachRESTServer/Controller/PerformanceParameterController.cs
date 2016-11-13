using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer.Controller
{
	public class PerformanceParameterController : ApiController
	{
		private IPerformanceParameterRepository _iPerformanceParameterRepository;

		public PerformanceParameterController(IPerformanceParameterRepository performanceParameterRepository)
		{
			_iPerformanceParameterRepository = performanceParameterRepository;
		}

		// GET: api/PerformanceParameter/5
		public ArrayList Get(int id)
		{
			return _iPerformanceParameterRepository.GetPerformanceParameters((long)id);
		}

		// POST: api/PerformanceParameter
		public HttpResponseMessage Post([FromBody]PerformanceParameter performanceParameter)
		{
			HttpResponseMessage response;

			if (performanceParameter == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			if (performanceParameter.sportId <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Sport Id passed.");
				return response;
			}

			if (performanceParameter.perfParaName.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Name Id passed.");
				return response;
			}

			if (performanceParameter.perfParaType.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Type Id passed.");
				return response;
			}

			long performanceParameterId = _iPerformanceParameterRepository.InsertPerformanceParameter(performanceParameter);

			if (performanceParameterId != -1)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "PerformanceParameter added successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Error adding performanceParameter.");
			}

			return response;
		}

		// PUT: api/PerformanceParameter/5
		public HttpResponseMessage Put([FromBody]PerformanceParameter performanceParameter)
		{
			HttpResponseMessage response;

			if (performanceParameter == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			if (performanceParameter.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid Id Passed.");
				return response;
			}

			if (performanceParameter.sportId <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Sport Id passed.");
				return response;
			}

			if (performanceParameter.perfParaName.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Name Id passed.");
				return response;
			}

			if (performanceParameter.perfParaType.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Type Id passed.");
				return response;
			}

			bool isUpdated = _iPerformanceParameterRepository.UpdatePerformanceParameter(performanceParameter);

			if (isUpdated)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Updated PerformanceParameter successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid PerformanceParameter id passed.");
			}

			return response;
		}

		// DELETE: api/PerformanceParameter/5
		public HttpResponseMessage Delete(int id)
		{
			HttpResponseMessage response;

			if (id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid Id Passed.");
				return response;
			}

			bool isDeleted = _iPerformanceParameterRepository.DeletePerformanceParameter(id);

			if (isDeleted)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "PerformanceParameter successfully deleted.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid PerformanceParameter Id.");
			}

			return response;
		}
	}
}