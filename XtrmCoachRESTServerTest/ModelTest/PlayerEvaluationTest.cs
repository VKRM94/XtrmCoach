using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class PlayerEvaluationTest
	{
		[TestMethod]
		public void PerformanceParameterEvaluation_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				PlayerEvaluation playerEvaluation = new PlayerEvaluation();

				// Assert
				Assert.IsNotNull(playerEvaluation);
			}
			catch (Exception)
			{
				throw new AssertFailedException("PlayerEvaluation initialization failed.");
			}
		}

		[TestMethod]
		public void PerformanceParameterEvaluation_Property_Set_Test()
		{
			try
			{
				// Arrange
				PlayerEvaluation playerEvaluation = new PlayerEvaluation();

				// Act
				playerEvaluation.sportId = 1;
				playerEvaluation.playerId = 1;
				playerEvaluation.perfParaName = new PerformanceParameterName();
				playerEvaluation.perfParaName.id = 1;
				playerEvaluation.perfParaName.name = "Perf Para 1";
				playerEvaluation.customName = "Custom Name";
				playerEvaluation.perfParaTypes = new List<PerformanceParameterType>();
				PerformanceParameterType perfParaType = new PerformanceParameterType();
				perfParaType.id = 1;
				perfParaType.groupId = 1;
				perfParaType.name = "Perf Para Type 1";
				playerEvaluation.perfParaTypes.Add(perfParaType);
				playerEvaluation.selectedType = new PerformanceParameterType();
				playerEvaluation.selectedType.id = 1;
				playerEvaluation.selectedType.groupId = 1;
				playerEvaluation.selectedType.name = "Perf Para Type 1";
				playerEvaluation.value = "value";

				// Assert
				Assert.AreEqual<long>(playerEvaluation.sportId, 1);
				Assert.AreEqual<long>(playerEvaluation.perfParaName.id, 1);
				Assert.AreEqual<string>(playerEvaluation.perfParaName.name, "Perf Para 1");
				Assert.AreEqual<string>(playerEvaluation.customName, "Custom Name");
				Assert.AreEqual<long>(playerEvaluation.perfParaTypes[0].id, 1);
				Assert.AreEqual<long>(playerEvaluation.perfParaTypes[0].groupId, 1);
				Assert.AreEqual<string>(playerEvaluation.perfParaTypes[0].name, "Perf Para Type 1");
				Assert.AreEqual<long>(playerEvaluation.selectedType.id, 1);
				Assert.AreEqual<long>(playerEvaluation.selectedType.groupId, 1);
				Assert.AreEqual<string>(playerEvaluation.selectedType.name, "Perf Para Type 1");
				Assert.AreEqual<string>(playerEvaluation.value, "value");
			}
			catch (Exception)
			{
				throw new AssertFailedException("Setting PerformanceParameterEvaluation properties failed.");
			}
		}
	}
}
