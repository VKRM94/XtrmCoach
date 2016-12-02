using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using XtrmCoachRESTServer;

// Testing methods which are exposed by API
namespace XtrmCoachRESTServerTest.RepositoryTest
{
	[TestClass]
	public class PerformanceParameterTypeGroupRepositoryTest
	{
		[TestMethod]
		public void Repository_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PerformanceParameterTypeGroupRepository performanceParameterTypeGroupRepository = new PerformanceParameterTypeGroupRepository();

				// Assert
				Assert.IsNotNull(performanceParameterTypeGroupRepository);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to initialize PerformanceParameterTypeGroupRepository.");
			}
		}

		[TestMethod]
		public void Repository_Get_All_PerformanceParameterTypeGroups_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterTypeGroupRepository performanceParameterTypeGroupRepository = new PerformanceParameterTypeGroupRepository();

				// Act
				ArrayList performanceParameterTypeGroupsFromDb = performanceParameterTypeGroupRepository.GetPerformanceParameterTypeGroups(4);

				// Assert
				Assert.IsTrue(performanceParameterTypeGroupsFromDb.Count > 0);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to fetch performance parameter types.");
			}
		}
	}
}