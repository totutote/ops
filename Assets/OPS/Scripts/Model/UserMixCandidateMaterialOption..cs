using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class UserMixCandidateMaterialOptionDB : BaseSqliteModel<UserMixCandidateMaterialOptionModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_mix_candidate_material_options"; } }

        [Inject]
        public MasterOptionDB _masterOptionDB;

        protected override UserMixCandidateMaterialOptionModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMixCandidateMaterialOptionModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.user_mix_candidate_material_id.Value = (int)DataRow["user_mix_candidate_material_id"];
            model.master_option_id.Value = (int)DataRow["master_option_id"];
            model.sort_index.Value = (int)DataRow["sort_index"];
            model.option_type.Value = (int)DataRow["option_type"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMixCandidateMaterialOptionModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["user_mix_candidate_material_id"] = model.user_mix_candidate_material_id.Value;
            dataRow["master_option_id"] = model.master_option_id.Value;
            dataRow["sort_index"] = model.sort_index.Value;
            dataRow["option_type"] = model.option_type.Value;
            return dataRow;
        }

        public class Factory : PlaceholderFactory<UserMixCandidateMaterialOptionDB>
        {
        }
    }

    public class UserMixCandidateMaterialOptionModel
    {
        UserMixCandidateMaterialOptionDB _userMixCandidateMaterialOptionDB;

        public void SetDB(UserMixCandidateMaterialOptionDB userMixCandidateMaterialOptionDB)
        {
            _userMixCandidateMaterialOptionDB = userMixCandidateMaterialOptionDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty user_mix_candidate_material_id = new IntReactiveProperty();
        public IntReactiveProperty master_option_id = new IntReactiveProperty();
        public IntReactiveProperty sort_index = new IntReactiveProperty();
        public IntReactiveProperty option_type = new IntReactiveProperty();

        public MasterOptionModel MasterOptionModel
        {
            get { return _userMixCandidateMaterialOptionDB._masterOptionDB.Id(master_option_id.Value).First().Value; }
        }
    }

}