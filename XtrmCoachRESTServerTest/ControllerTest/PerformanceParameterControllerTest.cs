using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
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
	public class PerformanceParameterControllerTest
	{
		[TestMethod]
		public void Get_All_PerformanceParameters_Test()
		{
			// Arrange
			ArrayList performanceParametersObjsAsOuptut = new ArrayList();

			PerformanceParameter performanceParameterObj1AsOutput = new PerformanceParameter();
			performanceParameterObj1AsOutput.sportId = 1;
			performanceParameterObj1AsOutput.perfParaName = new PerformanceParameterName();
			performanceParameterObj1AsOutput.perfParaName.id = 1;
			performanceParameterObj1AsOutput.perfParaName.name = "Parameter 1";
			performanceParameterObj1AsOutput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObj1AsOutput.perfParaTypeGroup.id = 1;
			performanceParameterObj1AsOutput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObj1AsOutput.customName = "Custom 1";

			PerformanceParameter performanceParameterObj2AsOutput = new PerformanceParameter();
			performanceParameterObj2AsOutput.sportId = 1;
			performanceParameterObj2AsOutput.perfParaName = new PerformanceParameterName();
			performanceParameterObj2AsOutput.perfParaName.id = 1;
			performanceParameterObj2AsOutput.perfParaName.name = "Parameter 1";
			performanceParameterObj2AsOutput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObj2AsOutput.perfParaTypeGroup.id = 1;
			performanceParameterObj2AsOutput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObj2AsOutput.customName = "Custom 1";

			performanceParametersObjsAsOuptut.Add(performanceParameterObj1AsOutput);
			performanceParametersObjsAsOuptut.Add(performanceParameterObj2AsOutput);

			var mockRepository = new Mock<IPerformanceParameterRepository>();
			mockRepository.Setup(x => x.GetPerformanceParameters(1))
				.Returns(performanceParametersObjsAsOuptut);

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			ArrayList performanceParametersObj = controller.Get(1);

			// Assert
			Assert.AreEqual<int>(performanceParametersObj.Count, performanceParametersObjsAsOuptut.Count);
			Assert.AreEqual(performanceParametersObj[0], performanceParametersObjsAsOuptut[0]);
			Assert.AreEqual(performanceParametersObj[1], performanceParametersObjsAsOuptut[1]);
		}

		[TestMethod]
		public void Post_Valid_Test()
		{
			// Arrange
			PerformanceParameter performanceParameterObjAsInput = new PerformanceParameter();
			performanceParameterObjAsInput.sportId = 1;
			performanceParameterObjAsInput.perfParaName = new PerformanceParameterName();
			performanceParameterObjAsInput.perfParaName.id = 1;
			performanceParameterObjAsInput.perfParaName.name = "Parameter 1";
			performanceParameterObjAsInput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObjAsInput.perfParaTypeGroup.id = 1;
			performanceParameterObjAsInput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObjAsInput.customName = "Custom 1";

			var mockRepository = new Mock<IPerformanceParameterRepository>();
			mockRepository.Setup(x => x.InsertPerformanceParameter(performanceParameterObjAsInput))
				.Returns(1);

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(performanceParameterObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "PerformanceParameter added successfully.");
		}

		[TestMethod]
		public void Post_Null_Input_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPerformanceParameterRepository>();

			var controller = new PerformanceParameterController(mockRepository.Object);
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
			PerformanceParameter performanceParameterObjAsInput = new PerformanceParameter();
			performanceParameterObjAsInput.sportId = -1;
			performanceParameterObjAsInput.perfParaName = new PerformanceParameterName();
			performanceParameterObjAsInput.perfParaName.id = 1;
			performanceParameterObjAsInput.perfParaName.name = "Parameter 1";
			performanceParameterObjAsInput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObjAsInput.perfParaTypeGroup.id = 1;
			performanceParameterObjAsInput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObjAsInput.customName = "Custom 1";

			var mockRepository = new Mock<IPerformanceParameterRepository>();

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(performanceParameterObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.PartialContent);
			Assert.AreEqual(responseStr, "Invalid Sport Id passed.");
		}

		[TestMethod]
		public void Post_Invalid_ParaNameId_Test()
		{
			// Arrange
			PerformanceParameter performanceParameterObjAsInput = new PerformanceParameter();
			performanceParameterObjAsInput.sportId = 1;
			performanceParameterObjAsInput.perfParaName = new PerformanceParameterName();
			performanceParameterObjAsInput.perfParaName.id = -1;
			performanceParameterObjAsInput.perfParaName.name = "Parameter 1";
			performanceParameterObjAsInput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObjAsInput.perfParaTypeGroup.id = 1;
			performanceParameterObjAsInput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObjAsInput.customName = "Custom 1";

			var mockRepository = new Mock<IPerformanceParameterRepository>();

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(performanceParameterObjAsInput);

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
			PerformanceParameter performanceParameterObjAsInput = new PerformanceParameter();
			performanceParameterObjAsInput.sportId = 1;
			performanceParameterObjAsInput.perfParaName = new PerformanceParameterName();
			performanceParameterObjAsInput.perfParaName.id = 1;
			performanceParameterObjAsInput.perfParaName.name = "Parameter 1";
			performanceParameterObjAsInput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObjAsInput.perfParaTypeGroup.id = -1;
			performanceParameterObjAsInput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObjAsInput.customName = "Custom 1";

			var mockRepository = new Mock<IPerformanceParameterRepository>();

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(performanceParameterObjAsInput);

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
			PerformanceParameter performanceParameterObjAsInput = new PerformanceParameter();
			performanceParameterObjAsInput.id = 1;
			performanceParameterObjAsInput.sportId = 1;
			performanceParameterObjAsInput.perfParaName = new PerformanceParameterName();
			performanceParameterObjAsInput.perfParaName.id = 1;
			performanceParameterObjAsInput.perfParaName.name = "Parameter 1";
			performanceParameterObjAsInput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObjAsInput.perfParaTypeGroup.id = 1;
			performanceParameterObjAsInput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObjAsInput.customName = "Custom 1";

			var mockRepository = new Mock<IPerformanceParameterRepository>();
			mockRepository.Setup(x => x.UpdatePerformanceParameter(performanceParameterObjAsInput))
				.Returns(true);

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(performanceParameterObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "Updated PerformanceParameter successfully.");
		}

		[TestMethod]
		public void Put_Null_Input_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPerformanceParameterRepository>();

			var controller = new PerformanceParameterController(mockRepository.Object);
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
			PerformanceParameter performanceParameterObjAsInput = new PerformanceParameter();
			performanceParameterObjAsInput.id = -1;
			performanceParameterObjAsInput.sportId = 1;
			performanceParameterObjAsInput.perfParaName = new PerformanceParameterName();
			performanceParameterObjAsInput.perfParaName.id = 1;
			performanceParameterObjAsInput.perfParaName.name = "Parameter 1";
			performanceParameterObjAsInput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObjAsInput.perfParaTypeGroup.id = 1;
			performanceParameterObjAsInput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObjAsInput.customName = "Custom 1";

			var mockRepository = new Mock<IPerformanceParameterRepository>();
			mockRepository.Setup(x => x.UpdatePerformanceParameter(performanceParameterObjAsInput))
				.Returns(true);

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(performanceParameterObjAsInput);

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
			PerformanceParameter performanceParameterObjAsInput = new PerformanceParameter();
			performanceParameterObjAsInput.id = 1;
			performanceParameterObjAsInput.sportId = -1;
			performanceParameterObjAsInput.perfParaName = new PerformanceParameterName();
			performanceParameterObjAsInput.perfParaName.id = 1;
			performanceParameterObjAsInput.perfParaName.name = "Parameter 1";
			performanceParameterObjAsInput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObjAsInput.perfParaTypeGroup.id = 1;
			performanceParameterObjAsInput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObjAsInput.customName = "Custom 1";

			var mockRepository = new Mock<IPerformanceParameterRepository>();
			mockRepository.Setup(x => x.UpdatePerformanceParameter(performanceParameterObjAsInput))
				.Returns(true);

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(performanceParameterObjAsInput);

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
			PerformanceParameter performanceParameterObjAsInput = new PerformanceParameter();
			performanceParameterObjAsInput.id = 1;
			performanceParameterObjAsInput.sportId = 1;
			performanceParameterObjAsInput.perfParaName = new PerformanceParameterName();
			performanceParameterObjAsInput.perfParaName.id = -1;
			performanceParameterObjAsInput.perfParaName.name = "Parameter 1";
			performanceParameterObjAsInput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObjAsInput.perfParaTypeGroup.id = 1;
			performanceParameterObjAsInput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObjAsInput.customName = "Custom 1";

			var mockRepository = new Mock<IPerformanceParameterRepository>();
			mockRepository.Setup(x => x.UpdatePerformanceParameter(performanceParameterObjAsInput))
				.Returns(true);

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(performanceParameterObjAsInput);

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
			PerformanceParameter performanceParameterObjAsInput = new PerformanceParameter();
			performanceParameterObjAsInput.id = 1;
			performanceParameterObjAsInput.sportId = 1;
			performanceParameterObjAsInput.perfParaName = new PerformanceParameterName();
			performanceParameterObjAsInput.perfParaName.id = 1;
			performanceParameterObjAsInput.perfParaName.name = "Parameter 1";
			performanceParameterObjAsInput.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			performanceParameterObjAsInput.perfParaTypeGroup.id = -1;
			performanceParameterObjAsInput.perfParaTypeGroup.name = "Type 1";
			performanceParameterObjAsInput.customName = "Custom 1";

			var mockRepository = new Mock<IPerformanceParameterRepository>();
			mockRepository.Setup(x => x.UpdatePerformanceParameter(performanceParameterObjAsInput))
				.Returns(true);

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Put(performanceParameterObjAsInput);

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
			var mockRepository = new Mock<IPerformanceParameterRepository>();
			mockRepository.Setup(x => x.DeletePerformanceParameter(1))
				.Returns(true);

			var controller = new PerformanceParameterController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Delete(1);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			string responseStr = Helper.Deserialize<string>(responseJSONStr);
			Assert.IsNotNull(response);
			Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseStr, "PerformanceParameter successfully deleted.");
		}

		[TestMethod]
		public void Delete_Invalid_Id_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPerformanceParameterRepository>();

			var controller = new PerformanceParameterController(mockRepository.Object);
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