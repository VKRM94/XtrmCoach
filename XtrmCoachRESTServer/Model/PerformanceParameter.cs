namespace XtrmCoachRESTServer.Models
{
	public class PerformanceParameter
	{
		public long id { get; set; }
		public long sportId { get; set; }
		public PerformanceParameterName perfParaName { get; set; }
		public string customName { get; set; }
		public PerformanceParameterType perfParaType { get; set; }
	}
}