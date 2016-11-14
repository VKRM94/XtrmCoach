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
	public class PerformanceParameterNameControllerTest
	{
		[TestMethod]
		public void Get_All_PerformanceParameterNames_Test()
		{
			// Arrange
			ArrayList performanceParameterNamesObjsAsOuptut = new ArrayList();

			PerformanceParameterName performanceParameterNameObj1AsOutput = new PerformanceParameterName();
			performanceParameterNameObj1AsOutput.id = 1;
			performanceParameterNameObj1AsOutput.name = "Para Name 1";

			PerformanceParameterName performanceParameterNameObj2AsOutput = new PerformanceParameterName();
			performanceParameterNameObj2AsOutput.id = 2;
			performanceParameterNameObj2AsOutput.name = "Para Name 2";

			performanceParameterNamesObjsAsOuptut.Add(performanceParameterNameObj1AsOutput);
			performanceParameterNamesObjsAsOuptut.Add(performanceParameterNameObj2AsOutput);

			var mockRepository = new Mock<IPerformanceParameterNameRepository>();
			mockRepository.Setup(x => x.GetPerformanceParameterNames())
				.Returns(performanceParameterNamesObjsAsOuptut);

			var controller = new PerformanceParameterNameController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			ArrayList performanceParameterNamesObj = controller.Get();

			// Assert
			Assert.AreEqual<int>(performanceParameterNamesObj.Count, performanceParameterNamesObjsAsOuptut.Count);
			Assert.AreEqual(performanceParameterNamesObj[0], performanceParameterNamesObjsAsOuptut[0]);
			Assert.AreEqual(performanceParameterNamesObj[1], performanceParameterNamesObjsAsOuptut[1]);
		}
	}
}