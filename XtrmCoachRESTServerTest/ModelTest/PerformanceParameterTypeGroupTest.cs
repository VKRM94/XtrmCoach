using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class PerformanceParameterTypeGroupTest
	{
		[TestMethod]
		public void PerformanceParameterTypeGroup_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PerformanceParameterTypeGroup performanceParameterTypeGroup = new PerformanceParameterTypeGroup();

				// Assert
				Assert.IsNotNull(performanceParameterTypeGroup);
			}
			catch (Exception)
			{
				throw new AssertFailedException("PerformanceParameterTypeGroup initialization failed.");
			}
		}

		[TestMethod]
		public void PerformanceParameterTypeGroup_Property_Set_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterTypeGroup performanceParameterTypeGroup = new PerformanceParameterTypeGroup();

				// Act
				performanceParameterTypeGroup.id = 1;
				performanceParameterTypeGroup.name = "parameterTypeGroup";

				// Assert
				Assert.AreEqual<long>(performanceParameterTypeGroup.id, 1);
				Assert.AreEqual<string>(performanceParameterTypeGroup.name, "parameterTypeGroup");
			}
			catch (Exception)
			{
				throw new AssertFailedException("Setting PerformanceParameterTypeGroup properties failed.");
			}
		}
	}
}
