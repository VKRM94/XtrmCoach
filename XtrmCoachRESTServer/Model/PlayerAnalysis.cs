using System;
using System.Collections.Generic;

namespace XtrmCoachRESTServer.Models
{
	public class PlayerAnalysis
	{
		public Sport sport { get; set; }
		public List<Player> players { get; set; }
		public PerformanceParameter perfPara { get; set; }
		public string timeRange { get; set; }
		public DateTime fromTime { get; set; }
		public DateTime toTime { get; set; }
	}
}