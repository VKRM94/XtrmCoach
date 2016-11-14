using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class SportTest
	{
		[TestMethod]
		public void Sport_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				Sport sport = new Sport();

				// Assert
				Assert.IsNotNull(sport);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Sport initialization failed.");
			}
		}

		[TestMethod]
		public void Sport_Property_Set_Test()
		{
			try
			{
				// Arrange
				Sport sport = new Sport();

				// Act
				sport.name = "Sport 1";
				sport.userId = 1;

				// Assert
				Assert.AreEqual<string>(sport.name, "Sport 1");
				Assert.AreEqual<long>(sport.userId, 1);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Setting Sport properties failed.");
			}
		}
	}
}
