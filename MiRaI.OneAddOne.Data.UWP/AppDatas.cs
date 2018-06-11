using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MiRaI.OneAddOne {
	public class AppDatas {
		//private static string AppDataGet (string key) {
		//	string req = null;
		//	using (SqliteConnection conn = new SqliteConnection()) {
		//		using (SqliteCommand cmd = conn.CreateCommand()) {
		//			cmd.CommandText = "select Value from AppDataSet where Key = @thekey";
		//			cmd.Parameters.AddWithValue("@thekey", key);

		//			conn.Open();
		//			using (SqliteDataReader reader = cmd.ExecuteReader()) {
		//				if (reader.Read()) {
		//					req = reader["Value"].ToString();
		//				}
		//			}
		//		}
		//	}

		//	return req;
		//}

		//private static void AppDataSet (string key, string value) {
		//	using (SqliteConnection conn = new SqliteConnection()) {
		//		using (SqliteCommand cmd = conn.CreateCommand()) {
		//			cmd.CommandText = "update AppDataSet set Value = @thevalue where Key = @thekey";
		//			cmd.Parameters.AddWithValue("@thekey", key);

		//			conn.Open();
		//			using (SqliteDataReader reader = cmd.ExecuteReader()) {
		//				if (reader.Read()) {
		//					req = reader["Value"].ToString();
		//				}
		//			}
		//		}
		//	}
		//}


		public static Guid machineGuid;
		private static string connstr = "Filename=Data/db.db";
		public static Guid MachineGuid() {
			return machineGuid;
		}

		public static void CreateDataBase() {
			try {

				using (SqliteConnection conn = new SqliteConnection("Filename=sqliteSample.db")) {
					using (SqliteCommand cmd = conn.CreateCommand()) {
						string tableCommand = "CREATE TABLE UserSet (ID integer NOT NULL PRIMARY KEY AUTOINCREMENT, Account string NOT NULL UNIQUE, Password string NOT NULL, Nickname string NOT NULL, IsParents boolean NOT NULL DEFAULT false, level integer NOT NULL DEFAULT 1, delflag integer NOT NULL DEFAULT 1);";
						cmd.CommandText = tableCommand;

						conn.Open();
						cmd.ExecuteNonQuery();
					}
					using (SqliteCommand cmd = conn.CreateCommand()) {
						string tableCommand = "CREATE TABLE HistorySet (UID integer NOT NULL, Datetime datetime NOT NULL, Testname string NOT NULL, Acnum integer NOT NULL DEFAULT 0, Wanum integer NOT NULL DEFAULT 0, MaxStreaks integer NOT NULL, Usetime integer NOT NULL);";
						cmd.CommandText = tableCommand;

						conn.Open();
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex) {
				Debug.WriteLine(ex.Message);
			}
		}
	}
}
