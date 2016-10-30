using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtrmCoachRESTServer;
using XtrmCoachRESTServer.Models;

// Testing methods which are exposed by API
namespace XtrmCoachRESTServerTest.RepositoryTest
{
	[TestClass]
	public class UserRepositoryTest
	{
		[TestMethod]
		public void Repository_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				UserRepository userRepository = new UserRepository();

				// Assert
				Assert.IsNotNull(userRepository);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to initialize UserRepository.");
			}
		}

		[TestMethod]
		public void Repository_Save_User_Test()
		{
			try
			{
				// Arrange
				UserRepository userRepository = new UserRepository();
				User userToSave = new User();
				userToSave.firstName = "Team";
				userToSave.lastName = "2";
				userToSave.emailId = "team2@uncc.edu";
				userToSave.password = "team2password";

				// Act
				long userId = userRepository.SaveUser(userToSave);
				User userFromDb = userRepository.getUser(userId);

				// Assert
				Assert.IsNotNull(userFromDb);
				Assert.AreEqual(userToSave.firstName, userFromDb.firstName);
				Assert.AreEqual(userToSave.lastName, userFromDb.lastName);
				Assert.AreEqual(userToSave.emailId, userFromDb.emailId);
				Assert.AreEqual(userToSave.password, userFromDb.password);

				// Cleanup
				bool isDeleted = userRepository.deleteUser(userId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add new user.");
			}
		}

		[TestMethod]
		public void Repository_Authenticate_User_Test()
		{
			try
			{
				// Arrange
				UserRepository userRepository = new UserRepository();
				User userToTest = new User();
				userToTest.firstName = "Team";
				userToTest.lastName = "2";
				userToTest.emailId = "team2@uncc.edu";
				userToTest.password = "team2password";

				// Act
				long userId = userRepository.SaveUser(userToTest);
				User userFromDb = userRepository.AuthenticateUser(userToTest.emailId, userToTest.password);

				// Assert
				Assert.IsNotNull(userFromDb);
				Assert.AreEqual(userToTest.firstName, userFromDb.firstName);
				Assert.AreEqual(userToTest.lastName, userFromDb.lastName);
				Assert.AreEqual(userToTest.emailId, userFromDb.emailId);

				// Cleanup
				bool isDeleted = userRepository.deleteUser(userId);
				Assert.IsTrue(isDeleted);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Unable to add new user.");
			}
		}
	}
}