using System;
using System.Collections.Generic;
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
	}
}
