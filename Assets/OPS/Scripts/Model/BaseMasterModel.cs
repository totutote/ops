using System.Collections.Generic;
using UniRx;

namespace OPS.Model
{

    public abstract class BaseSqliteModel<T> where T : new()
    {
        public abstract string DbName { get; }

        public abstract string TableName { get; }

        // TODO: publicにせずにメソッドを介して状態を通知する
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
                if (!cacheRecords.ContainsKey(record.Key))
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
                records[(int)dataTable[i]["id"]] = DataRow2Model(dataTable[i]);
            }
            var cachedRecords = Regist(records);
            return cachedRecords;
        }

        void Delete(int id)
        {
            if (!cacheRecords.ContainsKey(id))
            {
                cacheRecords.Remove(id);
            }
        }

        protected abstract T DataRow2Model(DataRow dataTable);

        protected abstract DataRow Model2DataRow(T model);

        public Dictionary<int, T> All()
        {
            return ConvertDataTable(db.All());
        }

        public Dictionary<int, T> Id(int id)
        {
            if(cacheRecords.ContainsKey(id))
            {
                var retDic = new Dictionary<int, T>();
                retDic[id] = cacheRecords[id];
                return retDic;
            }
            return ConvertDataTable(db.Id(id));
        }

        public Dictionary<int, T> Where(string culumn, string value)
        {
            return ConvertDataTable(db.Where(culumn, value));
        }

        public T New()
        {
            return new T();
        }

        public Dictionary<int, T> Save(T saveModel)
        {
            DataRow saveDataRow = Model2DataRow(saveModel);
            DataTable savedDataTable = db.Save(saveDataRow);
            return ConvertDataTable(savedDataTable);
        }

        public void Delete(T deleteModel)
        {
            DataRow deleteDataRow = Model2DataRow(deleteModel);
            db.Delete(deleteDataRow);
            Delete((int)deleteDataRow["id"]);
        }
    }

}