using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
	public class SignupControllerTest
	{
		[TestMethod]
		public void Post_Valid_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.firstName = "Team";
			userObjAsInput.lastName = "2";
			userObjAsInput.emailId = "team2@uncc.edu";
			userObjAsInput.password = "team2password";

			var mockRepository = new Mock<IUserRepository>();
			mockRepository.Setup(x => x.SaveUser(userObjAsInput))
				.Returns(1);

			var controller = new SignupController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "User created successfully.");
		}

		[TestMethod]
		public void Post_Null_Input_Test()
		{
			// Arrange
			var mockRepository = new Mock<IUserRepository>();

			var controller = new SignupController(mockRepository.Object);
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
		public void Post_Empty_Firstname_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.firstName = "";
			userObjAsInput.lastName = "2";
			userObjAsInput.emailId = "team2@uncc.edu";
			userObjAsInput.password = "team2password";

			var mockRepository = new Mock<IUserRepository>();

			var controller = new SignupController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "User First Name is empty.");
		}

		[TestMethod]
		public void Post_Empty_Lastname_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.firstName = "Team";
			userObjAsInput.lastName = "";
			userObjAsInput.emailId = "team2@uncc.edu";
			userObjAsInput.password = "team2password";

			var mockRepository = new Mock<IUserRepository>();

			var controller = new SignupController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "User Last Name is empty.");
		}

		[TestMethod]
		public void Post_Empty_Email_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.firstName = "Team";
			userObjAsInput.lastName = "2";
			userObjAsInput.emailId = "";
			userObjAsInput.password = "team2password";

			var mockRepository = new Mock<IUserRepository>();

			var controller = new SignupController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "User Email is empty.");
		}

		[TestMethod]
		public void Post_Empty_Password_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.firstName = "Team";
			userObjAsInput.lastName = "2";
			userObjAsInput.emailId = "team2@uncc.edu";
			userObjAsInput.password = "";

			var mockRepository = new Mock<IUserRepository>();

			var controller = new SignupController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "User Password is empty.");
		}

		[TestMethod]
		public void Post_User_Already_Exists_Test()
		{
			// Arrange
			User userObjAsInput = new User();
			userObjAsInput.firstName = "Team";
			userObjAsInput.lastName = "2";
			userObjAsInput.emailId = "team2@uncc.edu";
			userObjAsInput.password = "team2password";

			var mockRepository = new Mock<IUserRepository>();
			mockRepository.Setup(x => x.SaveUser(userObjAsInput))
				.Returns(-1);

			var controller = new SignupController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(userObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.Conflict);
			Assert.AreEqual(responseStr, "User already present.");
		}
	}
}
