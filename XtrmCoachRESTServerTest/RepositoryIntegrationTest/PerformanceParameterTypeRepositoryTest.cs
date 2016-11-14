using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtrmCoachRESTServer;
using XtrmCoachRESTServer.Models;
using System.Collections;

// Testing methods which are exposed by API
namespace XtrmCoachRESTServerTest.RepositoryTest
{
	[TestClass]
	public class PerformanceParameterTypeRepositoryTest
	{
		[TestMethod]
		public void Repository_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PerformanceParameterTypeRepository performanceParameterTypeRepository = new PerformanceParameterTypeRepository();

				// Assert
				Assert.IsNotNull(performanceParameterTypeRepository);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to initialize PerformanceParameterTypeRepository.");
			}
		}

		[TestMethod]
		public void Repository_Get_All_PerformanceParameterTypes_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterTypeRepository performanceParameterTypeRepository = new PerformanceParameterTypeRepository();

				// Act
				ArrayList performanceParameterTypesFromDb = performanceParameterTypeRepository.GetPerformanceParameterTypes(1);

				// Assert
				Assert.IsTrue(performanceParameterTypesFromDb.Count > 0);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to fetch performance parameter types.");
			}
		}
	}
}