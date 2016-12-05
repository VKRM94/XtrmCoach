using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class HighChartTest
	{
		[TestMethod]
		public void HighChart_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				HighChart highChart = new HighChart();

				// Assert
				Assert.IsNotNull(highChart);
			}
			catch (Exception)
			{
				throw new AssertFailedException("HighChart initialization failed.");
			}
		}

		[TestMethod]
		public void HighChart_Property_Set_Test()
		{
			try
			{
				// Arrange
				HighChart highChart = new HighChart();

				// Act
				highChart.chart = new Chart();
				highChart.chart.type = "line";
				highChart.series = new List<SeriesElement>();
				SeriesElement seriesElement = new SeriesElement();
				seriesElement.name = "Player 1";
				seriesElement.data = new List<string>();
				seriesElement.data.Add("10");
				seriesElement.data.Add("20");
				highChart.series.Add(seriesElement);
				highChart.subtitle = new Title();
				highChart.subtitle.text = "Subtitle";
				highChart.title = new Title();
				highChart.title.text = "Title";
				highChart.tooltip = new ToolTip();
				highChart.tooltip.valueSuffix = "Suffix";
				highChart.xAxis = new XAxis();
				highChart.xAxis.categories = new List<string>();
				highChart.xAxis.categories.Add("Category 1");
				highChart.yAxis = new YAxis();
				highChart.yAxis.title = new Title();
				highChart.yAxis.title.text = "Y Axis Title";

				// Assert
				Assert.AreEqual<string>(highChart.chart.type, "line");
				Assert.AreEqual<string>(highChart.series[0].name, "Player 1");
				Assert.AreEqual<string>(highChart.series[0].data[0], "10");
				Assert.AreEqual<string>(highChart.series[0].data[1], "20");
				Assert.AreEqual<string>(highChart.subtitle.text, "Subtitle");
				Assert.AreEqual<string>(highChart.title.text, "Title");
				Assert.AreEqual<string>(highChart.tooltip.valueSuffix, "Suffix");
				Assert.AreEqual<string>(highChart.xAxis.categories[0], "Category 1");
				Assert.AreEqual<string>(highChart.yAxis.title.text, "Y Axis Title");
			}
			catch (Exception)
			{
				throw new AssertFailedException("Setting HighChart properties failed.");
			}
		}
	}
}
