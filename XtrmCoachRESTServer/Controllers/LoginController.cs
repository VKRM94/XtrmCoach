using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using System.Web.Script.Serialization;

namespace XtrmCoachRESTServer.Controllers
{
	public class LoginController : ApiController
	{
		// POST: api/Login
		public HttpResponseMessage Post([FromBody]User user)
		{
			UserPersistence userPersistance = new UserPersistence();
			HttpResponseMessage response;

			if (user != null && user.emailId != null && user.password != null)
			{
				user = userPersistance.authenticateUser(user.emailId, user.password);

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