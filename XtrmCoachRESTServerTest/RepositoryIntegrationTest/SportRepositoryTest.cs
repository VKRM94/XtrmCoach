using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtrmCoachRESTServer;
using XtrmCoachRESTServer.Models;
using System.Collections;

// Testing methods which are exposed by API
namespace XtrmCoachRESTServerTest.RepositoryTest
{
	[TestClass]
	public class SportRepositoryTest
	{
		[TestMethod]
		public void Repository_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				SportRespository sportRepository = new SportRespository();

				// Assert
				Assert.IsNotNull(sportRepository);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to initialize SportRepository.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Get_Delete_Sport_Test()
		{
			try
			{
				// Arrange
				SportRespository sportRepository = new SportRespository();
				Sport sportToSave = new Sport();
				sportToSave.name = "Sport 1";
				sportToSave.userId = 1;

				// Act
				long sportId = sportRepository.InsertSport(sportToSave);
				Sport sportFromDb = sportRepository.GetSport(sportId);

				// Assert
				Assert.IsNotNull(sportFromDb);
				Assert.AreEqual(sportToSave.name, sportFromDb.name);
				Assert.AreEqual(sportToSave.userId, sportFromDb.userId);

				// Cleanup
				bool isDeleted = sportRepository.DeleteSport(sportId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add new sport.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Get_All_Delete_Sport_Test()
		{
			try
			{
				// Arrange
				SportRespository sportRepository = new SportRespository();
				Sport sportToSave = new Sport();
				sportToSave.name = "Sport 1";
				sportToSave.userId = 1;

				// Act
				long sportId = sportRepository.InsertSport(sportToSave);
				ArrayList sportsFromDb = sportRepository.GetSports();

				// Assert
				Assert.IsTrue(sportsFromDb.Count > 0);
				Assert.AreEqual(((Sport)sportsFromDb[0]).name, sportToSave.name);
				Assert.AreEqual(((Sport)sportsFromDb[0]).userId, sportToSave.userId);

				// Cleanup
				bool isDeleted = sportRepository.DeleteSport(sportId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add new sport.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Update_Delete_Sport_Test()
		{
			try
			{
				// Arrange
				SportRespository sportRepository = new SportRespository();
				Sport sportToSave = new Sport();
				sportToSave.name = "Sport 1";
				sportToSave.userId = 1;

				Sport sportToUpdate = new Sport();
				sportToUpdate.name = "Sport 2";
				sportToUpdate.userId = 1;

				// Act
				long sportId = sportRepository.InsertSport(sportToSave);
				sportToUpdate.id = sportId;
				bool isUpdated = sportRepository.UpdateSport(sportToUpdate);
				Sport sportFromDb = sportRepository.GetSport(sportId);

				// Assert
				Assert.IsTrue(isUpdated);
				Assert.AreEqual(sportFromDb.name, sportToUpdate.name);
				Assert.AreEqual(sportFromDb.userId, sportToUpdate.userId);

				// Cleanup
				bool isDeleted = sportRepository.DeleteSport(sportId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add new sport.");
			}
		}
	}
}