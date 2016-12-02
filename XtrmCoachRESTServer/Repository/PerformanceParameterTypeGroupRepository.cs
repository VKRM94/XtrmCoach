using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public class PerformanceParameterTypeGroupRepository : IPerformanceParameterTypeGroupRepository
	{
		MySqlConnection conn;

		public PerformanceParameterTypeGroupRepository()
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

		public ArrayList GetPerformanceParameterTypeGroups(long performanceParameterNameId)
		{
			String sqlStr = "SELECT group_id, GROUP_CONCAT(name separator ', ') FROM perf_para_type WHERE group_id IN (SELECT group_id FROM perf_para_link WHERE name_id = " + performanceParameterNameId + ") GROUP BY group_id";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList performanceParameterTypeGroups = new ArrayList();
			PerformanceParameterTypeGroup pp = null;

			while (sqlReader.Read())
			{
				pp = new PerformanceParameterTypeGroup();
				pp.id = sqlReader.GetInt32(0);
				pp.name = sqlReader.GetString(1);
				performanceParameterTypeGroups.Add(pp);
			}

			sqlReader.Close();
			return performanceParameterTypeGroups;
		}
	}
}