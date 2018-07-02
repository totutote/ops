using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OPS.Model
{

	public class MasterOption
	{
		const string dbName = "master.sqlite3";

		const string tableName = "master_options";

		public int id;
		public string name;

		static DatabaseConnection db = null;

		static DatabaseConnection GetDatabase()
		{
			if (db == null) {
				db = new DatabaseConnection(dbName, tableName);
			}
			return db;
		}

		private static List<MasterOption> assine(DataTable table)
		{
			var list = new List<MasterOption>();
			foreach (var row in table.Rows) {
				var master = new MasterOption();
				master.id = (int)row["id"];
				master.name = (string)row["name"];
				list.Add(master);
			}
			return list;
		}

		public static MasterOption Option(int id)
		{
			DataTable table = GetDatabase().Id(id);
			return assine(table)[0];
		}

		public static List<MasterOption> AllOptions()
		{
			DataTable table = GetDatabase().All();
			return assine(table);
		}
	}

}