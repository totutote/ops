using System.Collections.Generic;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class UserMixDB : BaseSqliteModel<UserMixModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_mixes"; } }

        [Inject]
        public UserMixCandidateMaterialDB _userMixCandidateMaterialDB;

        protected override UserMixModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMixModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.name.Value = (string)DataRow["name"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMixModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["name"] = model.name.Value;
            return dataRow;
        }

        public class Factory : PlaceholderFactory<UserMixDB>
        {
        }
    }

    public class UserMixModel
    {
        UserMixDB _userMixDB;

        public void SetDB(UserMixDB userMixDB)
        {
            _userMixDB = userMixDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public StringReactiveProperty name = new StringReactiveProperty();

        public Dictionary<int, UserMixCandidateMaterialModel> UserMixCandidateMaterialModel
        {
            get { return _userMixDB._userMixCandidateMaterialDB.Where("user_mix_id", id.Value.ToString()); }
        }
    }

}