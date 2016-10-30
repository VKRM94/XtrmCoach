using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtrmCoachRESTServer.Util;

namespace XtrmCoachRESTServerTest.HelperTest
{
	[TestClass]
	public class HelperTest
	{
		public class DummyClass
		{
			public string variable { get; set; }
		}

		[TestMethod]
		public void Serialize_Test()
		{
			// Arrange
			DummyClass dummyObj = new DummyClass();
			dummyObj.variable = "value";
			string serializedStr;

			// Act
			serializedStr = Helper.Serialize(dummyObj);

			// Assert
			Assert.AreEqual(serializedStr, "{\"variable\":\"value\"}");
		}

		[TestMethod]
		public void Deserialize_Test()
		{
			// Arrange
			DummyClass expectedDummyObj = new DummyClass();
			expectedDummyObj.variable = "value";
			string JSONStr = "{\"variable\":\"value\"}";

			// Act
			DummyClass dummyObj = Helper.Deserialize<DummyClass>(JSONStr);

			// Assert
			Assert.AreEqual(dummyObj.variable, expectedDummyObj.variable);
		}
	}
}
