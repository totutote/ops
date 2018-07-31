﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OPS.Model
{

	public class MasterOption : BaseMasterModel, IDataModel
	{
		const string dbName = "master.sqlite3";
		const string tableName = "master_options";

		public Dictionary<string, ReactiveProperty<object>> Record {get {return record;}}
		Dictionary<string, ReactiveProperty<object>> record = new Dictionary<string, ReactiveProperty<object>>();

		static DatabaseConnection db = null;

		static DatabaseConnection GetDatabase()
		{
			if (db == null) {
				db = new DatabaseConnection(dbName, tableName);
			}
			return db;
		}

		public static MasterOption Option(int id)
		{
			return GetDatabase().Id<MasterOption>(id);
		}

		public static Dictionary<int, MasterOption> AllOptions()
		{
			return GetDatabase().All<MasterOption>();
		}
	}

}