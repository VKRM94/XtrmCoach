using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public class PlayerRespository : IPlayerRepository
	{
		MySqlConnection conn;

		public PlayerRespository()
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

		public ArrayList GetPlayers(long userId)
		{
			String sqlStr = "SELECT id, name FROM sport WHERE user_id = @user_id";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("user_id", userId);

			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList players = new ArrayList();
			int playerId = 0;
			Dictionary<int, string> sportInfo = new Dictionary<int, string>();

			while (sqlReader.Read())
			{
				sportInfo.Add(sqlReader.GetInt32(0), sqlReader.GetString(1));
			}

			sqlReader.Close();

			foreach (KeyValuePair<int, string> entry in sportInfo)
			{
				sqlStr = "SELECT id, first_name, last_name, dob, image_id FROM player WHERE id IN (SELECT player_id FROM sport_player_link WHERE sport_id = @sport_id)";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.Parameters.AddWithValue("sport_id", entry.Key);

				MySqlDataReader nestedSqlReader = cmd.ExecuteReader();

				while (nestedSqlReader.Read())
				{
					playerId = nestedSqlReader.GetInt32(0);
					int index = -1;

					for (int i = 0; i < players.Count; i++)
					{
						if (playerId == ((Player)players[i]).id)
						{
							index = i;
							break;
						}
					}

					if (index == -1)
					{
						Player sp = new Player();
						sp.id = nestedSqlReader.GetInt32(0);
						sp.firstName = nestedSqlReader.GetString(1);
						sp.lastName = nestedSqlReader.GetString(2);
						sp.dob = nestedSqlReader.GetDateTime(3);
						sp.imageId = nestedSqlReader.GetString(4);
						sp.sports = new List<Sport>();

						Sport sport = new Sport();
						sport.id = entry.Key;
						sport.name = entry.Value;
						sport.userId = userId;

						sp.sports.Add(sport);

						players.Add(sp);
					}
					else
					{
						Sport sport = new Sport();
						sport.id = entry.Key;
						sport.name = entry.Value;
						sport.userId = userId;

						((Player)players[index]).sports.Add(sport);
					}
				}

				nestedSqlReader.Close();
			}

			return players;
		}

		public Player GetPlayer(long playerId)
		{
			String sqlStr = "SELECT id, first_name, last_name, dob, image_id FROM player WHERE id = @playerId";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("playerId", playerId);

			MySqlDataReader sqlReader = cmd.ExecuteReader();
			Player player = null;

			if (sqlReader.Read())
			{
				sqlStr = "SELECT id, name, user_id FROM sport WHERE id IN (SELECT sport_id FROM sport_player_link WHERE player_id = @playerID)";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.Parameters.AddWithValue("playerID", playerId);

				player = new Player();
				player.id = sqlReader.GetInt32(0);
				player.firstName = sqlReader.GetString(1);
				player.lastName = sqlReader.GetString(2);
				player.dob = sqlReader.GetDateTime(3);
				player.imageId = sqlReader.GetString(4);
				player.sports = new List<Sport>();

				sqlReader.Close();
				MySqlDataReader nestedSqlReader = cmd.ExecuteReader();
				Sport sport;

				while (nestedSqlReader.Read())
				{
					sport = new Sport();
					sport.id = nestedSqlReader.GetInt32(0);
					sport.name = nestedSqlReader.GetString(1);
					sport.userId = nestedSqlReader.GetInt32(2);

					player.sports.Add(sport);
				}

				nestedSqlReader.Close();
			}

			return player;
		}

		public long InsertPlayer(Player player)
		{
			String sqlStr = "INSERT INTO player (first_name, last_name, dob, image_id) VALUES (@firstName, @lastName, @dob, @imageId)";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("firstName", player.firstName);
			cmd.Parameters.AddWithValue("lastName", player.lastName);
			cmd.Parameters.AddWithValue("dob", player.dob);
			cmd.Parameters.AddWithValue("imageId", player.imageId);

			try
			{
				cmd.ExecuteNonQuery();
				long playerId = cmd.LastInsertedId;

				foreach (Sport sport in player.sports)
				{
					sqlStr = "INSERT INTO sport_player_link (sport_id, player_id) VALUES (@sportId, @playerId)";
					cmd = new MySqlCommand(sqlStr, conn);
					cmd.Parameters.AddWithValue("sportId", sport.id);
					cmd.Parameters.AddWithValue("playerId", playerId);

					cmd.ExecuteNonQuery();
				}

				return playerId;
			}
			catch (Exception)
			{
				return -1;
			}
		}


		public bool DeletePlayer(long playerId)
		{
			String sqlStr = "SELECT * FROM player WHERE id = @playerId";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("playerId", playerId);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				sqlStr = "DELETE FROM player WHERE Id = @playerId";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.Parameters.AddWithValue("playerId", playerId);

				cmd.ExecuteNonQuery();

				// removing entries from sport_player_link table for the deleted player
				sqlStr = "DELETE FROM sport_player_link WHERE player_id = @playerId";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.Parameters.AddWithValue("playerId", playerId);
				cmd.ExecuteNonQuery();

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool UpdatePlayer(Player player)
		{
			String sqlStr = "SELECT * FROM player WHERE id = @playerId";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.Parameters.AddWithValue("playerId", player.id);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				sqlStr = "UPDATE player SET first_name = @firstName, last_name = @lastName, dob = @dob, image_id = @imageId WHERE id = @playerId";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.Parameters.AddWithValue("playerId", player.id);
				cmd.Parameters.AddWithValue("firstName", player.firstName);
				cmd.Parameters.AddWithValue("lastName", player.lastName);
				cmd.Parameters.AddWithValue("dob", player.dob);
				cmd.Parameters.AddWithValue("imageId", player.imageId);
				cmd.ExecuteNonQuery();

				ArrayList sportIdsToUpdate = new ArrayList();
				foreach (Sport sport in player.sports)
				{
					sportIdsToUpdate.Add(sport.id);
				}

				ArrayList sportIdsFromDb = new ArrayList();
				sqlStr = "Select sport_id FROM sport_player_link WHERE player_id = @playerId";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.Parameters.AddWithValue("playerId", player.id);
				MySqlDataReader nestedSqlReader = cmd.ExecuteReader();

				while (nestedSqlReader.Read())
				{
					sportIdsFromDb.Add(nestedSqlReader.GetInt64(0));
				}

				nestedSqlReader.Close();

				foreach (var sportIdToUpdate in sportIdsToUpdate)
				{
					int index = -1;
					for (int i = 0; i < sportIdsFromDb.Count; i++)
					{
						if ((long)sportIdToUpdate == (long)sportIdsFromDb[i])
						{
							index = i;
							break;
						}
					}

					if (index == -1)
					{
						sqlStr = "INSERT INTO sport_player_link (sport_id, player_id) VALUES (@sportId, @playerId)";
						cmd = new MySqlCommand(sqlStr, conn);
						cmd.Parameters.AddWithValue("sportId", sportIdToUpdate);
						cmd.Parameters.AddWithValue("playerId", player.id);
						cmd.ExecuteNonQuery();
					}
				}

				foreach (var sportIdFromDb in sportIdsFromDb)
				{
					int index = -1;
					for (int i = 0; i < sportIdsToUpdate.Count; i++)
					{
						if ((long)sportIdFromDb == (long)sportIdsToUpdate[i])
						{
							index = i;
							break;
						}
					}

					if (index == -1)
					{
						sqlStr = "DELETE FROM sport_player_link WHERE sport_id = @sportId AND player_id = @playerId";
						cmd = new MySqlCommand(sqlStr, conn);
						cmd.Parameters.AddWithValue("sportId", sportIdFromDb);
						cmd.Parameters.AddWithValue("playerId", player.id);
						cmd.ExecuteNonQuery();
					}
				}

				return true;
			}
			else
			{
				return false;
			}
		}

	}
}