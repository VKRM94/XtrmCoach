using System.Collections.Generic;

namespace XtrmCoachRESTServer.Models
{
	public class HighChart
	{
		public Chart chart { get; set; }

		public Title title { get; set; }

		public Title subtitle { get; set; }

		public XAxis xAxis { get; set; }

		public YAxis yAxis { get; set; }

		public ToolTip tooltip { get; set; }

		public List<SeriesElement> series { get; set; }
	}

	public class Chart
	{
		public string type { get; set; }
	}

	public class Title
	{
		public string text { get; set; }
	}

	public class XAxis
	{
		public List<string> categories { get; set; }
	}

	public class YAxis
	{
		public Title title { get; set; }
	}

	public class ToolTip
	{
		public string valueSuffix { get; set; }
	}

	public class SeriesElement
	{
		public string name { get; set; }

		public List<string> data { get; set; }
	}
}