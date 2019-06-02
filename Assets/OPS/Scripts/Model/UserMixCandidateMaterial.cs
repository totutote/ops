using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class UserMixCandidateMaterialDB : BaseSqliteModel<UserMixCandidateMaterialModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_mix_candidate_materials"; } }

        [Inject]
        public UserMixCandidateMaterialOptionDB _userMixCandidateMaterialOptionDB = null;

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

        public Dictionary<int, UserMixCandidateMaterialOptionModel> UserMixCandidateMaterialOptionTypeNormalModel
        {
            get { return _userMixCandidateMaterialDB._userMixCandidateMaterialOptionDB.Where(new NameValueCollection { { "user_mix_candidate_material_id", id.Value.ToString() }, { "option_type", ((int)UserMixCandidateMaterialOptionDB.OptionType.Normal).ToString() } }); }
        }

        public UserMixCandidateMaterialOptionModel UserMixCandidateMaterialOptionTypeFartorModel
        {
            get { return _userMixCandidateMaterialDB._userMixCandidateMaterialOptionDB.Where(new NameValueCollection { { "user_mix_candidate_material_id", id.Value.ToString() }, { "option_type", ((int)UserMixCandidateMaterialOptionDB.OptionType.Factor).ToString() } }).FirstOrDefault().Value; }
        }

        public int OptionCount()
        {
            return UserMixCandidateMaterialOptionTypeNormalModel.Count;
        }

        public bool IsIncludeSmeltingOption()
        {
            foreach (var normalOptionModels in UserMixCandidateMaterialOptionTypeNormalModel)
            {
                if (normalOptionModels.Value.master_option_id.Value == 334) return true;
            }
            return false;
        }

        public UserMixCandidateMaterialOptionModel SameCategoryIncludeModel(MasterOptionModel masterOptionModel)
        {
            foreach (var userMixCandidateMaterialOptionModel in UserMixCandidateMaterialOptionTypeNormalModel)
            {
                if (masterOptionModel.category_id.Value == userMixCandidateMaterialOptionModel.Value.MasterOptionModel.category_id.Value)
                {
                    return userMixCandidateMaterialOptionModel.Value;
                }
            }
            return null;
        }

    }

}