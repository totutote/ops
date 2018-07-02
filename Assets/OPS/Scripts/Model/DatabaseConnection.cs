using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OPS.Model
{

	public class DatabaseConnection
	{
		string dbName;
		string tableName;

		public DatabaseConnection(string pdbName, string ptableName)
		{
			dbName = pdbName;
			tableName = ptableName;
			ConnectDB();
		}

		public SqliteDatabase ConnectDB()
		{
			return new SqliteDatabase(dbName);
		}
		
		public DataTable Query(string query)
		{
			return ConnectDB().ExecuteQuery(query);
		}

		public DataTable All()
		{
			return ConnectDB().ExecuteQuery("select * from " + tableName);
		}

		public DataTable Id(int id)
		{
			return ConnectDB().ExecuteQuery("select * from " + tableName + " where " + " id = " + id);
		}

		public DataTable Where(string culumn, string value)
		{
			return ConnectDB().ExecuteQuery("select * from " + tableName + " where " + culumn + " = " + value);
		}

	}

}