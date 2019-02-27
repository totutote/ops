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
            model.user_mix_id.Value = (int)DataRow["group_id"];
            model.user_materials_group_id.Value = (int)DataRow["option_type"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMixCandidateMaterialModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id;
            dataRow["group_id"] = model.user_mix_id.Value;
            dataRow["option_type"] = model.user_materials_group_id.Value;
            return dataRow;
        }
    }

    public class UserMixCandidateMaterialModel
    {
        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty user_mix_id = new IntReactiveProperty();
        public IntReactiveProperty user_materials_group_id = new IntReactiveProperty();
    }

}