using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer.Controller
{
	public class PlayerController : ApiController
	{
		private IPlayerRepository _iPlayerRepository;

		public PlayerController(IPlayerRepository playerRepository)
		{
			_iPlayerRepository = playerRepository;
		}

		// GET: api/Player
		public ArrayList Get(int id)
		{
			return _iPlayerRepository.GetPlayers(id);
		}

		/*
		// GET: api/Player/5
		public Player Get(int userId, int id)
		{
			return _iPlayerRepository.GetPlayer((long)id);
		}
		*/

		// POST: api/Player
		public HttpResponseMessage Post([FromBody]Player player)
		{
			HttpResponseMessage response;
			/*
			var httpRequest = HttpContext.Current.Request;
			if (httpRequest.Files.Count < 1)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}

			foreach (string file in httpRequest.Files)
			{
				var postedFile = httpRequest.Files[file];
				var filePath = HttpContext.Current.Server.MapPath("~/PlayerImage" + postedFile.FileName);
				postedFile.SaveAs(filePath);
				// NOTE: To store in memory use postedFile.InputStream
			}
			*/

			if (player == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(player.firstName))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player First Name is empty.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(player.lastName))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player Last Name is empty.");
				return response;
			}

			if (player.dob == null || player.dob.ToString().Equals("1/1/0001 12:00:00 AM"))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player Date of Birth is empty.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(player.imageId))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player Image Id is empty.");
				return response;
			}

			if (player.sports == null || player.sports.Count < 1)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player Sport Info is empty.");
				return response;
			}

			long playerId = _iPlayerRepository.InsertPlayer(player);

			if (playerId != -1)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Player added successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Error adding player.");
			}

			response = Request.CreateResponse(HttpStatusCode.OK, "Player added successfully.");
			return response;
		}

		// PUT: api/Player/5
		public HttpResponseMessage Put([FromBody]Player player)
		{
			HttpResponseMessage response;

			if (player == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			if (player.id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player Id is Invalid.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(player.firstName))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player First Name is empty.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(player.lastName))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player Last Name is empty.");
				return response;
			}

			if (player.dob == null)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player Date of Birth is empty.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(player.imageId))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player Image Id is empty.");
				return response;
			}

			if (player.sports == null || player.sports.Count < 1)
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "Player Sport Info is empty.");
				return response;
			}

			bool isUpdated = _iPlayerRepository.UpdatePlayer(player);

			if (isUpdated)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Updated Player successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid Player id passed.");
			}

			return response;
		}

		// DELETE: api/Player/5
		public HttpResponseMessage Delete(int id)
		{
			HttpResponseMessage response;

			if (id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid Id Passed.");
				return response;
			}

			bool isDeleted = _iPlayerRepository.DeletePlayer(id);

			if (isDeleted)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Player successfully deleted.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid Player Id.");
			}

			return response;
		}
	}
}