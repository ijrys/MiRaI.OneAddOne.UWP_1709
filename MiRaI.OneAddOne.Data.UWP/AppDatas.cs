using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MiRaI.OneAddOne {
	public class AppDatas {

		public static Guid machineGuid;
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
