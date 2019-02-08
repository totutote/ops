using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		public DataTable All()
		{
			return db.ExecuteQuery("select * from " + tableName);
		}

		public DataTable Id(int id)
		{
			return db.ExecuteQuery("select * from " + tableName + " where " + " id = " + id);
		}

		public DataTable Where(string culumn, string value)
		{
			return db.ExecuteQuery("select * from " + tableName + " where " + culumn + " = " + value);
		}

		public DataTable Save(DataRow saveData)
		{
			if((string)saveData["id"] == "")
			{
				return Insert(saveData);
			}
			else
			{
				return Update(saveData);
			}
		}

		public DataTable Insert(DataRow insertData)
		{
			string culumnsString = "";
			string valuesString = "";
			foreach (var record in insertData) {
				culumnsString += ", " + record.Key;
				valuesString += ", " + record.Value;
			}
			return db.ExecuteQuery("insert into " + tableName + "(" + culumnsString.Remove(0, 1) + ") values("+ valuesString.Remove(0, 1) +")");
		}

		public DataTable Update(DataRow updateData)
		{
			string setString = "";
			foreach (var record in updateData) {
				setString += ", " + record.Key + " = " + record.Value;
			}
			return db.ExecuteQuery("update " + tableName + " set " + setString.Remove(0, 1) + " where id = " + (string)updateData["id"]);
		}

		public void Delete(Dictionary<string, object> deleteData)
		{
			db.ExecuteQuery("delete from " + tableName + " where id = " + deleteData["id"]);
		}

	}

}