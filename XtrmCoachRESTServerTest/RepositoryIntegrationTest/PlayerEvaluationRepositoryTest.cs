using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using XtrmCoachRESTServer;
using XtrmCoachRESTServer.Models;

// Testing methods which are exposed by API
namespace XtrmCoachRESTServerTest.RepositoryTest
{
	[TestClass]
	public class PlayerEvaluationRepositoryTest
	{
		[TestMethod]
		public void Repository_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PlayerEvaluationRepository PlayerEvaluationRepository = new PlayerEvaluationRepository();

				// Assert
				Assert.IsNotNull(PlayerEvaluationRepository);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to initialize PlayerEvaluationRepository.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Get_Delete_PlayerEvaluation_Test()
		{
			try
			{
				// Arrange
				PlayerEvaluationRepository PlayerEvaluationRepository = new PlayerEvaluationRepository();
				PlayerEvaluation PlayerEvaluationToSave = new PlayerEvaluation();
				PlayerEvaluationToSave.sportId = 1;
				PlayerEvaluationToSave.playerId = 1;
				PlayerEvaluationToSave.perfParaName = new PerformanceParameterName();
				PlayerEvaluationToSave.perfParaName.id = 1;
				PlayerEvaluationToSave.selectedType = new PerformanceParameterType();
				PlayerEvaluationToSave.selectedType.id = 1;
				PlayerEvaluationToSave.value = "100";

				// Act
				long PlayerEvaluationId = PlayerEvaluationRepository.InsertPlayerEvaluation(PlayerEvaluationToSave);
				PlayerEvaluation PlayerEvaluationFromDb = PlayerEvaluationRepository.GetPlayerEvaluation(PlayerEvaluationId);

				// Assert
				Assert.IsNotNull(PlayerEvaluationFromDb);
				Assert.AreEqual(PlayerEvaluationToSave.sportId, PlayerEvaluationFromDb.sportId);
				Assert.AreEqual(PlayerEvaluationToSave.playerId, PlayerEvaluationFromDb.playerId);
				Assert.AreEqual(PlayerEvaluationToSave.perfParaName.id, PlayerEvaluationFromDb.perfParaName.id);
				Assert.AreEqual(PlayerEvaluationToSave.selectedType.id, PlayerEvaluationFromDb.selectedType.id);
				Assert.AreEqual(PlayerEvaluationToSave.value, PlayerEvaluationFromDb.value);

				// Cleanup
				bool isDeleted = PlayerEvaluationRepository.DeletePlayerEvaluation(PlayerEvaluationId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add/fetch/delete player evaluation.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Get_All_Delete_PlayerEvaluation_Test()
		{
			try
			{
				// Arrange
				PlayerEvaluationRepository PlayerEvaluationRepository = new PlayerEvaluationRepository();
				PlayerEvaluation PlayerEvaluationToSave = new PlayerEvaluation();
				PlayerEvaluationToSave.sportId = 1;
				PlayerEvaluationToSave.playerId = 1;
				PlayerEvaluationToSave.perfParaName = new PerformanceParameterName();
				PlayerEvaluationToSave.perfParaName.id = 1;
				PlayerEvaluationToSave.selectedType = new PerformanceParameterType();
				PlayerEvaluationToSave.selectedType.id = 1;
				PlayerEvaluationToSave.value = "100";

				// Act
				long PlayerEvaluationId = PlayerEvaluationRepository.InsertPlayerEvaluation(PlayerEvaluationToSave);
				ArrayList PlayerEvaluationsFromDb = PlayerEvaluationRepository.GetPlayerEvaluations(PlayerEvaluationToSave.sportId, PlayerEvaluationToSave.playerId);

				// Assert
				Assert.IsTrue(PlayerEvaluationsFromDb.Count > 0);
				Assert.AreEqual(((PlayerEvaluation)PlayerEvaluationsFromDb[0]).sportId, PlayerEvaluationToSave.sportId);
				Assert.AreEqual(((PlayerEvaluation)PlayerEvaluationsFromDb[0]).playerId, PlayerEvaluationToSave.playerId);
				Assert.AreEqual(((PlayerEvaluation)PlayerEvaluationsFromDb[0]).perfParaName.id, PlayerEvaluationToSave.perfParaName.id);
				Assert.AreEqual(((PlayerEvaluation)PlayerEvaluationsFromDb[0]).selectedType.id, PlayerEvaluationToSave.selectedType.id);
				Assert.AreEqual(((PlayerEvaluation)PlayerEvaluationsFromDb[0]).value, PlayerEvaluationToSave.value);

				// Cleanup
				bool isDeleted = PlayerEvaluationRepository.DeletePlayerEvaluation(PlayerEvaluationId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add/fetch all/detete player evaluation.");
			}
		}

		[TestMethod]
		public void Repository_Insert_Update_Delete_PlayerEvaluation_Test()
		{
			try
			{
				// Arrange
				PlayerEvaluationRepository PlayerEvaluationRepository = new PlayerEvaluationRepository();
				PlayerEvaluation PlayerEvaluationToSave = new PlayerEvaluation();
				PlayerEvaluationToSave.sportId = 1;
				PlayerEvaluationToSave.playerId = 1;
				PlayerEvaluationToSave.perfParaName = new PerformanceParameterName();
				PlayerEvaluationToSave.perfParaName.id = 1;
				PlayerEvaluationToSave.selectedType = new PerformanceParameterType();
				PlayerEvaluationToSave.selectedType.id = 1;
				PlayerEvaluationToSave.value = "100";

				PlayerEvaluation PlayerEvaluationToUpdate = new PlayerEvaluation();
				PlayerEvaluationToUpdate.sportId = 1;
				PlayerEvaluationToUpdate.playerId = 1;
				PlayerEvaluationToUpdate.perfParaName = new PerformanceParameterName();
				PlayerEvaluationToUpdate.perfParaName.id = 1;
				PlayerEvaluationToUpdate.selectedType = new PerformanceParameterType();
				PlayerEvaluationToUpdate.selectedType.id = 2;
				PlayerEvaluationToUpdate.value = "200";

				// Act
				long PlayerEvaluationId = PlayerEvaluationRepository.InsertPlayerEvaluation(PlayerEvaluationToSave);
				PlayerEvaluationToUpdate.id = PlayerEvaluationId;
				bool isUpdated = PlayerEvaluationRepository.UpdatePlayerEvaluation(PlayerEvaluationToUpdate);
				PlayerEvaluation PlayerEvaluationFromDb = PlayerEvaluationRepository.GetPlayerEvaluation(PlayerEvaluationId);

				// Assert
				Assert.IsTrue(isUpdated);
				Assert.IsNotNull(PlayerEvaluationFromDb);
				Assert.AreEqual(PlayerEvaluationToUpdate.sportId, PlayerEvaluationFromDb.sportId);
				Assert.AreEqual(PlayerEvaluationToUpdate.playerId, PlayerEvaluationFromDb.playerId);
				Assert.AreEqual(PlayerEvaluationToUpdate.perfParaName.id, PlayerEvaluationFromDb.perfParaName.id);
				Assert.AreEqual(PlayerEvaluationToUpdate.selectedType.id, PlayerEvaluationFromDb.selectedType.id);
				Assert.AreEqual(PlayerEvaluationToUpdate.value, PlayerEvaluationFromDb.value);

				// Cleanup
				bool isDeleted = PlayerEvaluationRepository.DeletePlayerEvaluation(PlayerEvaluationId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception ex)
			{
				throw new AssertFailedException("Unable to add/fetch/update/delete player evaluation.");
			}
		}
	}
}