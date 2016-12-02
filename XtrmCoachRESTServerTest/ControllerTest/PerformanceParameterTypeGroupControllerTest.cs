using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
using System.Net.Http;
using System.Web.Http;
using XtrmCoachRESTServer.Controller;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServerTest.ControllerTest
{
	[TestClass]
	public class PerformanceParameterTypeGroupControllerTest
	{
		[TestMethod]
		public void Get_All_PerformanceParameterTypeGroups_Test()
		{
			// Arrange
			ArrayList performanceParameterTypeGroupsObjsAsOuptut = new ArrayList();

			PerformanceParameterTypeGroup performanceParameterTypeGroupObj1AsOutput = new PerformanceParameterTypeGroup();
			performanceParameterTypeGroupObj1AsOutput.id = 1;
			performanceParameterTypeGroupObj1AsOutput.name = "Para TypeGroup 1";

			PerformanceParameterTypeGroup performanceParameterTypeGroupObj2AsOutput = new PerformanceParameterTypeGroup();
			performanceParameterTypeGroupObj2AsOutput.id = 2;
			performanceParameterTypeGroupObj2AsOutput.name = "Para TypeGroup 2";

			performanceParameterTypeGroupsObjsAsOuptut.Add(performanceParameterTypeGroupObj1AsOutput);
			performanceParameterTypeGroupsObjsAsOuptut.Add(performanceParameterTypeGroupObj2AsOutput);

			var mockRepository = new Mock<IPerformanceParameterTypeGroupRepository>();
			mockRepository.Setup(x => x.GetPerformanceParameterTypeGroups(1))
				.Returns(performanceParameterTypeGroupsObjsAsOuptut);

			var controller = new PerformanceParameterTypeGroupController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			ArrayList performanceParameterTypeGroupsObj = controller.Get(1);

			// Assert
			Assert.AreEqual<int>(performanceParameterTypeGroupsObj.Count, performanceParameterTypeGroupsObjsAsOuptut.Count);
			Assert.AreEqual(performanceParameterTypeGroupsObj[0], performanceParameterTypeGroupsObjsAsOuptut[0]);
			Assert.AreEqual(performanceParameterTypeGroupsObj[1], performanceParameterTypeGroupsObjsAsOuptut[1]);
		}
	}
}