using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Controllers;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;
using XtrmCoachRESTServer.Util;

namespace XtrmCoachRESTServerTest.ControllerTest
{
	[TestClass]
	public class LoginControllerTest
	{
		[TestMethod]
		public void Post_Valid_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.emailId = "team2@uncc.edu";
			userObjAsInput.password = "team2password";

			User userObjExpcetedAsResponse = new User();
			userObjExpcetedAsResponse.emailId = "team2@uncc.edu";
			userObjExpcetedAsResponse.firstName = "Team";
			userObjExpcetedAsResponse.lastName = "2";
			userObjExpcetedAsResponse.isAdmin = false;

			var mockRepository = new Mock<IUserRepository>();
			mockRepository.Setup(x => x.AuthenticateUser(userObjAsInput.emailId, userObjAsInput.password))
				.Returns(userObjExpcetedAsResponse);

			var controller = new LoginController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			User userObjInResponse = Helper.Deserialize<User>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.IsTrue(Object.Equals(userObjExpcetedAsResponse, userObjInResponse));
		}

		[TestMethod]
		public void Post_Null_Input_Test()
		{
			// Arrange
			var mockRepository = new Mock<IUserRepository>();

			var controller = new LoginController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(null);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
			Assert.AreEqual(responseStr, "Invalid JSON Passed.");
		}

		[TestMethod]
		public void Post_Empty_Username_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.emailId = "";
			userObjAsInput.password = "team2password";

			var mockRepository = new Mock<IUserRepository>();

			var controller = new LoginController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
			Assert.AreEqual(responseStr, "User Name is empty.");
		}

		[TestMethod]
		public void Post_Empty_Password_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.emailId = "team2@uncc.edu";
			userObjAsInput.password = "";

			var mockRepository = new Mock<IUserRepository>();

			var controller = new LoginController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
			Assert.AreEqual(responseStr, "User Password is empty.");
		}

		[TestMethod]
		public void Post_InValid_User_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.emailId = "team200@uncc.edu";
			userObjAsInput.password = "team2password";

			var mockRepository = new Mock<IUserRepository>();
			mockRepository.Setup(x => x.AuthenticateUser(userObjAsInput.emailId, userObjAsInput.password))
				.Returns((User)null);

			var controller = new LoginController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
			Assert.AreEqual(responseStr, "User not present.");
		}
	}
}
