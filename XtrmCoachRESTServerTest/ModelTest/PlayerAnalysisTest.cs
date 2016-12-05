using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class PlayerAnalysisTest
	{
		[TestMethod]
		public void PerformanceParameterAnalysis_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PlayerAnalysis playerAnalysis = new PlayerAnalysis();

				// Assert
				Assert.IsNotNull(playerAnalysis);
			}
			catch (Exception)
			{
				throw new AssertFailedException("PlayerAnalysis initialization failed.");
			}
		}

		[TestMethod]
		public void PerformanceParameterAnalysis_Property_Set_Test()
		{
			try
			{
				// Arrange
				PlayerAnalysis playerAnalysis = new PlayerAnalysis();

				// Act
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

				// Assert
				Assert.AreEqual<long>(playerAnalysis.sport.id, 1);
				Assert.AreEqual<string>(playerAnalysis.sport.name, "Sport 1");
				Assert.AreEqual<long>(playerAnalysis.sport.userId, 1);
				Assert.AreEqual<long>(playerAnalysis.players[0].id, 1);
				Assert.AreEqual<string>(playerAnalysis.players[0].firstName, "FirstName");
				Assert.AreEqual<string>(playerAnalysis.players[0].lastName, "LastName");
				Assert.AreEqual<long>(playerAnalysis.perfPara.id, 1);
				Assert.AreEqual<long>(playerAnalysis.perfPara.perfParaName.id, 1);
				Assert.AreEqual<string>(playerAnalysis.perfPara.perfParaName.name, "Para Name 1");
				Assert.AreEqual<long>(playerAnalysis.perfPara.perfParaTypeGroup.id, 1);
				Assert.AreEqual<string>(playerAnalysis.perfPara.perfParaTypeGroup.name, "Para Type Group 1");
				Assert.AreEqual<long>(playerAnalysis.perfPara.sportId, 1);
				Assert.AreEqual<string>(playerAnalysis.perfPara.customName, "custom name");
				Assert.AreEqual<string>(playerAnalysis.timeRange, "LAST1WEEK");
				Assert.AreEqual<DateTime>(playerAnalysis.fromTime, time);
				Assert.AreEqual<DateTime>(playerAnalysis.toTime, time);
			}
			catch (Exception ex)
			{
				throw new AssertFailedException("Setting PerformanceParameterAnalysis properties failed.");
			}
		}
	}
}
