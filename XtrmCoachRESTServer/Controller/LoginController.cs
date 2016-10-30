using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using System.Web.Script.Serialization;
using XtrmCoachRESTServer.RepositoryInterface;
using XtrmCoachRESTServer.Util;

namespace XtrmCoachRESTServer.Controllers
{
	public class LoginController : ApiController
	{
		private IUserRepository _iUserRepository;

		public LoginController(IUserRepository userRepository)
		{
			_iUserRepository = userRepository;
		}

		// POST: api/Login
		public HttpResponseMessage Post([FromBody]User user)
		{
			HttpResponseMessage response;

			if (user == null)
			{
				response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid JSON Passed.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(user.emailId))
			{
				response = Request.CreateResponse(HttpStatusCode.BadRequest, "User Name is empty.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(user.password))
			{
				response = Request.CreateResponse(HttpStatusCode.BadRequest, "User Password is empty.");
				return response;
			}

			user = _iUserRepository.AuthenticateUser(user.emailId, user.password);

			if (user != null)
			{
				response = Request.CreateResponse(HttpStatusCode.OK);
				response.Content = new StringContent(Helper.Serialize(user));
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.NotFound, "User not present.");
			}

			return response;
		}
	}
}