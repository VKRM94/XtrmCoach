using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public class PerformanceParameterTypeRepository : IPerformanceParameterTypeRepository
	{
		MySqlConnection conn;

		public PerformanceParameterTypeRepository()
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

		public ArrayList GetPerformanceParameterTypes(long performanceParameterNameId)
		{
			String sqlStr = "SELECT id, name FROM perf_para_type WHERE id IN (SELECT type_id FROM perf_para_link WHERE name_id = " + performanceParameterNameId + ")";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList performanceParameterTypes = new ArrayList();
			PerformanceParameterType pp = null;

			if (sqlReader.Read())
			{
				pp = new PerformanceParameterType();
				pp.id = sqlReader.GetInt32(0);
				pp.name = sqlReader.GetString(1);
				performanceParameterTypes.Add(pp);
			}

			sqlReader.Close();
			return performanceParameterTypes;
		}
	}
}