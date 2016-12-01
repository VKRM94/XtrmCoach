using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServerTest
{
	[TestClass]
	public class PlayerTest
	{
		[TestMethod]
		public void Player_Initialization_Test()
		{
			try
			{
				// Arrange and Act
				Player player = new Player();

				// Assert
				Assert.IsNotNull(player);
			}
			catch (Exception)
			{
				throw new AssertFailedException("Player initialization failed.");
			}
		}

		[TestMethod]
		public void Player_Property_Set_Test()
		{
			try
			{
				// Arrange
				Player player = new Player();

				// Act
				player.firstName = "FirstName";
				player.lastName = "LastName";
				player.dob = new DateTime(2016, 11, 25);
				player.imageId = "ImageId";
				player.sports = new List<Sport>();
				player.sports.Add(new Sport() { id = 1, name = "Sport 1", userId = 1 });

				// Assert
				Assert.AreEqual<string>(player.firstName, "FirstName");
				Assert.AreEqual<string>(player.lastName, "LastName");
				Assert.AreEqual<DateTime>(player.dob, new DateTime(2016, 11, 25));
				Assert.AreEqual<string>(player.imageId, "ImageId");
				Assert.AreEqual<Sport>(((Sport)player.sports[0]), new Sport() { id = 1, name = "Sport 1", userId = 1 });
			}
			catch (Exception)
			{
				throw new AssertFailedException("Setting Player properties failed.");
			}
		}
	}
}
