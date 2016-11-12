using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public class SportRespository : ISportRepository
	{
		MySqlConnection conn;

		public SportRespository()
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

		public ArrayList GetSports()
		{
			String sqlStr = "SELECT * FROM sport";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList sports = new ArrayList();

			while (sqlReader.Read())
			{
				Sport sp = new Sport();
				sp.id = sqlReader.GetInt32(0);
				sp.name = sqlReader.GetString(1);
				sp.userId = sqlReader.GetInt32(2);
				sports.Add(sp);
			}

			sqlReader.Close();
			return sports;
		}

		public Sport GetSport(long sportId)
		{
			String sqlStr = "SELECT id, name, user_id FROM sport WHERE id = " + sportId + "";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			Sport sport = null;

			if (sqlReader.Read())
			{
				sport = new Sport();
				sport.id = sqlReader.GetInt32(0);
				sport.name = sqlReader.GetString(1);
				sport.userId = sqlReader.GetInt32(2);
			}

			sqlReader.Close();
			return sport;
		}

		public long InsertSport(Sport sport)
		{
			String sqlStr = "INSERT INTO sport (name, user_id) VALUES ('" + sport.name + "', " + sport.userId + ")";
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

		public bool DeleteSport(long sportId)
		{
			String sqlStr = "SELECT * FROM Sport WHERE id = " + sportId + "";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				sqlStr = "DELETE FROM Sport WHERE Id = " + sportId + "";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.ExecuteNonQuery();

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool UpdateSport(Sport sport)
		{
			String sqlStr = "SELECT * FROM Sport WHERE id = " + sport.id;
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				sqlStr = "UPDATE Sport SET name =  '" + sport.name + "' WHERE id = " + sport.id;
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