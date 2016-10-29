using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace XtrmCoachRESTServer.Models
{
	public class User
	{
		[ScriptIgnore]
		public long id { get; set; }

		public string firstName { get; set; }

		public string lastName { get; set; }

		public string emailId { get; set; }

		[ScriptIgnore]
		public string password { get; set; }

		public bool isAdmin { get; set; }
	}
}