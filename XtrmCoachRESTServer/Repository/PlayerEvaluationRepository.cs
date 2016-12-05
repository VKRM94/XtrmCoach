using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public class PlayerEvaluationRepository : IPlayerEvaluationRepository
	{
		MySqlConnection conn;

		public PlayerEvaluationRepository()
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

		public ArrayList GetPlayerEvaluations(long sportsId, long playerId)
		{
			String sqlStr = "select pp.id, pp.sport_id, pp.name_id, ppn.name, pp.custom_name, pp.group_id from perf_para pp join perf_para_name ppn on pp.name_id = ppn.id where pp.sport_id = @sportId";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("sportId", sportsId);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList playerEvaluations = new ArrayList();
			PlayerEvaluation pp = null;
			List<int> groupIds = new List<int>();

			while (sqlReader.Read())
			{
				pp = new PlayerEvaluation();
				pp.id = sqlReader.GetInt32(0);
				pp.sportId = sqlReader.GetInt32(1);
				pp.playerId = playerId;

				pp.perfParaName = new PerformanceParameterName();
				pp.perfParaName.id = sqlReader.GetInt32(2);
				pp.perfParaName.name = sqlReader.GetString(3);

				pp.customName = sqlReader.GetString(4);

				groupIds.Add(sqlReader.GetInt32(5));

				playerEvaluations.Add(pp);
			}

			sqlReader.Close();

			for (int groupIdIndex = 0; groupIdIndex < groupIds.Count; groupIdIndex++)
			{
				int groupId = groupIds[groupIdIndex];
				sqlStr = "select id, group_id, name from perf_para_type where group_id = @groupId";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.Parameters.AddWithValue("groupId", groupId);
				MySqlDataReader perfParaTypeSqlReader = cmd.ExecuteReader();

				List<PerformanceParameterType> perfParaTypes = new List<PerformanceParameterType>();
				PerformanceParameterType perfParaType = null;

				while (perfParaTypeSqlReader.Read())
				{
					perfParaType = new PerformanceParameterType();
					perfParaType.id = perfParaTypeSqlReader.GetInt32(0);
					perfParaType.groupId = perfParaTypeSqlReader.GetInt32(1);
					perfParaType.name = perfParaTypeSqlReader.GetString(2);

					perfParaTypes.Add(perfParaType);
				}
				perfParaTypeSqlReader.Close();

				((PlayerEvaluation)playerEvaluations[groupIdIndex]).perfParaTypes = perfParaTypes;

				foreach (PerformanceParameterType paraType in perfParaTypes)
				{
					sqlStr = "select pe.type_id, ppt.group_id, ppt.name, pe.value from player_eval pe join perf_para_type ppt on pe.type_id = ppt.id where pe.sport_id = @sportId and pe.player_id = @playerId and pe.type_id = @typeId and pe.eval_date = current_date()";
					cmd = new MySqlCommand(sqlStr, conn);
					cmd.Parameters.AddWithValue("sportId", sportsId);
					cmd.Parameters.AddWithValue("playerId", playerId);
					cmd.Parameters.AddWithValue("typeId", paraType.id);
					cmd.Parameters.AddWithValue("currentDate", DateTime.Now);
					MySqlDataReader playerEvalSqlReader = cmd.ExecuteReader();

					if (playerEvalSqlReader.Read())
					{
						((PlayerEvaluation)playerEvaluations[groupIdIndex]).selectedType = new PerformanceParameterType();
						((PlayerEvaluation)playerEvaluations[groupIdIndex]).selectedType.id = playerEvalSqlReader.GetInt32(0);
						((PlayerEvaluation)playerEvaluations[groupIdIndex]).selectedType.groupId = playerEvalSqlReader.GetInt32(1);
						((PlayerEvaluation)playerEvaluations[groupIdIndex]).selectedType.name = playerEvalSqlReader.GetString(2);
						((PlayerEvaluation)playerEvaluations[groupIdIndex]).value = playerEvalSqlReader.IsDBNull(3) ? string.Empty : playerEvalSqlReader.GetString(3);

						playerEvalSqlReader.Close();
						break;
					}

					playerEvalSqlReader.Close();
				}
			}

			return playerEvaluations;
		}

		public PlayerEvaluation GetPlayerEvaluation(long evaluationId)
		{
			String sqlStr = "select id, sport_id, player_id, name_id, type_id, value, eval_date from player_eval where id = @evalId";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("evalId", evaluationId);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			PlayerEvaluation pp = null;

			if (sqlReader.Read())
			{
				pp = new PlayerEvaluation();
				pp.id = sqlReader.GetInt32(0);
				pp.sportId = sqlReader.GetInt32(1);
				pp.playerId = sqlReader.GetInt32(2);

				pp.perfParaName = new PerformanceParameterName();
				pp.perfParaName.id = sqlReader.GetInt32(3);

				pp.selectedType = new PerformanceParameterType();
				pp.selectedType.id = sqlReader.GetInt32(4);
				pp.value = sqlReader.GetString(5);
			}

			sqlReader.Close();
			return pp;
		}

		public long InsertPlayerEvaluation(PlayerEvaluation playerEvaluation)
		{
			String sqlStr = "insert into player_eval(sport_id, player_id, name_id, type_id, value, eval_date) values(@sportId, @playerId, @nameId, @typeId, @value, @currentDate)";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("sportId", playerEvaluation.sportId);
			cmd.Parameters.AddWithValue("playerId", playerEvaluation.playerId);
			cmd.Parameters.AddWithValue("nameId", playerEvaluation.perfParaName.id);
			cmd.Parameters.AddWithValue("typeId", playerEvaluation.selectedType.id);
			cmd.Parameters.AddWithValue("value", String.IsNullOrWhiteSpace(playerEvaluation.value) ? null : playerEvaluation.value);
			cmd.Parameters.AddWithValue("currentDate", DateTime.Now);

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

		public bool UpdatePlayerEvaluation(PlayerEvaluation playerEvaluation)
		{
			String sqlStr = "SELECT * FROM player_eval WHERE id = " + playerEvaluation.id;
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				string value = String.IsNullOrWhiteSpace(playerEvaluation.value) ? null : playerEvaluation.value;
				sqlStr = "UPDATE player_eval SET sport_id = " + playerEvaluation.sportId + ", player_id = " + playerEvaluation.playerId + ", name_id = " + playerEvaluation.perfParaName.id + ", type_id = " + playerEvaluation.selectedType.id + ", value = '" + value + "' WHERE id = " + playerEvaluation.id;
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.ExecuteNonQuery();

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool DeletePlayerEvaluation(long playerEvaluationId)
		{
			String sqlStr = "SELECT * FROM player_eval WHERE id = " + playerEvaluationId;
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				sqlStr = "DELETE FROM player_eval WHERE Id = " + playerEvaluationId;
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