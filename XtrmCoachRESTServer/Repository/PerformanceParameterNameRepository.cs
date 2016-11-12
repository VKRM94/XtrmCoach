using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public class PerformanceParameterNameRepository : IPerformanceParameterNameRepository
	{
		MySqlConnection conn;

		public PerformanceParameterNameRepository()
		{
			try
			{
				conn = new MySqlConnection();
				conn.ConnectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
				conn.Open();
			}
			catch (MySqlException ex)
			{
				throw ex;
			}
		}

		public ArrayList GetPerformanceParameterNames()
		{
			String sqlStr = "SELECT * FROM perf_para_name";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList performanceParameterNames = new ArrayList();
			PerformanceParameterName pp = null;

			while (sqlReader.Read())
			{
				pp = new PerformanceParameterName();
				pp.id = sqlReader.GetInt32(0);
				pp.name = sqlReader.GetString(1);
				performanceParameterNames.Add(pp);
			}

			sqlReader.Close();
			return performanceParameterNames;
		}

		public PerformanceParameterName GetPerformanceParameterName(long performanceParameterNameId)
		{
			String sqlStr = "SELECT id, name FROM perf_para_name WHERE id = " + performanceParameterNameId;
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			PerformanceParameterName pp = null;

			if (sqlReader.Read())
			{
				pp = new PerformanceParameterName();
				pp.id = sqlReader.GetInt32(0);
				pp.name = sqlReader.GetString(1);
			}

			sqlReader.Close();
			return pp;
		}
	}
}