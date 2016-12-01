using System;
using System.Collections;
using System.Collections.Generic;

namespace XtrmCoachRESTServer.Models
{
	public class Player
	{
		public long id { get; set; }

		public string firstName { get; set; }

		public string lastName { get; set; }

		public DateTime dob { get; set; }

		public string imageId { get; set; }

		public List<Sport> sports { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Player s = obj as Player;
			if ((System.Object)s == null)
			{
				return false;
			}

			if (this.id != s.id) return false;
			if (this.firstName != s.firstName) return false;
			if (this.lastName != s.lastName) return false;
			if (this.dob != s.dob) return false;
			if (this.imageId != s.imageId) return false;
			if (this.sports.Equals(s.sports)) return false;

			return true;
		}
	}
}