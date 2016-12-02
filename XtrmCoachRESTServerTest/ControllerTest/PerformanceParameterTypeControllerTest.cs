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
	public class PerformanceParameterTypeControllerTest
	{
		[TestMethod]
		public void Get_All_PerformanceParameterTypes_Test()
		{
			// Arrange
			ArrayList performanceParameterTypesObjsAsOuptut = new ArrayList();

			PerformanceParameterType performanceParameterTypeObj1AsOutput = new PerformanceParameterType();
			performanceParameterTypeObj1AsOutput.id = 1;
			performanceParameterTypeObj1AsOutput.groupId = 1;
			performanceParameterTypeObj1AsOutput.name = "Para Type 1";

			PerformanceParameterType performanceParameterTypeObj2AsOutput = new PerformanceParameterType();
			performanceParameterTypeObj2AsOutput.id = 2;
			performanceParameterTypeObj2AsOutput.groupId = 2;
			performanceParameterTypeObj2AsOutput.name = "Para Type 2";

			performanceParameterTypesObjsAsOuptut.Add(performanceParameterTypeObj1AsOutput);
			performanceParameterTypesObjsAsOuptut.Add(performanceParameterTypeObj2AsOutput);

			var mockRepository = new Mock<IPerformanceParameterTypeRepository>();
			mockRepository.Setup(x => x.GetPerformanceParameterTypes(1))
				.Returns(performanceParameterTypesObjsAsOuptut);

			var controller = new PerformanceParameterTypeController(mockRepository.Object);
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();

			// Act
			ArrayList performanceParameterTypesObj = controller.Get(1);

			// Assert
			Assert.AreEqual<int>(performanceParameterTypesObj.Count, performanceParameterTypesObjsAsOuptut.Count);
			Assert.AreEqual(performanceParameterTypesObj[0], performanceParameterTypesObjsAsOuptut[0]);
			Assert.AreEqual(performanceParameterTypesObj[1], performanceParameterTypesObjsAsOuptut[1]);
		}
	}
}