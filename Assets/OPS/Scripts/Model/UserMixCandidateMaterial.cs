using System.Collections.Generic;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class UserMixCandidateMaterialDB : BaseSqliteModel<UserMixCandidateMaterialModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_mix_candidate_materials"; } }

        [Inject]
        public UserMixCandidateMaterialOptionDB _userMixCandidateMaterialOptionModel;

        protected override UserMixCandidateMaterialModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMixCandidateMaterialModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.user_mix_id.Value = (int)DataRow["user_mix_id"];
            model.sort_index.Value = (int)DataRow["sort_index"];
            model.ref_user_mix_id.Value = (int)DataRow["ref_user_mix_id"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMixCandidateMaterialModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["user_mix_id"] = model.user_mix_id.Value;
            dataRow["sort_index"] = model.sort_index.Value;
            dataRow["ref_user_mix_id"] = model.ref_user_mix_id.Value;
            return dataRow;
        }

        public class Factory : PlaceholderFactory<UserMixCandidateMaterialDB>
        {
        }
    }

    public class UserMixCandidateMaterialModel
    {
        UserMixCandidateMaterialDB _userMixCandidateMaterialDB;

        public void SetDB(UserMixCandidateMaterialDB userMixCandidateMaterialDB)
        {
            _userMixCandidateMaterialDB = userMixCandidateMaterialDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty user_mix_id = new IntReactiveProperty();
        public IntReactiveProperty sort_index = new IntReactiveProperty();
        public IntReactiveProperty ref_user_mix_id = new IntReactiveProperty();

        public Dictionary<int, UserMixCandidateMaterialOptionModel> UserMixCandidateMaterialOptionModel
        {
            get { return _userMixCandidateMaterialDB._userMixCandidateMaterialOptionModel.Where("material_sort_index", sort_index.Value.ToString()); }
        }
    }

}