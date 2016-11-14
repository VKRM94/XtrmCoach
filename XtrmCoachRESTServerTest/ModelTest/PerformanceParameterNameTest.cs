using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class PerformanceParameterNameTest
	{
		[TestMethod]
		public void PerformanceParameterName_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PerformanceParameterName performanceParameterName = new PerformanceParameterName();

				// Assert
				Assert.IsNotNull(performanceParameterName);
			}
			catch (Exception)
			{
				throw new AssertFailedException("PerformanceParameterName initialization failed.");
			}
		}

		[TestMethod]
		public void PerformanceParameterName_Property_Set_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterName performanceParameterName = new PerformanceParameterName();

				// Act
				performanceParameterName.id = 1;
				performanceParameterName.name = "parameterName";

				// Assert
				Assert.AreEqual<long>(performanceParameterName.id, 1);
				Assert.AreEqual<string>(performanceParameterName.name, "parameterName");
			}
			catch (Exception)
			{
				throw new AssertFailedException("Setting PerformanceParameterName properties failed.");
			}
		}
	}
}
