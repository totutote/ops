using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OPS.Model
{

	public abstract class BaseSqliteModel<T>
	{
        public abstract string DbName {get;}

        public abstract string TableName {get;}

		public ReactiveDictionary<int, T> cacheRecords = new ReactiveDictionary<int, T>();

        protected DatabaseConnection db = null;

        public BaseSqliteModel()
        {
            db = new DatabaseConnection(DbName, TableName);
        }

        public Dictionary<int, T> Regist(Dictionary<int, T> records)
        {
			var refRecords = new Dictionary<int, T>();
			foreach (var record in records)
			{
				if(!cacheRecords.ContainsKey(record.Key))
				{
					cacheRecords[record.Key] = record.Value;
				}
				refRecords[record.Key] = cacheRecords[record.Key];
			}
			return refRecords;
        }

        Dictionary<int, T> ConvertDataTable(DataTable dataTable)
        {
            var records = new Dictionary<int, T>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                records[i] = DataRow2Model(dataTable[i]);
            }
            var cachedRecords = Regist(records);
            return cachedRecords;
        }

        protected abstract T DataRow2Model(DataRow dataTable);

        protected abstract DataRow Model2DataRow(T model);

		public Dictionary<int, T> All()
		{
			return ConvertDataTable(db.All());
		}

		public Dictionary<int, T> Id(int id)
		{
			return ConvertDataTable(db.Id(id));
		}

		public Dictionary<int, T> Where(string culumn, string value)
		{
			return ConvertDataTable(db.Where(culumn, value));
		}
    }

}