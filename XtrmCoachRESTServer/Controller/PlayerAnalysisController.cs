using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;
using XtrmCoachRESTServer.Util;

namespace XtrmCoachRESTServer.Controller
{
	public class PlayerAnalysisController : ApiController
	{
		private IPlayerAnalysisRepository _iPlayerAnalysisRepository;

		public PlayerAnalysisController(IPlayerAnalysisRepository PlayerAnalysisRepository)
		{
			_iPlayerAnalysisRepository = PlayerAnalysisRepository;
		}

		// POST: api/PlayerAnalysis
		public HttpResponseMessage Post([FromBody]PlayerAnalysis playerInfo)
		{
			HttpResponseMessage response;

			if (playerInfo == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			HighChart highChart = _iPlayerAnalysisRepository.GetPlayerAnalysis(playerInfo);

			if (highChart != null)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "PlayerAnalysis added successfully.");
				response.Content = new StringContent(Helper.Serialize(highChart));
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Error adding PlayerAnalysis.");
			}

			return response;
		}
	}
}