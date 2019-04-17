using UniRx;

namespace OPS.Model
{

    public class UserMixCandidateMaterialDB : BaseSqliteModel<UserMixCandidateMaterialModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_materials"; } }

        protected override UserMixCandidateMaterialModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMixCandidateMaterialModel();
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
    }

    public class UserMixCandidateMaterialModel
    {
        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty user_mix_id = new IntReactiveProperty();
        public IntReactiveProperty sort_index = new IntReactiveProperty();
        public IntReactiveProperty ref_user_mix_id = new IntReactiveProperty();
    }

}