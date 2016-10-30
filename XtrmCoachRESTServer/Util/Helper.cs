using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace XtrmCoachRESTServer.Util
{
	public static class Helper
	{
		public static string Serialize(Object obj)
		{
			return (new JavaScriptSerializer().Serialize(obj));
		}

		public static T Deserialize<T>(string objStr) where T : class
		{
			return (new JavaScriptSerializer().Deserialize<T>(objStr));
		}
	}
}