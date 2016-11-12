namespace XtrmCoachRESTServer.Models
{
	public class PerformanceParameterName
	{
		public long id { get; set; }

		public string name { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			PerformanceParameterName s = obj as PerformanceParameterName;
			if ((System.Object)s == null)
			{
				return false;
			}

			if (this.id != s.id) return false;
			if (this.name != s.name) return false;

			return true;
		}
	}
}