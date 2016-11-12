using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public class PerformanceParameterRepository : IPerformanceParameterRepository
	{
		MySqlConnection conn;

		public PerformanceParameterRepository()
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

		public ArrayList GetPerformanceParameters()
		{
			String sqlStr = "select pp.id, pp.sport_id, pp.name_id, ppn.name, pp.custom_name, pp.type_id, ppt.name from perf_para pp join perf_para_name ppn on pp.name_id = ppn.id join perf_para_type ppt on pp.type_id = ppt.id";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList performanceParameters = new ArrayList();
			PerformanceParameter pp = null;

			while (sqlReader.Read())
			{
				pp = new PerformanceParameter();
				pp.id = sqlReader.GetInt32(0);
				pp.sportId = sqlReader.GetInt32(1);

				pp.perfParaName = new PerformanceParameterName();
				pp.perfParaName.id = sqlReader.GetInt32(2);
				pp.perfParaName.name = sqlReader.GetString(3);
				pp.customName = sqlReader.GetString(4);

				pp.perfParaType = new PerformanceParameterType();
				pp.perfParaType.id = sqlReader.GetInt32(5);
				pp.perfParaType.name = sqlReader.GetString(6);

				performanceParameters.Add(pp);
			}

			sqlReader.Close();
			return performanceParameters;
		}

		public ArrayList GetPerformanceParameters(long sportsId)
		{
			String sqlStr = "select pp.id, pp.sport_id, pp.name_id, ppn.name, pp.custom_name, pp.type_id, ppt.name from perf_para pp join perf_para_name ppn on pp.name_id = ppn.id join perf_para_type ppt on pp.type_id = ppt.id where pp.sport_id = " + sportsId;
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList performanceParameters = new ArrayList();
			PerformanceParameter pp = null;

			while (sqlReader.Read())
			{
				pp = new PerformanceParameter();
				pp.id = sqlReader.GetInt32(0);
				pp.sportId = sqlReader.GetInt32(1);

				pp.perfParaName = new PerformanceParameterName();
				pp.perfParaName.id = sqlReader.GetInt32(2);
				pp.perfParaName.name = sqlReader.GetString(3);
				pp.customName = sqlReader.GetString(4);

				pp.perfParaType = new PerformanceParameterType();
				pp.perfParaType.id = sqlReader.GetInt32(5);
				pp.perfParaType.name = sqlReader.GetString(6);

				performanceParameters.Add(pp);
			}

			sqlReader.Close();
			return performanceParameters;
		}

		public PerformanceParameter GetPerformanceParameter(long performanceParameterId)
		{
			String sqlStr = "select pp.id, pp.sport_id, pp.name_id, ppn.name, pp.custom_name, pp.type_id, ppt.name from perf_para pp join perf_para_name ppn on pp.name_id = ppn.id join perf_para_type ppt on pp.type_id = ppt.id WHERE pp.id = " + performanceParameterId;
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			PerformanceParameter pp = null;

			if (sqlReader.Read())
			{
				pp = new PerformanceParameter();
				pp.id = sqlReader.GetInt32(0);
				pp.sportId = sqlReader.GetInt32(1);

				pp.perfParaName = new PerformanceParameterName();
				pp.perfParaName.id = sqlReader.GetInt32(2);
				pp.perfParaName.name = sqlReader.GetString(3);
				pp.customName = sqlReader.GetString(4);

				pp.perfParaType = new PerformanceParameterType();
				pp.perfParaType.id = sqlReader.GetInt32(5);
				pp.perfParaType.name = sqlReader.GetString(6);
			}

			sqlReader.Close();
			return pp;
		}

		public long InsertPerformanceParameter(PerformanceParameter performanceParameter)
		{
			String sqlStr = "INSERT INTO perf_para (sport_id, name_id, custom_name, type_id) VALUES (" + performanceParameter.sportId + ", " + performanceParameter.perfParaName.id + ", '" + performanceParameter.customName + "', " + performanceParameter.perfParaType.id + ")";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			try
			{
				cmd.ExecuteNonQuery();
				return cmd.LastInsertedId;
			}
			catch (Exception)
			{
				return -1;
			}
		}

		public bool DeletePerformanceParameter(long performanceParameterId)
		{
			String sqlStr = "SELECT * FROM perf_para WHERE id = " + performanceParameterId + "";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				sqlStr = "DELETE FROM perf_para WHERE Id = " + performanceParameterId + "";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.ExecuteNonQuery();

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool UpdatePerformanceParameter(PerformanceParameter performanceParameter)
		{
			String sqlStr = "SELECT * FROM perf_para WHERE id = " + performanceParameter.id;
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				sqlStr = "UPDATE perf_para SET name_id = " + performanceParameter.perfParaName.id + ", custom_name = '" + performanceParameter.customName + "', type_id = " + performanceParameter.perfParaType.id + " WHERE id = " + performanceParameter.id;
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.ExecuteNonQuery();

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}