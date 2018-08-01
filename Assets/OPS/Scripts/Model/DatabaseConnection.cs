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

		public Dictionary<int, T> Where<T>(string culumn, string value) where T : BaseMasterModel, IDataModel, new()
		{
			return assign<T>(db.ExecuteQuery("select * from " + tableName + " where " + culumn + " = " + value));
		}

		public T Save<T>(Dictionary<string, ReactiveProperty<object>> saveData) where T : BaseMasterModel, IDataModel, new()
		{
			if((string)saveData["id"].Value == "")
			{
				return Insert<T>(saveData);
			}
			else
			{
				return Update<T>(saveData);
			}
		}

		public T Insert<T>(Dictionary<string, ReactiveProperty<object>> insertData) where T : BaseMasterModel, IDataModel, new()
		{
			string culumnsString = "";
			string valuesString = "";
			foreach (var record in insertData) {
				culumnsString += ", " + record.Key;
				valuesString += ", " + record.Value;
			}
			return assign<T>(db.ExecuteQuery("insert into " + tableName + "(" + culumnsString.Remove(0, 1) + ") values("+ valuesString.Remove(0, 1) +")"))[0];
		}

		public T Update<T>(Dictionary<string, ReactiveProperty<object>> insertData) where T : BaseMasterModel, IDataModel, new()
		{
			string setString = "";
			foreach (var record in insertData) {
				setString += ", " + record.Key + " = " + (string)record.Value.Value;
			}
			return assign<T>(db.ExecuteQuery("update " + tableName + " set " + setString.Remove(0, 1) + " where id = " + (string)insertData["id"].Value))[0];
		}

		public void Delete(Dictionary<string, ReactiveProperty<object>> deleteData)
		{
			db.ExecuteQuery("delete from " + tableName + " where id = " + deleteData["id"].Value);
		}

	}

}