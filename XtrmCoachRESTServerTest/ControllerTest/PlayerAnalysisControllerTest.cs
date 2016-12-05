using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Controller;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServerTest.ControllerTest
{
	[TestClass]
	public class PlayerAnalysisControllerTest
	{
		[TestMethod]
		public void Post_Valid_Test()
		{
			// Arrange
			PlayerAnalysis PlayerAnalysisObjAsInput = new PlayerAnalysis();
			PlayerAnalysisObjAsInput.sport = new Sport();
			PlayerAnalysisObjAsInput.sport.id = 1;
			PlayerAnalysisObjAsInput.sport.name = "Sport 1";
			PlayerAnalysisObjAsInput.sport.userId = 1;
			PlayerAnalysisObjAsInput.players = new List<Player>();
			Player player = new Player();
			player.id = 1;
			player.firstName = "FirstName";
			player.lastName = "LastName";
			PlayerAnalysisObjAsInput.players.Add(player);
			PlayerAnalysisObjAsInput.perfPara = new PerformanceParameter();
			PlayerAnalysisObjAsInput.perfPara.id = 1;
			PlayerAnalysisObjAsInput.perfPara.perfParaName = new PerformanceParameterName();
			PlayerAnalysisObjAsInput.perfPara.perfParaName.id = 1;
			PlayerAnalysisObjAsInput.perfPara.perfParaName.name = "Para Name 1";
			PlayerAnalysisObjAsInput.perfPara.perfParaTypeGroup = new PerformanceParameterTypeGroup();
			PlayerAnalysisObjAsInput.perfPara.perfParaTypeGroup.id = 1;
			PlayerAnalysisObjAsInput.perfPara.perfParaTypeGroup.name = "Para Type Group 1";
			PlayerAnalysisObjAsInput.perfPara.sportId = 1;
			PlayerAnalysisObjAsInput.perfPara.customName = "custom name";
			PlayerAnalysisObjAsInput.timeRange = "LAST1WEEK";
			PlayerAnalysisObjAsInput.fromTime = DateTime.Now.AddDays(-1);
			PlayerAnalysisObjAsInput.toTime = DateTime.Now;

			HighChart highChart = new HighChart();

			var mockRepository = new Mock<IPlayerAnalysisRepository>();
			mockRepository.Setup(x => x.GetPlayerAnalysis(PlayerAnalysisObjAsInput))
				.Returns(highChart);

			var controller = new PlayerAnalysisController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(PlayerAnalysisObjAsInput);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			Assert.IsNotNull(responseJSONStr);
		}

		[TestMethod]
		public void Post_Null_Input_Test()
		{
			// Arrange
			var mockRepository = new Mock<IPlayerAnalysisRepository>();

			var controller = new PlayerAnalysisController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			HttpResponseMessage response = controller.Post(null);

			// Assert
			string responseJSONStr = response.Content.ReadAsStringAsync().Result;
			Assert.IsNotNull(responseJSONStr);
		}
	}
}