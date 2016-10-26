using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using System.Collections;

namespace XtrmCoachRESTServer.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public ArrayList Get()
        {
            // just testing push and pull
            // i will remove this line later
            UserPersistence userPersistance = new UserPersistence();
            return userPersistance.getUser();
        }

        // GET: api/User/5
        public User Get(long id)
        {
            UserPersistence userPersistance = new UserPersistence();
            return userPersistance.getUser(id);
        }

        // POST: api/User
        public HttpResponseMessage Post([FromBody]User value)
        {
            UserPersistence userPersistance = new UserPersistence();
            long userId = userPersistance.saveUser(value);
            value.Id = userId;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("person/{0}", userId));
            return response;
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
            // TODO: Implementation Pending
        }

        // DELETE: api/User/5
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
        }
    }
}
