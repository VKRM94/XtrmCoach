using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer.Controllers
{
	public class SignupController : ApiController
	{
		private IUserRepository _iUserRepository;

		public SignupController(IUserRepository userRepository)
		{
			_iUserRepository = userRepository;
		}

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
			HttpResponseMessage response;

			if (user == null)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid JSON Passed.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(user.firstName))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "User First Name is empty.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(user.lastName))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "User Last Name is empty.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(user.emailId))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "User Email is empty.");
				return response;
			}

			if (string.IsNullOrWhiteSpace(user.password))
			{
				response = Request.CreateResponse(HttpStatusCode.PartialContent, "User Password is empty.");
				return response;
			}

			long userId = _iUserRepository.SaveUser(user);

			if (userId != -1)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "User created successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "User already present.");
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
