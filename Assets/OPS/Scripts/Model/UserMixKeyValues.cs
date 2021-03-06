﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class UserMixKeyValueDB : BaseSqliteModel<UserMixKeyValueModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_mix_key_values"; } }

        protected override UserMixKeyValueModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMixKeyValueModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.user_mix_id.Value = (int)DataRow["user_mix_id"];
            model.key.Value = (string)DataRow["key"];
            model.value.Value = (string)DataRow["value"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMixKeyValueModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["user_mix_id"] = model.user_mix_id.Value;
            dataRow["key"] = model.key.Value;
            dataRow["value"] = model.value.Value;
            return dataRow;
        }

        public void SaveOrCreateInteger(int userMixId, string key, int value)
        {
            var model = Where(new NameValueCollection { { "user_mix_id", userMixId.ToString() }, { "key", "\"" + key + "\"" } }).FirstOrDefault().Value;
            if (model == null)
            {
                if (value == 0) return;
                var newModel = New();
                newModel.user_mix_id.Value = userMixId;
                newModel.key.Value = key;
                newModel.value.Value = value.ToString();
                Save(newModel);
            }
            else
            {
                if (value == 0)
                {
                    Delete(model);
                }
                else
                {
                    model.value.Value = value.ToString();
                    Save(model);
                }
            }
        }

        public class Factory : PlaceholderFactory<UserMixKeyValueDB>
        {
        }
    }

    public class UserMixKeyValueModel
    {
        UserMixKeyValueDB _masterOptionDB;

        public void SetDB(UserMixKeyValueDB masterOptionDB)
        {
            _masterOptionDB = masterOptionDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty user_mix_id = new IntReactiveProperty();
        public StringReactiveProperty key = new StringReactiveProperty();
        public StringReactiveProperty value = new StringReactiveProperty();
    }

}