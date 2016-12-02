using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class PerformanceParameterTypeTest
	{
		[TestMethod]
		public void PerformanceParameterType_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PerformanceParameterType performanceParameterType = new PerformanceParameterType();

				// Assert
				Assert.IsNotNull(performanceParameterType);
			}
			catch (Exception)
			{
				throw new AssertFailedException("PerformanceParameterType initialization failed.");
			}
		}

		[TestMethod]
		public void PerformanceParameterType_Property_Set_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterType performanceParameterType = new PerformanceParameterType();

				// Act
				performanceParameterType.id = 1;
				performanceParameterType.groupId = 1;
				performanceParameterType.name = "parameterType";

				// Assert
				Assert.AreEqual<long>(performanceParameterType.id, 1);
				Assert.AreEqual<long>(performanceParameterType.groupId, 1);
				Assert.AreEqual<string>(performanceParameterType.name, "parameterType");
			}
			catch (Exception)
			{
				throw new AssertFailedException("Setting PerformanceParameterType properties failed.");
			}
		}
	}
}
