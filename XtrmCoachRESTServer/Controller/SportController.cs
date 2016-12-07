using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer.Controller
{
	public class SportController : ApiController
	{
		private ISportRepository _iSportRepository;

		public SportController(ISportRepository sportRepository)
		{
			_iSportRepository = sportRepository;
		}

		// GET: api/Sport
		public ArrayList Get()
		{
			long userId = 0;

			long.TryParse(HttpContext.Current.Request.QueryString.GetValues("userId")[0], out userId);

			if (userId== 0)
			{
				return null;
			}
			else
			{
				return _iSportRepository.GetSports(userId);
			}
		}

		// GET: api/Sport/5
		public Sport Get(int id)
		{
			return _iSportRepository.GetSport((long)id);
		}

		// POST: api/Sport
		public HttpResponseMessage Post([FromBody]Sport sport)
		{
			HttpResponseMessage response;

			if (sport == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(sport.name))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Sport Name is empty.");
				return response;
			}

			if (sport.userId <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "User Id is empty.");
				return response;
			}

			long sportId = _iSportRepository.InsertSport(sport);

			if (sportId != -1)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Sport added successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Error adding sport.");
			}

			return response;
		}

		// PUT: api/Sport/5
		public HttpResponseMessage Put([FromBody]Sport sport)
		{
			HttpResponseMessage response;

			if (sport == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			if (sport.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Sport Id is Invalid.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(sport.name))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Sport Name is empty.");
				return response;
			}

			if (sport.userId <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "User Id is invalid.");
				return response;
			}

			bool isUpdated = _iSportRepository.UpdateSport(sport);

			if (isUpdated)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Updated Sport successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid Sport id passed.");
			}

			return response;
		}

		// DELETE: api/Sport/5
		public HttpResponseMessage Delete(int id)
		{
			HttpResponseMessage response;

			if (id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid Id Passed.");
				return response;
			}

			bool isDeleted = _iSportRepository.DeleteSport(id);

			if (isDeleted)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Sport successfully deleted.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid Sport Id.");
			}

			return response;
		}
	}
}