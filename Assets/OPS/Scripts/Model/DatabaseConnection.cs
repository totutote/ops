using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OPS.Model
{

	public class DatabaseConnection
	{
		string dbName;
		string tableName;
		SqliteDatabase db = null;

		public DatabaseConnection(string pdbName, string ptableName)
		{
			dbName = pdbName;
			tableName = ptableName;
			db = new SqliteDatabase(dbName);
		}
		
		public DataTable Query(string query)
		{
			return db.ExecuteQuery(query);
		}

		public static Dictionary<int, T> assign<T>(DataTable table) where T : IDataModel, new()
		{
			var list = new Dictionary<int, T>();
			foreach (var row in table.Rows) {
				var obj = new T();
				foreach (var culumn in table.Columns) { 
					obj.Record.Add((string)culumn, new ReactiveProperty<object>(row[(string)culumn]));
				}
				list.Add((int)row["id"], obj);
			}
			return list;
		}

		public Dictionary<int, T> All<T>() where T : BaseMasterModel, IDataModel, new()
		{
			return assign<T>(db.ExecuteQuery("select * from " + tableName));
		}

		public T Id<T>(int id) where T : BaseMasterModel, IDataModel, new()
		{
			return assign<T>(db.ExecuteQuery("select * from " + tableName + " where " + " id = " + id))[0];
		}

		public DataTable Where(string culumn, string value)
		{
			return db.ExecuteQuery("select * from " + tableName + " where " + culumn + " = " + value);
		}

		public void Insert(Dictionary<string, object> insertData)
		{
			foreach (var record in insertData) {
				db.ExecuteQuery("insert into" + tableName + "values("+record.Key+", "+record.Value+")");
			}
		}

	}

}