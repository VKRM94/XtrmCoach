using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class PerformanceParameterTest
	{
		[TestMethod]
		public void PerformanceParameter_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PerformanceParameter performanceParameter = new PerformanceParameter();

				// Assert
				Assert.IsNotNull(performanceParameter);
			}
			catch (Exception)
			{
				throw new AssertFailedException("PerformanceParameter initialization failed.");
			}
		}

		[TestMethod]
		public void PerformanceParameter_Property_Set_Test()
		{
			try
			{
				// Arrange
				PerformanceParameter performanceParameter = new PerformanceParameter();

				// Act
				performanceParameter.sportId = 1;
				performanceParameter.perfParaName = new PerformanceParameterName();
				performanceParameter.perfParaName.id = 1;
				performanceParameter.perfParaType = new PerformanceParameterType();
				performanceParameter.perfParaType.id = 1;

				// Assert
				Assert.AreEqual<long>(performanceParameter.sportId, 1);
				Assert.AreEqual<long>(performanceParameter.perfParaName.id, 1);
				Assert.AreEqual<long>(performanceParameter.perfParaType.id, 1);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Setting PerformanceParameter properties failed.");
			}
		}
	}
}
