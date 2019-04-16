using UniRx;

namespace OPS.Model
{

    public class UserMixCandidateMaterialOptionDB : BaseSqliteModel<UserMixCandidateMaterialOptionModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_material_options"; } }

        protected override UserMixCandidateMaterialOptionModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMixCandidateMaterialOptionModel();
            model.id.Value = (int)DataRow["id"];
            model.user_mix_id.Value = (int)DataRow["user_mix_id"];
            model.material_sort_index.Value = (int)DataRow["material_sort_index"];
            model.master_option_id.Value = (int)DataRow["master_option_id"];
            model.sort_index.Value = (int)DataRow["sort_index"];
            model.option_type.Value = (int)DataRow["option_type"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMixCandidateMaterialOptionModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id;
            dataRow["user_mix_id"] = model.user_mix_id.Value;
            dataRow["material_sort_index"] = model.material_sort_index.Value;
            dataRow["master_option_id"] = model.master_option_id.Value;
            dataRow["sort_index"] = model.sort_index.Value;
            dataRow["option_type"] = model.option_type.Value;
            return dataRow;
        }
    }

    public class UserMixCandidateMaterialOptionModel
    {
        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty user_mix_id = new IntReactiveProperty();
        public IntReactiveProperty material_sort_index = new IntReactiveProperty();
        public IntReactiveProperty master_option_id = new IntReactiveProperty();
        public IntReactiveProperty sort_index = new IntReactiveProperty();
        public IntReactiveProperty option_type = new IntReactiveProperty();
    }

}