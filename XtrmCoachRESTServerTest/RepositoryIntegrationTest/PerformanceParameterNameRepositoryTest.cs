using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtrmCoachRESTServer;
using XtrmCoachRESTServer.Models;
using System.Collections;

// Testing methods which are exposed by API
namespace XtrmCoachRESTServerTest.RepositoryTest
{
	[TestClass]
	public class PerformanceParameterNameRepositoryTest
	{
		[TestMethod]
		public void Repository_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PerformanceParameterNameRepository performanceParameterNameRepository = new PerformanceParameterNameRepository();

				// Assert
				Assert.IsNotNull(performanceParameterNameRepository);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to initialize PerformanceParameterNameRepository.");
			}
		}

		[TestMethod]
		public void Repository_Get_PerformanceParameterName_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterNameRepository performanceParameterNameRepository = new PerformanceParameterNameRepository();

				// Act
				PerformanceParameterName performanceParameterNameFromDb = performanceParameterNameRepository.GetPerformanceParameterName(1);

				// Assert
				Assert.IsNotNull(performanceParameterNameFromDb);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to fetch performance parameter name.");
			}
		}

		[TestMethod]
		public void Repository_Get_All_PerformanceParameterNames_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterNameRepository performanceParameterNameRepository = new PerformanceParameterNameRepository();

				// Act
				ArrayList performanceParameterNamesFromDb = performanceParameterNameRepository.GetPerformanceParameterNames();

				// Assert
				Assert.IsTrue(performanceParameterNamesFromDb.Count > 0);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to fetch performance parameter names.");
			}
		}
	}
}