using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OPS.Model
{

	public class UserMaterialOption : BaseMasterModel, IDataModel
	{
		const string dbName = "user.sqlite3";
		const string tableName = "user_material";

		public Dictionary<string, ReactiveProperty<object>> Record {get {return record;}}
		Dictionary<string, ReactiveProperty<object>> record = new Dictionary<string, ReactiveProperty<object>>();

		static DatabaseConnection db = null;

		public static Subject<string> subject = new Subject<string>();

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

		public static Dictionary<int, UserMaterialOption> AllOptions()
		{
			return GetDatabase().All<UserMaterialOption>();
		}

		public void Save()
		{
			var saveData = GetDatabase().Save<UserMaterialOption>(record);
			record["id"].Value = saveData.Record["id"].Value;
			subject.OnNext("Save");
		}

        public void Delete()
		{
			GetDatabase().Delete(record);
			subject.OnNext("Delete");
		}

	}

}