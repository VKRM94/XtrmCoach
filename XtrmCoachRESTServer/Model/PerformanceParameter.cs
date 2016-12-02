namespace XtrmCoachRESTServer.Models
{
	public class PerformanceParameter
	{
		public long id { get; set; }
		public long sportId { get; set; }
		public PerformanceParameterName perfParaName { get; set; }
		public string customName { get; set; }
		public PerformanceParameterTypeGroup perfParaTypeGroup { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			PerformanceParameter s = obj as PerformanceParameter;
			if ((System.Object)s == null)
			{
				return false;
			}

			if (this.id != s.id) return false;
			if (this.sportId != s.sportId) return false;
			if (this.perfParaName.id != s.perfParaName.id) return false;
			if (this.perfParaName.name != s.perfParaName.name) return false;
			if (this.perfParaTypeGroup.id != s.perfParaTypeGroup.id) return false;
			if (this.perfParaTypeGroup.name != s.perfParaTypeGroup.name) return false;
			if (this.customName != s.customName) return false;

			return true;
		}
	}
}