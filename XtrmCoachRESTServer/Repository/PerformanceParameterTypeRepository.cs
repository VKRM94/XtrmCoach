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
			String sqlStr = "SELECT id, group_id, name FROM perf_para_type WHERE group_id IN (SELECT group_id FROM perf_para_link WHERE name_id = " + performanceParameterNameId + ")";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList performanceParameterTypes = new ArrayList();
			PerformanceParameterType pp = null;

			while (sqlReader.Read())
			{
				pp = new PerformanceParameterType();
				pp.id = sqlReader.GetInt32(0);
				pp.groupId = sqlReader.GetInt32(1);
				pp.name = sqlReader.GetString(2);
				performanceParameterTypes.Add(pp);
			}

			sqlReader.Close();
			return performanceParameterTypes;
		}
	}
}