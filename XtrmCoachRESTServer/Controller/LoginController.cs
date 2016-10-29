using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using System.Web.Script.Serialization;
using XtrmCoachRESTServer.RepositoryInterface;

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

			if (user != null && user.emailId != null && user.password != null)
			{
				user = _iUserRepository.AuthenticateUser(user.emailId, user.password);

				if (user != null)
				{
					response = Request.CreateResponse(HttpStatusCode.OK);
					response.Content = new StringContent(new JavaScriptSerializer().Serialize(user));
				}
				else
				{
					response = Request.CreateResponse(HttpStatusCode.NotFound, "User not present.");
				}

				return response;
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid JSON Passed.");
				return response;
			}
		}
	}
}