using UniRx;

namespace OPS.Model
{

    public class UserMaterialOptionDB : BaseSqliteModel<UserMaterialOptionModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_material"; } }

        protected override UserMaterialOptionModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMaterialOptionModel();
            model.id.Value = (int)DataRow["id"];
            model.name.Value = (string)DataRow["name"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMaterialOptionModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id;
            dataRow["name"] = model.name;
            return dataRow;
        }
    }

    public class UserMaterialOptionModel
    {
        public IntReactiveProperty id = new IntReactiveProperty();
        public StringReactiveProperty name = new StringReactiveProperty();

    }

}