using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtrmCoachRESTServer;
using XtrmCoachRESTServer.Models;
using System.Collections;
using System.Collections.Generic;

// Testing methods which are exposed by API
namespace XtrmCoachRESTServerTest.RepositoryTest
{
	[TestClass]
	public class PlayerRepositoryTest
	{
		[TestMethod]
		public void Repository_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PlayerRespository playerRepository = new PlayerRespository();

				// Assert
				Assert.IsNotNull(playerRepository);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to initialize PlayerRepository.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Get_Delete_Player_Test()
		{
			try
			{
				// Arrange
				PlayerRespository playerRepository = new PlayerRespository();
				Player playerToSave = new Player();
				playerToSave.firstName = "FirstName";
				playerToSave.lastName = "LastName";
				playerToSave.dob = new DateTime(2016, 11, 25);
				playerToSave.imageId = "ImageId";
				playerToSave.sports = new List<Sport>();
				playerToSave.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

				// Act
				long playerId = playerRepository.InsertPlayer(playerToSave);
				Player playerFromDb = playerRepository.GetPlayer(playerId);

				// Assert
				Assert.IsNotNull(playerFromDb);
				Assert.AreEqual(playerToSave.firstName, playerFromDb.firstName);
				Assert.AreEqual(playerToSave.lastName, playerFromDb.lastName);
				Assert.AreEqual(playerToSave.dob, playerFromDb.dob);
				Assert.AreEqual(playerToSave.imageId, playerFromDb.imageId);
				Assert.AreEqual(playerToSave.sports[0], playerFromDb.sports[0]);

				// Cleanup
				bool isDeleted = playerRepository.DeletePlayer(playerId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to get/add/delete player.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Get_All_Delete_Player_Test()
		{
			try
			{
				// Arrange
				PlayerRespository playerRepository = new PlayerRespository();
				Player playerToSave = new Player();
				playerToSave.firstName = "FirstName";
				playerToSave.lastName = "LastName";
				playerToSave.dob = new DateTime(2016, 11, 25);
				playerToSave.imageId = "ImageId";
				playerToSave.sports = new List<Sport>();
				playerToSave.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

				// Act
				long playerId = playerRepository.InsertPlayer(playerToSave);
				ArrayList playersFromDb = playerRepository.GetPlayers(1);

				// Assert
				Assert.IsTrue(playersFromDb.Count > 0);
				Assert.AreEqual(((Player)playersFromDb[2]).firstName, playerToSave.firstName);
				Assert.AreEqual(((Player)playersFromDb[2]).lastName, playerToSave.lastName);
				Assert.AreEqual(((Player)playersFromDb[2]).dob, playerToSave.dob);
				Assert.AreEqual(((Player)playersFromDb[2]).imageId, playerToSave.imageId);
				Assert.AreEqual(((Player)playersFromDb[2]).sports[0], playerToSave.sports[0]);

				// Cleanup
				bool isDeleted = playerRepository.DeletePlayer(playerId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add/get all/delete player.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Update_Delete_Player_Test()
		{
			try
			{
				// Arrange
				PlayerRespository playerRepository = new PlayerRespository();
				Player playerToSave = new Player();
				playerToSave.firstName = "FirstName";
				playerToSave.lastName = "LastName";
				playerToSave.dob = new DateTime(2016, 11, 25);
				playerToSave.imageId = "ImageId";
				playerToSave.sports = new List<Sport>();
				playerToSave.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

				Player playerToUpdate = new Player();
				playerToUpdate.firstName = "FirstName1";
				playerToUpdate.lastName = "LastName1";
				playerToUpdate.dob = new DateTime(2016, 11, 26);
				playerToUpdate.imageId = "ImageId1";
				playerToUpdate.sports = new List<Sport>();
				playerToUpdate.sports.Add(new Sport() { id = 2, name = "Sport 2", userId = 1 });

				// Act
				long playerId = playerRepository.InsertPlayer(playerToSave);
				playerToUpdate.id = playerId;
				bool isUpdated = playerRepository.UpdatePlayer(playerToUpdate);
				Player playerFromDb = playerRepository.GetPlayer(playerId);

				// Assert
				Assert.IsTrue(isUpdated);
				Assert.AreEqual(playerToUpdate.firstName, playerFromDb.firstName);
				Assert.AreEqual(playerToUpdate.lastName, playerFromDb.lastName);
				Assert.AreEqual(playerToUpdate.dob, playerFromDb.dob);
				Assert.AreEqual(playerToUpdate.imageId, playerFromDb.imageId);
				Assert.AreEqual(playerToUpdate.sports[0], playerFromDb.sports[0]);

				// Cleanup
				bool isDeleted = playerRepository.DeletePlayer(playerId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception ex)
			{
				throw new AssertFailedException("Unable to add/update/delete player.");
			}
		}
	}
}