using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer.Controllers
{
	public class SignupController : ApiController
	{
		/*// GET: api/User
		public ArrayList Get()
		{
			// just testing push and pull
			// i will remove this line later
			UserPersistence userPersistance = new UserPersistence();
			return userPersistance.getUser();
		}

		//GET: api/User/5
		public User Get(long id)
		{
			UserPersistence userPersistance = new UserPersistence();
			return userPersistance.getUser(id);
		}*/

		// POST: api/Signup
		public HttpResponseMessage Post([FromBody]User user)
		{
			UserPersistence userPersistance = new UserPersistence();
			HttpResponseMessage response;
			long userId = userPersistance.saveUser(user);

			if (userId != -1)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "User created successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.BadRequest, "User already present.");
			}

			return response;
		}

		/*// PUT: api/User/5
		public void Put(int id, [FromBody]string value)
		{
			// TODO: Implementation Pending
		}*/


		/*// DELETE: api/User/5
		public HttpResponseMessage Delete(long id)
		{
			UserPersistence userPersistance = new UserPersistence();
			bool recordExisted = userPersistance.deleteUser(id);

			HttpResponseMessage response = null;

			if (recordExisted)
			{
				response = Request.CreateResponse(HttpStatusCode.OK);
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return response;
		}*/
	}
}
