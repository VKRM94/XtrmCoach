
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace XtrmCoachRESTServer.Controller
{
    public class SportController : ApiController
    {
		private ISportRepository _iSportRepository;
		// GET: api/Sport
		public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sport/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sport
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Sport/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sport/5
        public void Delete(int id)
        {
        }
    }
}
