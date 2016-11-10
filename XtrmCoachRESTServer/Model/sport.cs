namespace XtrmCoachRESTServer.Models
{
	public class Sport
	{
		public long id { get; set; }

		public string name { get; set; }

		public long user_id { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Sport s = obj as Sport;
			if ((System.Object)s == null)
			{
				return false;
			}

			if (this.id != s.id) return false;
			if (this.name != s.name) return false;
			if (this.user_id != s.user_id) return false;

			return true;
		}
	}
}