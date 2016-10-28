using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using XtrmCoachRESTServer.Models;
using System.Collections;

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

		public User getUser(long userId)
		{
			String sqlStr = "SELECT * FROM User WHERE Id = " + userId + "";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			MySqlDataReader sqlReader = cmd.ExecuteReader();
			User user = new User();

			if (sqlReader.Read())
			{
				user.Id = sqlReader.GetInt32(0);
				user.Name = sqlReader.GetString(1);
			}

			return user;
		}

		public ArrayList getUser()
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
		}

		public long saveUser(User userToSave)
		{
			String sqlStr = "INSERT INTO User (Name) VALUES ('" + userToSave.Name + "')";
			MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
			cmd.ExecuteNonQuery();
			return cmd.LastInsertedId;
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

		//public bool authenticateUser(string username, string password)
		//{
		//	String sqlStr = "SELECT * FROM User WHERE Id = " + userId + "";
		//	MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
		//	MySqlDataReader sqlReader = cmd.ExecuteReader();
		//	User user = new User();

		//	if (sqlReader.Read())
		//	{
		//		user.Id = sqlReader.GetInt32(0);
		//		user.Name = sqlReader.GetString(1);
		//	}

		//	return user;
		//}
	}
}