using MySql.Data.MySqlClient;
using System;
using XtrmCoachRESTServer.Models;

namespace XtrmCoachRESTServer
{
	public class UserPersistence
	{
		MySqlConnection conn;

		public UserPersistence()
		{
			string myConnectionString = "server=127.0.0.1;uid=root;pwd=;database=XtrmCoach";
			try
			{
				conn = new MySqlConnection();
				conn.ConnectionString = myConnectionString;
				conn.Open();
			}
			catch (MySqlException ex)
			{
				throw;
			}
		}

		/*public User getUser(long userId)
		{
			String sqlStr = "SELECT * FROM User WHERE Id = " + userId + "";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			User user = new User();

			if (sqlReader.Read())
			{
				user.id = sqlReader.GetInt32(0);
				user.firstName = sqlReader.GetString(1);
			}

			return user;
		}*/

		/*public ArrayList getUser()
		{
			String sqlStr = "SELECT * FROM User";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			ArrayList users = new ArrayList();
			User user;

			while (sqlReader.Read())
			{
				user = new User();
				user.Id = sqlReader.GetInt32(0);
				user.Name = sqlReader.GetString(1);
				users.Add(user);
			}

			return users;
		}*/

		public long saveUser(User userToSave)
		{
			String sqlStr = "INSERT INTO user (first_name, last_name, email_id, password, is_admin) VALUES ('" + userToSave.firstName + "', '" + userToSave.lastName + "', '" + userToSave.emailId + "', '" + userToSave.password + "', false)";
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

		/*public bool deleteUser(long userId)
		{
			String sqlStr = "SELECT * FROM User WHERE Id = " + userId + "";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				sqlReader.Close();

				sqlStr = "DELETE FROM User WHERE Id = " + userId + "";
				cmd = new MySqlCommand(sqlStr, conn);
				cmd.ExecuteNonQuery();

				return true;
			}
			else
			{
				return false;
			}
		}*/

		public User authenticateUser(string username, string password)
		{
			String sqlStr = "SELECT * FROM User WHERE email_id = '" + username + "' AND password = '" + password + "'";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				User user = new User();

				user.firstName = sqlReader.GetString(1);
				user.lastName = sqlReader.GetString(2);
				user.emailId = sqlReader.GetString(3);
				user.isAdmin = sqlReader.GetBoolean(5);

				return user;
			}

			return null;
		}
	}
}