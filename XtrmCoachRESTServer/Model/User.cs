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
		public long id { get; set; }

		public string firstName { get; set; }

		public string lastName { get; set; }

		public string emailId { get; set; }

		[ScriptIgnore]
		public string password { get; set; }

		public bool isAdmin { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			User user = obj as User;
			if ((System.Object)user == null)
			{
				return false;
			}

			if (this.id != user.id) return false;
			if (this.firstName != user.firstName) return false;
			if (this.lastName != user.lastName) return false;
			if (this.emailId != user.emailId) return false;
			if (this.password != user.password) return false;
			if (this.isAdmin != user.isAdmin) return false;

			return true;
		}
	}
}