using UniRx;

namespace OPS.Model
{

    public class UserMaterialDB : BaseSqliteModel<UserMaterialModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_materials"; } }

        protected override UserMaterialModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMaterialModel();
            model.id.Value = (int)DataRow["id"];
            model.group_id.Value = (int)DataRow["group_id"];
            model.option_type.Value = (int)DataRow["option_type"];
            model.master_option_id.Value = (int)DataRow["master_option_id"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMaterialModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["group_id"] = model.group_id.Value;
            dataRow["option_type"] = model.option_type.Value;
            dataRow["master_option_id"] = model.master_option_id.Value;
            return dataRow;
        }
    }

    public class UserMaterialModel
    {
        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty group_id = new IntReactiveProperty();
        public IntReactiveProperty option_type = new IntReactiveProperty();
        public IntReactiveProperty master_option_id = new IntReactiveProperty();

    }

}