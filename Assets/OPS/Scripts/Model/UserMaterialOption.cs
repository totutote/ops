using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OPS.Model
{

	public class UserMaterialOption : BaseMasterModel, IDataModel
	{
		const string dbName = "user.sqlite3";
		const string tableName = "user_material";

		public Dictionary<string, object> Record {get {return record;}}
		Dictionary<string, object> record = new Dictionary<string, object>();

		static DatabaseConnection db = null;

		static DatabaseConnection GetDatabase()
		{
			if (db == null) {
				db = new DatabaseConnection(dbName, tableName);
			}
			return db;
		}

		public static UserMaterialOption Option(int id)
		{
			return GetDatabase().Id<UserMaterialOption>(id);
		}

		public static List<UserMaterialOption> AllOptions()
		{
			return GetDatabase().All<UserMaterialOption>();
		}
	}

}