using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class UserTest
	{
		[TestMethod]
		public void User_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				User user = new User();

				// Assert
				Assert.IsNotNull(user);
			}
			catch (Exception)
			{
				throw new AssertFailedException("User initialization failed.");
			}
		}

		[TestMethod]
		public void User_Property_Set_Test()
		{
			try
			{
				// Arrange
				User user = new User();

				// Act
				user.firstName = "Team";
				user.lastName = "2";
				user.emailId = "team2@uncc.edu";
				user.password = "team2";
				user.isAdmin = false;

				// Assert
				Assert.AreEqual<string>(user.firstName, "Team");
				Assert.AreEqual<string>(user.lastName, "2");
				Assert.AreEqual<string>(user.emailId, "team2@uncc.edu");
				Assert.AreEqual<string>(user.password, "team2");
				Assert.AreEqual<bool>(user.isAdmin, false);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Setting User properties failed.");
			}
		}
	}
}
