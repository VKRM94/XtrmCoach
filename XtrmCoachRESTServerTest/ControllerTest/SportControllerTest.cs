using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Controller;
using XtrmCoachRESTServer.Controllers;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;
using XtrmCoachRESTServer.Util;

namespace XtrmCoachRESTServerTest.ControllerTest
{
	[TestClass]
	public class SportControllerTest
	{
		/*
		[TestMethod]
		public void Get_All_Sports_Test()
		{
			// Arrange
			ArrayList sportsObjsAsOuptut = new ArrayList();

			Sport sportObj1AsOutput = new Sport();
			sportObj1AsOutput.name = "Sport 1";
			sportObj1AsOutput.userId = 1;

			Sport sportObj2AsOutput = new Sport();
			sportObj2AsOutput.name = "Sport 2";
			sportObj2AsOutput.userId = 2;

			sportsObjsAsOuptut.Add(sportObj1AsOutput);
			sportsObjsAsOuptut.Add(sportObj2AsOutput);

			var mockRepository = new Mock<ISportRepository>();
			mockRepository.Setup(x => x.GetSports(1))
				.Returns(sportsObjsAsOuptut);

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			ArrayList sportsObj = controller.Get();

			// Assert
			Assert.AreEqual<int>(sportsObj.Count, sportsObjsAsOuptut.Count);
			Assert.AreEqual(sportsObj[0], sportsObjsAsOuptut[0]);
			Assert.AreEqual(sportsObj[1], sportsObjsAsOuptut[1]);
		}
		*/

		[TestMethod]
		public void Get_Sport_Test()
		{
			// Arrange
			Sport sportObjAsOutput = new Sport();
			sportObjAsOutput.name = "Sport 1";
			sportObjAsOutput.userId = 1;

			var mockRepository = new Mock<ISportRepository>();
			mockRepository.Setup(x => x.GetSport(1))
				.Returns(sportObjAsOutput);

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			Sport sportsObj = controller.Get(1);

			// Assert
			Assert.AreEqual<Sport>(sportsObj, sportObjAsOutput);
		}

		[TestMethod]
		public void Post_Valid_Test()
		{
			// Arrange
			Sport sportObjAsInput = new Sport();
			sportObjAsInput.name = "Sport 1";
			sportObjAsInput.userId = 1;

			var mockRepository = new Mock<ISportRepository>();
			mockRepository.Setup(x => x.InsertSport(sportObjAsInput))
				.Returns(1);

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(sportObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "Sport added successfully.");
		}

		[TestMethod]
		public void Post_Null_Input_Test()
		{
			// Arrange
			var mockRepository = new Mock<ISportRepository>();

			var controller = new SportController(mockRepository.Object);
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
		public void Post_Empty_SportName_Test()
		{
			// Arrange
			Sport sportObjAsInput = new Sport();
			sportObjAsInput.name = "";
			sportObjAsInput.userId = 1;

			var mockRepository = new Mock<ISportRepository>();

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(sportObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Sport Name is empty.");
		}

		[TestMethod]
		public void Post_Invalid_UserId_Test()
		{
			// Arrange
			Sport sportObjAsInput = new Sport();
			sportObjAsInput.name = "Sport 1";
			sportObjAsInput.userId = -1;

			var mockRepository = new Mock<ISportRepository>();

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(sportObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "User Id is empty.");
		}

		[TestMethod]
		public void Put_Valid_Test()
		{
			// Arrange
			Sport sportObjAsInput = new Sport();
			sportObjAsInput.id = 1;
			sportObjAsInput.name = "Sport 1";
			sportObjAsInput.userId = 1;

			var mockRepository = new Mock<ISportRepository>();
			mockRepository.Setup(x => x.UpdateSport(sportObjAsInput))
				.Returns(true);

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(sportObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "Updated Sport successfully.");
		}

		[TestMethod]
		public void Put_InValid_Id_Test()
		{
			// Arrange
			Sport sportObjAsInput = new Sport();
			sportObjAsInput.id = -1;
			sportObjAsInput.name = "Sport 1";
			sportObjAsInput.userId = 1;

			var mockRepository = new Mock<ISportRepository>();

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(sportObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Sport Id is Invalid.");
		}

		[TestMethod]
		public void Put_Empty_SportName_Test()
		{
			// Arrange
			Sport sportObjAsInput = new Sport();
			sportObjAsInput.id = 1;
			sportObjAsInput.name = "";
			sportObjAsInput.userId = 1;

			var mockRepository = new Mock<ISportRepository>();

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(sportObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Sport Name is empty.");
		}

		[TestMethod]
		public void Put_InValid_UserId_Test()
		{
			// Arrange
			Sport sportObjAsInput = new Sport();
			sportObjAsInput.id = 1;
			sportObjAsInput.name = "Sport 1";
			sportObjAsInput.userId = -1;

			var mockRepository = new Mock<ISportRepository>();

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(sportObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "User Id is invalid.");
		}

		[TestMethod]
		public void Delete_Valid_Test()
		{
			// Arrange
			var mockRepository = new Mock<ISportRepository>();
			mockRepository.Setup(x => x.DeleteSport(1))
				.Returns(true);

			var controller = new SportController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Delete(1);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "Sport successfully deleted.");
		}
	}
}