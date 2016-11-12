using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public class UserRepository : IUserRepository
	{
		MySqlConnection conn;

		public UserRepository()
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

		public User getUser(long userId)
		{
			String sqlStr = "SELECT id, first_name, last_name, email_id, password, is_admin FROM User WHERE Id = " + userId + "";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			User user = new User();

			if (sqlReader.Read())
			{
				user.id = sqlReader.GetInt32(0);
				user.firstName = sqlReader.GetString(1);
				user.lastName = sqlReader.GetString(2);
				user.emailId = sqlReader.GetString(3);
				user.password = sqlReader.GetString(4);
				user.isAdmin = sqlReader.GetBoolean(5);
			}

			sqlReader.Close();
			return user;
		}

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

		public long SaveUser(User userToSave)
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

		public bool deleteUser(long userId)
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
		}

		public User AuthenticateUser(string username, string password)
		{
			String sqlStr = "SELECT * FROM User WHERE email_id = '" + username + "' AND password = '" + password + "'";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();

			if (sqlReader.Read())
			{
				User user = new User();

				user.id = sqlReader.GetInt32(0);
				user.firstName = sqlReader.GetString(1);
				user.lastName = sqlReader.GetString(2);
				user.emailId = sqlReader.GetString(3);
				user.isAdmin = sqlReader.GetBoolean(5);

				sqlReader.Close();
				return user;
			}

			return null;
		}
	}
}