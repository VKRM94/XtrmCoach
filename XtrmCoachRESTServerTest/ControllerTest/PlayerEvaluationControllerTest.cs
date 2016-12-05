using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Controller;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;
using XtrmCoachRESTServer.Util;

namespace XtrmCoachRESTServerTest.ControllerTest
{
	[TestClass]
	public class PlayerEvaluationControllerTest
	{
		/*
		[TestMethod]
		public void Get_All_PlayerEvaluations_Test()
		{
			// Arrange
			ArrayList PlayerEvaluationsObjsAsOuptut = new ArrayList();

			PlayerEvaluation PlayerEvaluationObj1AsOutput = new PlayerEvaluation();
			PlayerEvaluationObj1AsOutput.sportId = 1;
			PlayerEvaluationObj1AsOutput.playerId = 1;
			PlayerEvaluationObj1AsOutput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObj1AsOutput.perfParaName.id = 1;
			PlayerEvaluationObj1AsOutput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObj1AsOutput.selectedType.id = 1;
			PlayerEvaluationObj1AsOutput.value = "100";

			PlayerEvaluation PlayerEvaluationObj2AsOutput = new PlayerEvaluation();
			PlayerEvaluationObj2AsOutput.sportId = 1;
			PlayerEvaluationObj2AsOutput.playerId = 1;
			PlayerEvaluationObj2AsOutput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObj2AsOutput.perfParaName.id = 1;
			PlayerEvaluationObj2AsOutput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObj2AsOutput.selectedType.id = 1;
			PlayerEvaluationObj2AsOutput.value = "100";

			PlayerEvaluationsObjsAsOuptut.Add(PlayerEvaluationObj1AsOutput);
			PlayerEvaluationsObjsAsOuptut.Add(PlayerEvaluationObj2AsOutput);

			var mockRepository = new Mock<IPlayerEvaluationRepository>();
			mockRepository.Setup(x => x.GetPlayerEvaluations(1, 1))
				.Returns(PlayerEvaluationsObjsAsOuptut);

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();
			//controller.

			// Act
			ArrayList PlayerEvaluationsObj = controller.Get();

			// Assert
			Assert.AreEqual<int>(PlayerEvaluationsObj.Count, PlayerEvaluationsObjsAsOuptut.Count);
			Assert.AreEqual(PlayerEvaluationsObj[0], PlayerEvaluationsObjsAsOuptut[0]);
			Assert.AreEqual(PlayerEvaluationsObj[1], PlayerEvaluationsObjsAsOuptut[1]);
		}
		*/

		[TestMethod]
		public void Post_Valid_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.sportId = 1;
			PlayerEvaluationObjAsInput.playerId = 1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = 1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = 1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();
			mockRepository.Setup(x => x.InsertPlayerEvaluation(PlayerEvaluationObjAsInput))
				.Returns(1);

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "PlayerEvaluation added successfully.");
		}

		[TestMethod]
		public void Post_Null_Input_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPlayerEvaluationRepository>();

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(null);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
			Assert.AreEqual(responseStr, "Invalid JSON Passed.");
		}

		[TestMethod]
		public void Post_Invalid_SportId_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.sportId = -1;
			PlayerEvaluationObjAsInput.playerId = 1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = 1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = 1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Invalid Sport Id passed.");
		}

		[TestMethod]
		public void Post_Invalid_PlayerId_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.sportId = 1;
			PlayerEvaluationObjAsInput.playerId = -1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = 1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = 1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Invalid Player Id passed.");
		}

		[TestMethod]
		public void Post_Invalid_ParaNameId_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.sportId = 1;
			PlayerEvaluationObjAsInput.playerId = 1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = -1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = 1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Invalid Name Id passed.");
		}

		[TestMethod]
		public void Post_Invalid_ParaTypeId_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.sportId = 1;
			PlayerEvaluationObjAsInput.playerId = 1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = 1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = -1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Invalid Type Id passed.");
		}

		[TestMethod]
		public void Put_Valid_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.id = 1;
			PlayerEvaluationObjAsInput.sportId = 1;
			PlayerEvaluationObjAsInput.playerId = 1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = 1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = 1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();
			mockRepository.Setup(x => x.UpdatePlayerEvaluation(PlayerEvaluationObjAsInput))
				.Returns(true);

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "Updated PlayerEvaluation successfully.");
		}

		[TestMethod]
		public void Put_Null_Input_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPlayerEvaluationRepository>();

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(null);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
			Assert.AreEqual(responseStr, "Invalid JSON Passed.");
		}

		[TestMethod]
		public void Put_Invalid_Id_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.id = -1;
			PlayerEvaluationObjAsInput.sportId = 1;
			PlayerEvaluationObjAsInput.playerId = 1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = 1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = 1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();
			mockRepository.Setup(x => x.UpdatePlayerEvaluation(PlayerEvaluationObjAsInput))
				.Returns(true);

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
			Assert.AreEqual(responseStr, "Invalid Id Passed.");
		}

		[TestMethod]
		public void Put_Invalid_SportId_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.id = 1;
			PlayerEvaluationObjAsInput.sportId = -1;
			PlayerEvaluationObjAsInput.playerId = 1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = 1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = 1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();
			mockRepository.Setup(x => x.UpdatePlayerEvaluation(PlayerEvaluationObjAsInput))
				.Returns(true);

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Invalid Sport Id passed.");
		}

		[TestMethod]
		public void Put_Invalid_ParaNameId_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.id = 1;
			PlayerEvaluationObjAsInput.sportId = 1;
			PlayerEvaluationObjAsInput.playerId = 1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = -1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = 1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();
			mockRepository.Setup(x => x.UpdatePlayerEvaluation(PlayerEvaluationObjAsInput))
				.Returns(true);

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Invalid Name Id passed.");
		}

		[TestMethod]
		public void Put_Invalid_ParaTypeId_Test()
		{
			// Arrange
			PlayerEvaluation PlayerEvaluationObjAsInput = new PlayerEvaluation();
			PlayerEvaluationObjAsInput.id = 1;
			PlayerEvaluationObjAsInput.sportId = 1;
			PlayerEvaluationObjAsInput.playerId = 1;
			PlayerEvaluationObjAsInput.perfParaName = new PerformanceParameterName();
			PlayerEvaluationObjAsInput.perfParaName.id = 1;
			PlayerEvaluationObjAsInput.selectedType = new PerformanceParameterType();
			PlayerEvaluationObjAsInput.selectedType.id = -1;
			PlayerEvaluationObjAsInput.value = "100";

			var mockRepository = new Mock<IPlayerEvaluationRepository>();
			mockRepository.Setup(x => x.UpdatePlayerEvaluation(PlayerEvaluationObjAsInput))
				.Returns(true);

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(PlayerEvaluationObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Invalid Type Id passed.");
		}

		[TestMethod]
		public void Delete_Valid_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPlayerEvaluationRepository>();
			mockRepository.Setup(x => x.DeletePlayerEvaluation(1))
				.Returns(true);

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Delete(1);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "PlayerEvaluation successfully deleted.");
		}

		[TestMethod]
		public void Delete_Invalid_Id_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPlayerEvaluationRepository>();

			var controller = new PlayerEvaluationController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Delete(-1);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
			Assert.AreEqual(responseStr, "Invalid Id Passed.");
		}
	}
}