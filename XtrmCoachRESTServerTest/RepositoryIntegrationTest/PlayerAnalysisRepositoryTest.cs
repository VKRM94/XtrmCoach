using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using XtrmCoachRESTServer;
using XtrmCoachRESTServer.Models;

// Testing methods which are exposed by API
namespace XtrmCoachRESTServerTest.RepositoryTest
{
	[TestClass]
	public class PlayerAnalysisRepositoryTest
	{
		[TestMethod]
		public void Repository_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PlayerAnalysisRepository PlayerAnalysisRepository = new PlayerAnalysisRepository();

				// Assert
				Assert.IsNotNull(PlayerAnalysisRepository);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to initialize PlayerAnalysisRepository.");
			}
		}

		[TestMethod]
		public void Repository_Get_PlayerAnalysis_Test()
		{
			try
			{
				// Arrange
				PlayerAnalysisRepository PlayerAnalysisRepository = new PlayerAnalysisRepository();
				PlayerAnalysis playerAnalysis = new PlayerAnalysis();
				playerAnalysis.sport = new Sport();
				playerAnalysis.sport.id = 1;
				playerAnalysis.sport.name = "Sport 1";
				playerAnalysis.sport.userId = 1;
				playerAnalysis.players = new List<Player>();
				Player player = new Player();
				player.id = 1;
				player.firstName = "FirstName";
				player.lastName = "LastName";
				playerAnalysis.players.Add(player);
				playerAnalysis.perfPara = new PerformanceParameter();
				playerAnalysis.perfPara.id = 1;
				playerAnalysis.perfPara.perfParaName = new PerformanceParameterName();
				playerAnalysis.perfPara.perfParaName.id = 1;
				playerAnalysis.perfPara.perfParaName.name = "Para Name 1";
				playerAnalysis.perfPara.perfParaTypeGroup = new PerformanceParameterTypeGroup();
				playerAnalysis.perfPara.perfParaTypeGroup.id = 1;
				playerAnalysis.perfPara.perfParaTypeGroup.name = "Para Type Group 1";
				playerAnalysis.perfPara.sportId = 1;
				playerAnalysis.perfPara.customName = "custom name";

				playerAnalysis.timeRange = "LAST1WEEK";

				DateTime time = DateTime.Now;
				playerAnalysis.fromTime = time;
				playerAnalysis.toTime = time;

				// Act
				HighChart PlayerAnalysisResultFromDb = PlayerAnalysisRepository.GetPlayerAnalysis(playerAnalysis);

				// Assert
				Assert.IsNotNull(PlayerAnalysisResultFromDb);
				Assert.AreEqual(PlayerAnalysisResultFromDb.chart.type, "line");
				Assert.IsTrue(PlayerAnalysisResultFromDb.xAxis.categories.Count == 8);
				Assert.AreEqual(PlayerAnalysisResultFromDb.yAxis.title.text, playerAnalysis.perfPara.perfParaName.name);
				Assert.AreEqual(PlayerAnalysisResultFromDb.tooltip.valueSuffix, " " + playerAnalysis.perfPara.perfParaTypeGroup.name);
				Assert.AreEqual(PlayerAnalysisResultFromDb.series[0].name, playerAnalysis.players[0].firstName + " " + playerAnalysis.players[0].lastName);
				Assert.IsTrue(PlayerAnalysisResultFromDb.series[0].data.Count == 8);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to fetch player Analysis.");
			}
		}
	}
}