using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public class PlayerAnalysisRepository : IPlayerAnalysisRepository
	{
		MySqlConnection conn;

		public PlayerAnalysisRepository()
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

		public HighChart GetPlayerAnalysis(PlayerAnalysis playerAnalysis)
		{
			HighChart highChart = new HighChart();

			highChart.chart = new Chart();
			highChart.chart.type = "line";

			highChart.title = new Title();
			highChart.title.text = "Performance Analysis Report of " + playerAnalysis.perfPara.perfParaName.name + " for " + playerAnalysis.sport.name;

			highChart.subtitle = new Title();

			if (playerAnalysis.timeRange == "LAST1WEEK")
			{
				highChart.subtitle.text = "<strong>Duration</strong>: Last 1 Week";
				playerAnalysis.fromTime = DateTime.Now.AddDays(-7);
				playerAnalysis.toTime = DateTime.Now;
			}
			else if (playerAnalysis.timeRange == "LAST1MONTH")
			{
				highChart.subtitle.text = "Duration: Last 1 Month";
				playerAnalysis.fromTime = DateTime.Now.AddMonths(-1);
				playerAnalysis.toTime = DateTime.Now;
			}
			else if (playerAnalysis.timeRange == "LAST3MONTHS")
			{
				highChart.subtitle.text = "Duration: Last 3 Months";
				playerAnalysis.fromTime = DateTime.Now.AddMonths(-3);
				playerAnalysis.toTime = DateTime.Now;
			}
			else if (playerAnalysis.timeRange == "LAST6MONTHS")
			{
				highChart.subtitle.text = "Duration: Last 6 Months";
				playerAnalysis.fromTime = DateTime.Now.AddMonths(-6);
				playerAnalysis.toTime = DateTime.Now;
			}
			else
			{
				highChart.subtitle.text = "Duration: Custom";
				playerAnalysis.fromTime = DateTime.Now.AddDays(-7);
				playerAnalysis.toTime = DateTime.Now;
			}

			highChart.xAxis = new XAxis();
			highChart.xAxis.categories = new List<string>();

			highChart.yAxis = new YAxis();
			highChart.yAxis.title = new Title();
			highChart.yAxis.title.text = playerAnalysis.perfPara.perfParaName.name;

			highChart.tooltip = new ToolTip();

			highChart.series = new List<SeriesElement>();
			SeriesElement seriesElement;

			String sqlStr = "select id, group_id, name from perf_para_type where group_id = @groupId";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("groupId", playerAnalysis.perfPara.perfParaTypeGroup.id);
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

			MySqlDataReader sqlReader;

			if (perfParaTypes.Count == 1)
			{
				highChart.tooltip.valueSuffix = " " + playerAnalysis.perfPara.perfParaTypeGroup.name;

				DateTime startTime = playerAnalysis.fromTime;
				while (startTime <= playerAnalysis.toTime)
				{
					highChart.xAxis.categories.Add(startTime.Day.ToString() + "-" + startTime.Month.ToString());
					startTime = startTime.AddDays(1);
				}

				for (int i = 0; i < playerAnalysis.players.Count; i++)
				{
					seriesElement = new SeriesElement();
					seriesElement.name = playerAnalysis.players[i].firstName + " " + playerAnalysis.players[i].lastName;

					seriesElement.data = new List<string>();

					sqlStr = "select value from player_eval where sport_id = @sportId and player_id = @playerId and name_id = @nameId and type_id = @typeId and eval_date = @evalDate";
					cmd = new MySqlCommand(sqlStr, conn);
					cmd.Parameters.AddWithValue("sportId", playerAnalysis.sport.id);
					cmd.Parameters.AddWithValue("playerId", playerAnalysis.players[i].id);
					cmd.Parameters.AddWithValue("nameId", playerAnalysis.perfPara.perfParaName.id);
					cmd.Parameters.AddWithValue("typeId", ((PerformanceParameterType)perfParaTypes[0]).id);
					MySqlParameter para;

					startTime = playerAnalysis.fromTime;
					while (startTime <= playerAnalysis.toTime)
					{
						if (cmd.Parameters.Contains("evalDate"))
							cmd.Parameters.RemoveAt(4);

						para = new MySqlParameter("evalDate", MySqlDbType.Date);
						para.Value = startTime;
						cmd.Parameters.Add(para);
						sqlReader = cmd.ExecuteReader();

						if (sqlReader.Read())
						{
							string[] timeformats = { @"m\:ss", @"mm\:ss", @"h\:mm\:ss" };
							TimeSpan duration;

							if (playerAnalysis.perfPara.perfParaTypeGroup.name == "min:sec")
							{
								duration = TimeSpan.ParseExact(sqlReader.GetString(0), timeformats, CultureInfo.InvariantCulture);
								seriesElement.data.Add(duration.TotalSeconds.ToString());
								highChart.tooltip.valueSuffix = " total sec";
							}
							else if (playerAnalysis.perfPara.perfParaTypeGroup.name == "hr:min:sec")
							{
								duration = TimeSpan.ParseExact(sqlReader.GetString(0), timeformats, CultureInfo.InvariantCulture);
								seriesElement.data.Add(duration.TotalSeconds.ToString());
								highChart.tooltip.valueSuffix = " total sec";
							}
							else
							{
								seriesElement.data.Add(sqlReader.GetString(0));
							}
						}
						else
						{
							seriesElement.data.Add(string.Empty);
						}

						sqlReader.Close();
						startTime = startTime.AddDays(1);
					}

					highChart.series.Add(seriesElement);
				}
			}
			else
			{
				highChart.chart.type = "column";

				for (int i = 0; i < perfParaTypes.Count; i++)
				{
					highChart.xAxis.categories.Add(((PerformanceParameterType)perfParaTypes[i]).name);
				}

				for (int i = 0; i < playerAnalysis.players.Count; i++)
				{
					seriesElement = new SeriesElement();
					seriesElement.name = playerAnalysis.players[i].firstName + " " + playerAnalysis.players[i].lastName;

					seriesElement.data = new List<string>();

					sqlStr = "select count(*) from player_eval where sport_id = @sportId and player_id = @playerId and name_id = @nameId and type_id = @typeId and eval_date >= @evalFromDate";
					cmd = new MySqlCommand(sqlStr, conn);
					cmd.Parameters.AddWithValue("sportId", playerAnalysis.sport.id);
					cmd.Parameters.AddWithValue("playerId", playerAnalysis.players[i].id);
					cmd.Parameters.AddWithValue("nameId", playerAnalysis.perfPara.perfParaName.id);

					MySqlParameter para = new MySqlParameter("evalFromDate", MySqlDbType.Date);
					para.Value = playerAnalysis.fromTime;
					cmd.Parameters.Add(para);

					//para = new MySqlParameter("evalToDate", MySqlDbType.Date);
					//para.Value = playerAnalysis.toTime;
					//cmd.Parameters.Add(para);

					for (int j = 0; j < perfParaTypes.Count; j++)
					{
						if (cmd.Parameters.Contains("typeId"))
							cmd.Parameters.RemoveAt(4);

						cmd.Parameters.AddWithValue("typeId", ((PerformanceParameterType)perfParaTypes[j]).id);
						//sqlReader = cmd.ExecuteReader();

						//if (sqlReader.Read())
						//{
						seriesElement.data.Add(cmd.ExecuteScalar().ToString());
						//}
						//else
						//{
						//seriesElement.data.Add(string.Empty);
						//}

						//sqlReader.Close();
					}

					highChart.series.Add(seriesElement);
				}
			}

			return highChart;
		}

		/*
		public ArrayList GetPlayerAnalysiss(long sportsId, long playerId)
		{
			String sqlStr = "select pp.id, pp.sport_id, pp.name_id, ppn.name, pp.custom_name, pp.group_id from perf_para pp join perf_para_name ppn on pp.name_id = ppn.id where pp.sport_id = @sportId";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("sportId", sportsId);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList playerAnalysiss = new ArrayList();
			PlayerAnalysis pp = null;
			List<int> groupIds = new List<int>();

			while (sqlReader.Read())
			{
				pp = new PlayerAnalysis();
				pp.id = sqlReader.GetInt32(0);
				pp.sportId = sqlReader.GetInt32(1);
				pp.playerId = playerId;

				pp.perfParaName = new PerformanceParameterName();
				pp.perfParaName.id = sqlReader.GetInt32(2);
				pp.perfParaName.name = sqlReader.GetString(3);

				pp.customName = sqlReader.GetString(4);

				groupIds.Add(sqlReader.GetInt32(5));

				playerAnalysiss.Add(pp);
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

				((PlayerAnalysis)playerAnalysiss[groupIdIndex]).perfParaTypes = perfParaTypes;

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
						((PlayerAnalysis)playerAnalysiss[groupIdIndex]).selectedType = new PerformanceParameterType();
						((PlayerAnalysis)playerAnalysiss[groupIdIndex]).selectedType.id = playerEvalSqlReader.GetInt32(0);
						((PlayerAnalysis)playerAnalysiss[groupIdIndex]).selectedType.groupId = playerEvalSqlReader.GetInt32(1);
						((PlayerAnalysis)playerAnalysiss[groupIdIndex]).selectedType.name = playerEvalSqlReader.GetString(2);
						((PlayerAnalysis)playerAnalysiss[groupIdIndex]).value = playerEvalSqlReader.IsDBNull(3) ? string.Empty : playerEvalSqlReader.GetString(3);

						playerEvalSqlReader.Close();
						break;
					}

					playerEvalSqlReader.Close();
				}
			}

			return playerAnalysiss;
		}

		public PlayerAnalysis GetPlayerAnalysis(long AnalysisId)
		{
			String sqlStr = "select id, sport_id, player_id, name_id, type_id, value, eval_date from player_eval where id = @evalId";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("evalId", AnalysisId);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			PlayerAnalysis pp = null;

			if (sqlReader.Read())
			{
				pp = new PlayerAnalysis();
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

		public long InsertPlayerAnalysis(PlayerAnalysis playerAnalysis)
		{
			String sqlStr = "insert into player_eval(sport_id, player_id, name_id, type_id, value, eval_date) values(@sportId, @playerId, @nameId, @typeId, @value, @currentDate)";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("sportId", playerAnalysis.sportId);
			cmd.Parameters.AddWithValue("playerId", playerAnalysis.playerId);
			cmd.Parameters.AddWithValue("nameId", playerAnalysis.perfParaName.id);
			cmd.Parameters.AddWithValue("typeId", playerAnalysis.selectedType.id);
			cmd.Parameters.AddWithValue("value", String.IsNullOrWhiteSpace(playerAnalysis.value) ? null : playerAnalysis.value);
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

		public bool UpdatePlayerAnalysis(PlayerAnalysis playerAnalysis)
		{
			String sqlStr = "SELECT * FROM player_eval WHERE id = " + playerAnalysis.id;
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				string value = String.IsNullOrWhiteSpace(playerAnalysis.value) ? null : playerAnalysis.value;
				sqlStr = "UPDATE player_eval SET sport_id = " + playerAnalysis.sportId + ", player_id = " + playerAnalysis.playerId + ", name_id = " + playerAnalysis.perfParaName.id + ", type_id = " + playerAnalysis.selectedType.id + ", value = '" + value + "' WHERE id = " + playerAnalysis.id;
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.ExecuteNonQuery();

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool DeletePlayerAnalysis(long playerAnalysisId)
		{
			String sqlStr = "SELECT * FROM player_eval WHERE id = " + playerAnalysisId;
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				sqlStr = "DELETE FROM player_eval WHERE Id = " + playerAnalysisId;
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.ExecuteNonQuery();

				return true;
			}
			else
			{
				return false;
			}
		}
		*/
	}
}