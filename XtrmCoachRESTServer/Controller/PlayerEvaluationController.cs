using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer.Controller
{
	public class PlayerEvaluationController : ApiController
	{
		private IPlayerEvaluationRepository _iPlayerEvaluationRepository;

		public PlayerEvaluationController(IPlayerEvaluationRepository PlayerEvaluationRepository)
		{
			_iPlayerEvaluationRepository = PlayerEvaluationRepository;
		}

		// GET: api/PlayerEvaluation
		public ArrayList Get()
		{
			int sportId = 0;
			int playerId = 0;

			int.TryParse(HttpContext.Current.Request.QueryString.GetValues("sportId")[0], out sportId);
			int.TryParse(HttpContext.Current.Request.QueryString.GetValues("playerId")[0], out playerId);

			if (sportId == 0 || playerId == 0)
			{
				return null;
			}
			else
			{
				return _iPlayerEvaluationRepository.GetPlayerEvaluations(sportId, playerId);
			}
		}

		// POST: api/PlayerEvaluation
		public HttpResponseMessage Post([FromBody]PlayerEvaluation PlayerEvaluation)
		{
			HttpResponseMessage response;

			if (PlayerEvaluation == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			if (PlayerEvaluation.sportId <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Sport Id passed.");
				return response;
			}

			if (PlayerEvaluation.playerId <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Player Id passed.");
				return response;
			}

			if (PlayerEvaluation.perfParaName.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Name Id passed.");
				return response;
			}

			if (PlayerEvaluation.selectedType.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Type Id passed.");
				return response;
			}

			long PlayerEvaluationId = _iPlayerEvaluationRepository.InsertPlayerEvaluation(PlayerEvaluation);

			if (PlayerEvaluationId != -1)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "PlayerEvaluation added successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Error adding PlayerEvaluation.");
			}

			return response;
		}

		// PUT: api/PlayerEvaluation/5
		public HttpResponseMessage Put([FromBody]PlayerEvaluation PlayerEvaluation)
		{
			HttpResponseMessage response;

			if (PlayerEvaluation == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			if (PlayerEvaluation.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid Id Passed.");
				return response;
			}

			if (PlayerEvaluation.sportId <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Sport Id passed.");
				return response;
			}

			if (PlayerEvaluation.playerId <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Player Id passed.");
				return response;
			}

			if (PlayerEvaluation.perfParaName.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Name Id passed.");
				return response;
			}

			if (PlayerEvaluation.selectedType.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Invalid Type Id passed.");
				return response;
			}

			bool isUpdated = _iPlayerEvaluationRepository.UpdatePlayerEvaluation(PlayerEvaluation);

			if (isUpdated)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Updated PlayerEvaluation successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid PlayerEvaluation id passed.");
			}

			return response;
		}

		// DELETE: api/PlayerEvaluation/5
		public HttpResponseMessage Delete(int id)
		{
			HttpResponseMessage response;

			if (id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid Id Passed.");
				return response;
			}

			bool isDeleted = _iPlayerEvaluationRepository.DeletePlayerEvaluation(id);

			if (isDeleted)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "PlayerEvaluation successfully deleted.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid PlayerEvaluation Id.");
			}

			return response;
		}
	}
}