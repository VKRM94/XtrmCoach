using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
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
	public class PlayerControllerTest
	{
		[TestMethod]
		public void Get_All_Players_Test()
		{
			// Arrange
			ArrayList playersObjsAsOuptut = new ArrayList();

			Player playerObj1AsOutput = new Player();
			playerObj1AsOutput.firstName = "FirstName1";
			playerObj1AsOutput.lastName = "LastName1";
			playerObj1AsOutput.dob = new DateTime(2016, 11, 25);
			playerObj1AsOutput.imageId = "ImageId1";
			playerObj1AsOutput.sports = new List<Sport>();
			playerObj1AsOutput.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

			Player playerObj2AsOutput = new Player();
			playerObj2AsOutput.firstName = "FirstName2";
			playerObj2AsOutput.lastName = "LastName2";
			playerObj2AsOutput.dob = new DateTime(2016, 11, 25);
			playerObj2AsOutput.imageId = "ImageId2";
			playerObj2AsOutput.sports = new List<Sport>();
			playerObj2AsOutput.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

			playersObjsAsOuptut.Add(playerObj1AsOutput);
			playersObjsAsOuptut.Add(playerObj2AsOutput);

			var mockRepository = new Mock<IPlayerRepository>();
			mockRepository.Setup(x => x.GetPlayers(1))
				.Returns(playersObjsAsOuptut);

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			ArrayList playersObj = controller.Get(1);

			// Assert
			Assert.AreEqual<int>(playersObj.Count, playersObjsAsOuptut.Count);
			Assert.AreEqual(playersObj[0], playersObjsAsOuptut[0]);
			Assert.AreEqual(playersObj[1], playersObjsAsOuptut[1]);
		}

		[TestMethod]
		public void Post_Valid_Test()
		{
			// Arrange
			Player playerObjAsInput = new Player();
			playerObjAsInput.firstName = "FirstName";
			playerObjAsInput.lastName = "LastName";
			playerObjAsInput.dob = new DateTime(2016, 11, 25);
			playerObjAsInput.imageId = "ImageId";
			playerObjAsInput.sports = new List<Sport>();
			playerObjAsInput.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

			var mockRepository = new Mock<IPlayerRepository>();
			mockRepository.Setup(x => x.InsertPlayer(playerObjAsInput))
				.Returns(1);

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(playerObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "Player added successfully.");
		}

		[TestMethod]
		public void Post_Null_Input_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPlayerRepository>();

			var controller = new PlayerController(mockRepository.Object);
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
		public void Post_Empty_Player_FirstName_Test()
		{
			// Arrange
			Player playerObjAsInput = new Player();
			playerObjAsInput.firstName = "";
			playerObjAsInput.lastName = "LastName";
			playerObjAsInput.dob = new DateTime(2016, 11, 25);
			playerObjAsInput.imageId = "ImageId";
			playerObjAsInput.sports = new List<Sport>();
			playerObjAsInput.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

			var mockRepository = new Mock<IPlayerRepository>();

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(playerObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Player First Name is empty.");
		}

		[TestMethod]
		public void Post_Empty_Player_LastName_Test()
		{
			// Arrange
			Player playerObjAsInput = new Player();
			playerObjAsInput.firstName = "FirstName";
			playerObjAsInput.lastName = "";
			playerObjAsInput.dob = new DateTime(2016, 11, 25);
			playerObjAsInput.imageId = "ImageId";
			playerObjAsInput.sports = new List<Sport>();
			playerObjAsInput.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

			var mockRepository = new Mock<IPlayerRepository>();

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(playerObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Player Last Name is empty.");
		}

		[TestMethod]
		public void Post_Empty_Player_DOB_Test()
		{
			// Arrange
			Player playerObjAsInput = new Player();
			playerObjAsInput.firstName = "FirstName";
			playerObjAsInput.lastName = "LastName";
			playerObjAsInput.imageId = "ImageId";
			playerObjAsInput.sports = new List<Sport>();
			playerObjAsInput.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

			var mockRepository = new Mock<IPlayerRepository>();

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(playerObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Player Date of Birth is empty.");
		}

		[TestMethod]
		public void Post_Empty_Player_ImageId_Test()
		{
			// Arrange
			Player playerObjAsInput = new Player();
			playerObjAsInput.firstName = "FirstName";
			playerObjAsInput.lastName = "LastName";
			playerObjAsInput.dob = new DateTime(2016, 11, 25);
			playerObjAsInput.imageId = "";
			playerObjAsInput.sports = new List<Sport>();
			playerObjAsInput.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

			var mockRepository = new Mock<IPlayerRepository>();

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(playerObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Player Image Id is empty.");
		}

		[TestMethod]
		public void Post_Empty_Player_SportList_Test()
		{
			// Arrange
			Player playerObjAsInput = new Player();
			playerObjAsInput.firstName = "FirstName";
			playerObjAsInput.lastName = "LastName";
			playerObjAsInput.dob = new DateTime(2016, 11, 25);
			playerObjAsInput.imageId = "ImageId";

			var mockRepository = new Mock<IPlayerRepository>();

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(playerObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Player Sport Info is empty.");
		}

		[TestMethod]
		public void Put_Valid_Test()
		{
			// Arrange
			Player playerObjAsInput = new Player();
			playerObjAsInput.id = 1;
			playerObjAsInput.firstName = "FirstName";
			playerObjAsInput.lastName = "LastName";
			playerObjAsInput.dob = new DateTime(2016, 11, 25);
			playerObjAsInput.imageId = "ImageId";
			playerObjAsInput.sports = new List<Sport>();
			playerObjAsInput.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

			var mockRepository = new Mock<IPlayerRepository>();
			mockRepository.Setup(x => x.UpdatePlayer(playerObjAsInput))
				.Returns(true);

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(playerObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "Updated Player successfully.");
		}

		[TestMethod]
		public void Put_InValid_Id_Test()
		{
			// Arrange
			Player playerObjAsInput = new Player();
			playerObjAsInput.id = -1;
			playerObjAsInput.firstName = "FirstName";
			playerObjAsInput.lastName = "LastName";
			playerObjAsInput.dob = new DateTime(2016, 11, 25);
			playerObjAsInput.imageId = "ImageId";
			playerObjAsInput.sports = new List<Sport>();
			playerObjAsInput.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

			var mockRepository = new Mock<IPlayerRepository>();

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(playerObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Player Id is Invalid.");
		}

		[TestMethod]
		public void Delete_Valid_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPlayerRepository>();
			mockRepository.Setup(x => x.DeletePlayer(1))
				.Returns(true);

			var controller = new PlayerController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Delete(1);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "Player successfully deleted.");
		}
	}
}