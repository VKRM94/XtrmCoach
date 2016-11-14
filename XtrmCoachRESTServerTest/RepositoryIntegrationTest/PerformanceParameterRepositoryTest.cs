using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using XtrmCoachRESTServer;
using XtrmCoachRESTServer.Models;

// Testing methods which are exposed by API
namespace XtrmCoachRESTServerTest.RepositoryTest
{
	[TestClass]
	public class PerformanceParameterRepositoryTest
	{
		[TestMethod]
		public void Repository_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PerformanceParameterRepository performanceParameterRepository = new PerformanceParameterRepository();

				// Assert
				Assert.IsNotNull(performanceParameterRepository);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to initialize PerformanceParameterRepository.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Get_Delete_PerformanceParameter_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterRepository performanceParameterRepository = new PerformanceParameterRepository();
				PerformanceParameter performanceParameterToSave = new PerformanceParameter();
				performanceParameterToSave.sportId = 1;
				performanceParameterToSave.perfParaName = new PerformanceParameterName();
				performanceParameterToSave.perfParaName.id = 1;
				performanceParameterToSave.customName = "custom";
				performanceParameterToSave.perfParaType = new PerformanceParameterType();
				performanceParameterToSave.perfParaType.id = 1;

				// Act
				long performanceParameterId = performanceParameterRepository.InsertPerformanceParameter(performanceParameterToSave);
				PerformanceParameter performanceParameterFromDb = performanceParameterRepository.GetPerformanceParameter(performanceParameterId);

				// Assert
				Assert.IsNotNull(performanceParameterFromDb);
				Assert.AreEqual(performanceParameterToSave.sportId, performanceParameterFromDb.sportId);
				Assert.AreEqual(performanceParameterToSave.perfParaName.id, performanceParameterFromDb.perfParaName.id);
				Assert.AreEqual(performanceParameterToSave.customName, performanceParameterFromDb.customName);
				Assert.AreEqual(performanceParameterToSave.perfParaType.id, performanceParameterFromDb.perfParaType.id);

				// Cleanup
				bool isDeleted = performanceParameterRepository.DeletePerformanceParameter(performanceParameterId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add new performance parameter.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Get_All_Delete_PerformanceParameter_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterRepository performanceParameterRepository = new PerformanceParameterRepository();
				PerformanceParameter performanceParameterToSave = new PerformanceParameter();
				performanceParameterToSave.sportId = 1;
				performanceParameterToSave.perfParaName = new PerformanceParameterName();
				performanceParameterToSave.perfParaName.id = 1;
				performanceParameterToSave.customName = "custom";
				performanceParameterToSave.perfParaType = new PerformanceParameterType();
				performanceParameterToSave.perfParaType.id = 1;

				// Act
				long performanceParameterId = performanceParameterRepository.InsertPerformanceParameter(performanceParameterToSave);
				ArrayList performanceParametersFromDb = performanceParameterRepository.GetPerformanceParameters(performanceParameterToSave.sportId);

				// Assert
				Assert.IsTrue(performanceParametersFromDb.Count > 0);
				Assert.AreEqual(((PerformanceParameter)performanceParametersFromDb[1]).sportId, performanceParameterToSave.sportId);
				Assert.AreEqual(((PerformanceParameter)performanceParametersFromDb[1]).perfParaName.id, performanceParameterToSave.perfParaName.id);
				Assert.AreEqual(((PerformanceParameter)performanceParametersFromDb[1]).customName, performanceParameterToSave.customName);
				Assert.AreEqual(((PerformanceParameter)performanceParametersFromDb[1]).perfParaType.id, performanceParameterToSave.perfParaType.id);

				// Cleanup
				bool isDeleted = performanceParameterRepository.DeletePerformanceParameter(performanceParameterId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add new performance parameter.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Update_Delete_PerformanceParameter_Test()
		{
			try
			{
				// Arrange
				PerformanceParameterRepository performanceParameterRepository = new PerformanceParameterRepository();
				PerformanceParameter performanceParameterToSave = new PerformanceParameter();
				performanceParameterToSave.sportId = 1;
				performanceParameterToSave.perfParaName = new PerformanceParameterName();
				performanceParameterToSave.perfParaName.id = 1;
				performanceParameterToSave.customName = "custom";
				performanceParameterToSave.perfParaType = new PerformanceParameterType();
				performanceParameterToSave.perfParaType.id = 1;

				PerformanceParameter performanceParameterToUpdate = new PerformanceParameter();
				performanceParameterToUpdate.sportId = 1;
				performanceParameterToUpdate.perfParaName = new PerformanceParameterName();
				performanceParameterToUpdate.perfParaName.id = 2;
				performanceParameterToUpdate.customName = "custom 2";
				performanceParameterToUpdate.perfParaType = new PerformanceParameterType();
				performanceParameterToUpdate.perfParaType.id = 5;

				// Act
				long performanceParameterId = performanceParameterRepository.InsertPerformanceParameter(performanceParameterToSave);
				performanceParameterToUpdate.id = performanceParameterId;
				bool isUpdated = performanceParameterRepository.UpdatePerformanceParameter(performanceParameterToUpdate);
				PerformanceParameter performanceParameterFromDb = performanceParameterRepository.GetPerformanceParameter(performanceParameterId);

				// Assert
				Assert.IsTrue(isUpdated);
				Assert.AreEqual(performanceParameterFromDb.sportId, performanceParameterToUpdate.sportId);
				Assert.AreEqual(performanceParameterFromDb.perfParaName.id, performanceParameterToUpdate.perfParaName.id);
				Assert.AreEqual(performanceParameterFromDb.customName, performanceParameterToUpdate.customName);
				Assert.AreEqual(performanceParameterFromDb.perfParaType.id, performanceParameterToUpdate.perfParaType.id);

				// Cleanup
				bool isDeleted = performanceParameterRepository.DeletePerformanceParameter(performanceParameterId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add new performance parameter.");
			}
		}
	}
}