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
            if ((int)saveData["id"] == 0)
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
            foreach (var record in insertData)
            {
                if (record.Key == "id") continue;
                culumnsString += ", " + record.Key;
                if (record.Value.GetType() == typeof(int))
                {
                    valuesString += ", " + record.Value;
                }
                else if (record.Value.GetType() == typeof(string))
                {
                    valuesString += ", " + "'" + record.Value + "'";
                }
            }
            db.ExecuteQuery("insert into " + tableName + "(" + culumnsString.Remove(0, 1) + ") values(" + valuesString.Remove(0, 1) + ")");
            return db.ExecuteQuery("select * from " + tableName + " order by id DESC LIMIT 1;");
        }

        public DataTable Update(DataRow updateData)
        {
            string setString = "";
            foreach (var record in updateData)
            {
                if (record.Value.GetType() == typeof(int)) setString += ", " + record.Key + " = " + record.Value;
                if (record.Value.GetType() == typeof(string)) setString += ", " + record.Key + " = \"" + record.Value + "\"";
            }
            return db.ExecuteQuery("update " + tableName + " set " + setString.Remove(0, 1) + " where id = " + updateData["id"]);
        }

        public void Delete(Dictionary<string, object> deleteData)
        {
            db.ExecuteQuery("delete from " + tableName + " where id = " + deleteData["id"]);
        }

    }

}