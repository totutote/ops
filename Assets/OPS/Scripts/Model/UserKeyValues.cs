using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class UserKeyValueDB : BaseSqliteModel<UserKeyValueModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "user_key_values"; } }

        protected override UserKeyValueModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserKeyValueModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.key.Value = (string)DataRow["key"];
            model.value.Value = (string)DataRow["value"];
            return model;
        }

        protected override DataRow Model2DataRow(UserKeyValueModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["key"] = model.key.Value;
            dataRow["value"] = model.value.Value;
            return dataRow;
        }

        public class Factory : PlaceholderFactory<UserKeyValueDB>
        {
        }
    }

    public class UserKeyValueModel
    {
        UserKeyValueDB _masterOptionDB;

        public void SetDB(UserKeyValueDB masterOptionDB)
        {
            _masterOptionDB = masterOptionDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public StringReactiveProperty key = new StringReactiveProperty();
        public StringReactiveProperty value = new StringReactiveProperty();

    }

}